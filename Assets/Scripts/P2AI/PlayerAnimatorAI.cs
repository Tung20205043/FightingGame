using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerAnimatorAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Animator anim;
    private AnimatorStateInfo PlayerLayer0;

    private float xRange = 1.0f;
    protected bool IsJumping = false;
    public static int directionP2 = 0;
    private float moveX = 0;

    public GameObject Player1;
    public GameObject Player2;


    public float AtkDistance = 0.3f;
    public static bool canAttack = false;
    public static float OppDistance;

    public Rigidbody rb;

    private Transform player1Transform;
    public enum Direction {
        Left,
        Right
    }

    public enum MovementType {
        Idle,
        Run
    }
    void Start() {
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");
        player1Transform = GameObject.Find("Player1").transform;
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(FlipToOpponent());
        directionP2 = 1;
        moveX = 0;
    }

    // Update is called once per frame
    void Update() {

        //ready
        if (!RoundStart.started) {
            SetMovement((Direction)directionP2, (MovementType)moveX);
            return;
        }



        PlayerLayer0 = anim.GetCurrentAnimatorStateInfo(0);

        OppDistance = Player1.transform.position.x - Player2.transform.position.x;   
        canAttack = Vector3.Distance(Player1.transform.position, Player2.transform.position) <= AtkDistance ? true : false;

        //AImove
        if (PlayerLayer0.IsTag("Motion") && !canAttack) {
            moveSpeed = OppDistance < 0 ? -6 : 6;
            moveX = 1;
            Vector3 movement = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            rb.MovePosition(transform.position + movement);
        } else { moveX = 0; }
        SetMovement((Direction)directionP2, (MovementType)(moveX == 0 ? 0 : 1));


        //can't exit screen

        if (transform.position.x < -xRange) {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange) {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }




        //jmup
        if (Input.GetAxis("Vertical2") > 0) {

            if (IsJumping == false) {
                anim.SetTrigger("Jump");
                anim.SetFloat("JumpType", directionP2);
                IsJumping = true;
                StartCoroutine(JumpPause());
            }

        }
        

        //victory & lnocked out

        if (SaveScript.Player1Health <= 0 || SaveScript.Player2Health <= 0) {
            int a;
            anim.SetTrigger("EndGame");
            anim.SetFloat("EndGameType", directionP2 * 2 + (a = (SaveScript.Player2Health < 0) ? 1 : 0));
            Player2.GetComponent<PlayerActionAI>().enabled = false;
            this.GetComponent<PlayerAnimatorAI>().enabled = false;
        }

        //timeOut
        if (TimeManager.timeOut == true) {
            Player2.GetComponent<Player2Action>().enabled = false;
            this.GetComponent<PlayerAnimatorAI>().enabled = false;
        }
    }



    IEnumerator JumpPause() {
        yield return new WaitForSeconds(1.0f);
        IsJumping = false;
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
        movementTween = DOTween.To(() => lastValue, x => lastValue = x, value, 0.001f).OnUpdate(() => {
            anim.SetFloat("Movement", lastValue, 0.1f, Time.deltaTime);
        });

    }
    public void BackToIdle() { 

    }
}
