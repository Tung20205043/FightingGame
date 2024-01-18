using DG.Tweening;
using System.Collections;
using UnityEngine;
using static Player1Action;

public class Player1Animator : MonoBehaviour {

    private Animator anim;

    private float xRange = 1.0f;
    protected bool IsJumping = false;
    public static int directionP1 = 0;
    private float moveX = 0;
    [Header("Object")]
    public GameObject Player1;
    public GameObject Player2;
    public Rigidbody rb;
    public CapsuleCollider capsule;

    private Transform player2Transform;
    public static bool isCrouch = false;
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
    public GameObject WinCheck;

    [Header("DashControl")]
    [SerializeField] private float moveSpeed = 7.5f;
    public float dashBoost;
    public float dashTime;
    private float _dashTime;
    bool isDashing = false;


    void Start() {
        WinCheck = GameObject.Find("WinCheck");

        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");

        player2Transform = GameObject.Find("Player2").transform;
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(FlipToOpponent());
        directionP1 = 0;
        moveX = 0;

        currentLocation = CurrentLocation.OnGround;
    }

    
    void Update() {

        //ready
        if (!RoundStart.started) {
            SetMovement((Direction)directionP1, (MovementType)moveX);
            return;
        }

        isCrouch = Input.GetKey(KeyCode.S);

        //move
        if (currentLocation == CurrentLocation.OnGround && !isCrouch && !isBlock
            && currentAnimationState != PlayerAction1.P1Crouch
            && currentAnimationState != PlayerAction1.P1Block
            && currentAnimationState != PlayerAction1.P1Atk) {
            //move
            float horizontalInput = Input.GetAxis("Horizontal");
            moveX = Mathf.Abs(horizontalInput);
            Vector3 movement = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
            rb.MovePosition(transform.position + movement);

            SetMovement((Direction)directionP1, (MovementType)(moveX == 0 ? 0 : 1));
            currentAnimationState = moveX == 0 ? PlayerAction1.P1Idle : PlayerAction1.P1Move;

        }
        //dash
        if (Input.GetKeyDown(KeyCode.I) && _dashTime <= 0 && !isDashing
            && currentLocation == CurrentLocation.OnGround) {
            moveSpeed += dashBoost;
            _dashTime = dashTime;
            isDashing = true;
            capsule.isTrigger = true;
            rb.useGravity = false;

        }

        if (_dashTime <= 0 && isDashing) {
            moveSpeed -= dashBoost;
            isDashing = false;
            capsule.isTrigger = false;
            rb.useGravity = true;
        } else {
            _dashTime -= Time.deltaTime;
        }



        //cannot exit screen
        transform.position = new Vector3(
        (transform.position.x < -xRange) ? -xRange : (transform.position.x > xRange) ? xRange : transform.position.x, transform.position.y, transform.position.z
        );


        //jump
        if (Input.GetKeyDown(KeyCode.W) && !isBlock) {

            if (!IsJumping) {
                currentAnimationState = PlayerAction1.P1Jump;
                currentLocation = CurrentLocation.OnAir;

                anim.SetTrigger("Jump");
                anim.SetFloat("JumpType", directionP1);
                IsJumping = true;
                StartCoroutine(JumpPause());
            }

        }

        //crouch
        if (isCrouch && !isBlock) {
            currentAnimationState = PlayerAction1.P1Crouch;

            anim.SetBool("Crouch", true);
            anim.SetFloat("CrouchType", directionP1);
        }

        if (!isCrouch && currentAnimationState == PlayerAction1.P1Crouch) {
            anim.SetBool("Crouch", false);
            currentAnimationState = PlayerAction1.P1Idle;
        }



        //knocked out
        if (SaveScript.Player1Health <= 0 || SaveScript.Player2Health <= 0) {
            int a;
            anim.SetTrigger("EndGame");
            anim.SetFloat("EndGameType", directionP1 * 2 + (a = (SaveScript.Player1Health < 0) ? 1 : 0));
            Player1.GetComponent<Player1Action>().enabled = false;
            this.GetComponent<Player1Animator>().enabled = false;
            LoseWin.endGame = true;
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
            if (transform.position.x > player2Transform.position.x) {
                Flip(true);
            } else {
                Flip(false);
            }
        }
    }
    private void Flip(bool faceRight) {
        float targetRotation = faceRight ? -90 : 90;
        Player1.transform.rotation = Quaternion.Euler(0, targetRotation, 0);
        directionP1 = faceRight ? 1 : 0;
    }

    private Tween movementTween = null;
    public void SetMovement(Direction dir, MovementType type) {
        float value = (int)dir * 2 + ((int)type);
        float lastValue = anim.GetFloat("Movement");
        if (movementTween != null)
            movementTween.Kill();
        movementTween = DOTween.To(() => lastValue, x => lastValue = x, value, 0.001f).OnUpdate(() => {
            anim.SetFloat("Movement", lastValue, 0.1f, Time.deltaTime);
        });

    }

}
