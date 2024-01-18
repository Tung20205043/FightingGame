using System.Collections;
using UnityEngine;

public class Player2AnimatorAI : MonoBehaviour {
    private float moveX = 0.0f;
    private Animator anim;
    [SerializeField] private float moveSpeed = 10f;
    private AnimatorStateInfo PlayerLayer0;
    [SerializeField] private float xRange = 1.0f;
    public bool IsJumping = false;

    public GameObject Player1;
    public GameObject Player2;
    public Vector3 OppPosition;

    public static bool FacingLeftAI = false;
    public static bool FacingRightAI = true;

    //AI variable
    public float OppDistance;
    public float AttackDistance = 0.5f;
    private bool MoveAI = true;
    public static bool AttackState = false;
    //public static bool IsBlocking = false;

    void Start() {
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");
        FacingLeftAI = false;
        FacingRightAI = true;
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(FaceLeft());
    }

    // Update is called once per frame
    void Update() {

        //ready

        OppPosition = Player1.transform.position;
        if (OppPosition.x > Player2.transform.position.x) {
            StartCoroutine(FaceLeft());
        }
        if (OppPosition.x < Player2.transform.position.x) {
            StartCoroutine(FaceRight());
        }

        if (!RoundStart.started) {
            moveX = 0;
            anim.SetFloat("Movement", moveX);
            return;
        }
        OppDistance = Vector3.Distance(Player1.transform.position, Player2.transform.position);
        PlayerLayer0 = anim.GetCurrentAnimatorStateInfo(0);


        //can't exit screen
        anim.SetFloat("Movement", moveX);
        if (transform.position.x < -xRange) {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange) {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        //face opp
        OppPosition = Player1.transform.position;
        if (OppPosition.x > Player2.transform.position.x) {
            StartCoroutine(FaceLeft());
            //AI move
            if (PlayerLayer0.IsTag("Motion")) {
                if (OppDistance > AttackDistance && MoveAI == true) {
                    anim.SetBool("CanAttack", false);
                    moveX = 1f;
                    AttackState = false;
                    transform.Translate(-moveSpeed, 0, 0);
                }
                if (OppDistance < AttackDistance && MoveAI == true) {
                    anim.SetBool("CanAttack", true);
                    MoveAI = false;
                    moveX -= 0.2f;
                    if (moveX < 0) moveX = 0;
                    StartCoroutine(ForwaedFalse());
                }
            }
        }


        if (OppPosition.x < Player2.transform.position.x) {
            StartCoroutine(FaceRight());
            //AI move
            if (PlayerLayer0.IsTag("Motion")) {
                if (OppDistance > AttackDistance && MoveAI == true) {
                    anim.SetBool("CanAttack", false);
                    moveX = 1f;
                    AttackState = false;
                    transform.Translate(moveSpeed, 0, 0);
                }

                if (OppDistance < AttackDistance && MoveAI == true) {
                    anim.SetBool("CanAttack", true);
                    MoveAI = false;
                    moveX -= 0.2f;
                    if (moveX < 0) moveX = 0;
                    StartCoroutine(ForwaedFalse());
                }
            }
        }



        //AI counter attack
        if (Player2ReactAI.AIDef == 3) {
            anim.SetBool("Crouch", true);
            Player2ReactAI.AIDef = 0;
        }


        //knocked out
        if (SaveScript.Player2Health <= 0) {
            anim.SetTrigger("KnockedOut");
            Player2.GetComponent<Player2ActionAI>().enabled = false;
            this.GetComponent<Player2AnimatorAI>().enabled = false;
        }


        //victory
        if (SaveScript.Player1Health <= 0) {
            anim.SetTrigger("Victory");
            Player2.GetComponent<Player2ActionAI>().enabled = false;
            this.GetComponent<Player2AnimatorAI>().enabled = false;
        }

        //timeOut
        if (TimeManager.timeOut == true) {
            Player2.GetComponent<Player2ActionAI>().enabled = false;
            this.GetComponent<Player2AnimatorAI>().enabled = false;
        }


    }



    IEnumerator JumpPause() {
        yield return new WaitForSeconds(1.0f);
        IsJumping = false;
    }
    IEnumerator FaceLeft() {
        if (FacingLeftAI == true) {
            FacingLeftAI = false;
            FacingRightAI = true;
            yield return new WaitForSeconds(0.15f);
            Player2.transform.Rotate(0, -180, 0);
            anim.SetLayerWeight(1, 0);
        }
    }

    IEnumerator FaceRight() {
        if (FacingRightAI == true) {
            FacingRightAI = false;
            FacingLeftAI = true;
            yield return new WaitForSeconds(0.15f);
            Player2.transform.Rotate(0, 180, 0);
            anim.SetLayerWeight(1, 1);
        }
    }

    IEnumerator ForwaedFalse() {
        yield return new WaitForSeconds(0.01f);
        MoveAI = true;
    }


}
