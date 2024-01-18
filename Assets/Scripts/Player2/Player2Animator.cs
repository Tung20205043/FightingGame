using DG.Tweening;
using System.Collections;
using UnityEngine;
using static Player2Action;

public class Player2Animator : MonoBehaviour
{
    private Animator anim;
    private float xRange = 1.0f;
    protected bool IsJumping = false;
    public static int directionP2 = 0;
    private float moveX = 0;

    [Header("Object")]
    public GameObject Player1;
    public GameObject Player2;
    public Rigidbody rb;
    public CapsuleCollider capsule;

    private Transform player1Transform;
    public enum Direction {
        Left,
        Right
    }
    public enum MovementType {
        Idle,
        Run
    }
    public enum CurrentLocation {
        OnGround,
        OnAir
    }
    public static CurrentLocation currentLocation;
    public static bool isCrouch;
    [Header("DashControl")]
    [SerializeField] private float moveSpeed = 7.5f;
    public float dashBoost;
    public float dashTime;
    private float _dashTime;
    bool isDash = false;

    void Start() {
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");

        player1Transform = GameObject.Find("Player1").transform;
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(FlipToOpponent());
        directionP2 = 0;
        moveX = 0;

        currentLocation = CurrentLocation.OnGround;
    }

    // Update is called once per frame
    void Update()
    {
        
        //ready
        if (!RoundStart.started) {
            SetMovement((Direction) directionP2, (MovementType) moveX );
            return;
        }

        isCrouch = Input.GetKey(KeyCode.DownArrow);

        //move
        if (currentLocation == CurrentLocation.OnGround && !isCrouch && !isBlock
            && currentAnimationState != PlayerAction2.P2Crouch
            && currentAnimationState != PlayerAction2.P2Block
            && currentAnimationState != PlayerAction2.P2Atk) {

            float horizontalInput = Input.GetAxis("Horizontal2");
            moveX = Mathf.Abs(horizontalInput);
            Vector3 movement = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
            rb.MovePosition(transform.position + movement);

            SetMovement((Direction)directionP2, (MovementType)(moveX == 0 ? 0 : 1));
            currentAnimationState = moveX == 0 ? PlayerAction2.P2Idle : PlayerAction2.P2Move;
        }
        //dash
        if (Input.GetKeyDown(KeyCode.Keypad5) && _dashTime <= 0 && !isDash
             && currentLocation == CurrentLocation.OnGround) {
            moveSpeed += dashBoost;
            _dashTime = dashTime;
            isDash = true;
            rb.useGravity = false;
            capsule.isTrigger = true;
        }
        if (_dashTime <= 0 && isDash) {
            moveSpeed -= dashBoost;
            isDash = false;
            rb.useGravity = true;
            capsule.isTrigger = false;
        } else { _dashTime -= Time.deltaTime; }

        //can't exit screen

        transform.position = new Vector3(
        (transform.position.x < -xRange) ? -xRange : (transform.position.x > xRange) ? xRange : transform.position.x, transform.position.y, transform.position.z
        );




        //jmup
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isBlock) {

            if (!IsJumping) {

                currentAnimationState = PlayerAction2.P2Jump;
                currentLocation = CurrentLocation.OnAir;

                anim.SetTrigger("Jump");
                anim.SetFloat("JumpType", directionP2);
                IsJumping = true;
                StartCoroutine(JumpPause());
            }

        }
        //crouch
        if (isCrouch) {
            anim.SetBool("Crouch", true);
            anim.SetFloat("CrouchType", directionP2);
            currentAnimationState = PlayerAction2.P2Crouch;

        }
        if (!isCrouch && currentAnimationState == PlayerAction2.P2Crouch) {
            anim.SetBool("Crouch", false);
            currentAnimationState = PlayerAction2.P2Idle;
        }

        //victory & lnocked out

        if (SaveScript.Player1Health <= 0 || SaveScript.Player2Health <= 0) {
            int a;
            anim.SetTrigger("EndGame");
            anim.SetFloat("EndGameType", directionP2 * 2 + (a = (SaveScript.Player2Health<0) ? 1:0 ));
            Player2.GetComponent<Player2Action>().enabled = false;
            this.GetComponent<Player2Animator>().enabled = false;
        }

        //timeOut
        if (TimeManager.timeOut == true) {
            Player2.GetComponent<Player2Action>().enabled = false;
            this.GetComponent<Player2Animator>().enabled = false;
        }
    }

    

    IEnumerator JumpPause() {
        yield return new WaitForSeconds(0.7f);
        IsJumping = false;
        currentLocation = CurrentLocation.OnGround;
    }
    IEnumerator FlipToOpponent() {
        while (true) {
            yield return new WaitForSeconds(0.15f);

            if (transform.position.x > player1Transform.position.x) {
                Flip(true);
            } else {
                Flip(false);
            }
        }
    }

    private void Flip(bool faceRight) {
        float targetRotation = faceRight ? -90 : 90;
        Player2.transform.rotation = Quaternion.Euler(0, targetRotation, 0);
        directionP2 = faceRight ? 1 : 0;
    }

    private Tween movementTween = null;
    public void SetMovement(Direction dir, MovementType type) {
        float value = (int)dir * 2 + ((int)type);
        float lastValue = anim.GetFloat("Movement");
        if (movementTween != null)
            movementTween.Kill();
        movementTween = DOTween.To(() => lastValue, x => lastValue = x, value, 0.001f).OnUpdate(() => 
        {
            anim.SetFloat("Movement", lastValue, 0.1f, Time.deltaTime);
        });
        
    }
}
