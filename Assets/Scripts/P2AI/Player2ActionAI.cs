using System.Collections;
using UnityEngine;


public class Player2ActionAI : MonoBehaviour {
    [SerializeField] private float jumpSpeed = 0.3f;
    public GameObject Player2;
    private Animator Anim;
    private AnimatorStateInfo Player2Layer0;
    private float atkType = 0.0f;

    private AudioSource MyPlayer;
    [SerializeField] private AudioClip PunchWoosh;
    [SerializeField] private AudioClip KickWoosh;

    private int AtkNum = 1;
    protected bool Attacking = true;
    public float Difficlut;

    void Start() {
        Anim = GetComponent<Animator>();
        MyPlayer = GetComponent<AudioSource>();
        Difficlut = UImode.modeValue;
    }

    // Update is called once per frame
    void Update() {
        Player2Layer0 = Anim.GetCurrentAnimatorStateInfo(0);
        //timeOut
        //if (TimeManager.timeOut == true) {
        //    this.GetComponent<Player2ActionAI>().enabled = false;

        //}


        if (Player2Layer0.IsTag("Motion")) {
            if (Attacking == true) {
                Attacking = false;
                if (AtkNum == 1) {
                    Anim.SetTrigger("isAttacking");
                    atkType = 0.0f;
                }
                if (AtkNum == 2) {
                    Anim.SetTrigger("isAttacking");
                    atkType = 1.0f;
                }
                if (AtkNum == 3) {
                    Anim.SetTrigger("isAttacking");
                    atkType = 2.0f;
                }
                if (AtkNum == 4) {
                    Anim.SetTrigger("isAttacking");
                    atkType = 3.0f;
                }
            }

        }
        if (Player2Layer0.IsTag("Crouching")) {
            Anim.SetTrigger("isAttacking");
            atkType = 4.0f;
            Anim.SetBool("Crouch", false);
            RandomAttack();
        }

        if (Player2Layer0.IsTag("Jumping")) {
            if (Input.GetButtonDown("Fire4P2")) {
                Anim.SetTrigger("isAttacking");
                atkType = 5.0f;
            }
        }

        Anim.SetFloat("Attack", atkType);
    }
    //sound
    public void PunchWooshSound() {
        MyPlayer.clip = PunchWoosh;
        MyPlayer.Play();
    }

    public void KickWooshSound() {
        MyPlayer.clip = KickWoosh;
        MyPlayer.Play();
    }

    //jump
    public void JumpUp() {
        Player2.transform.Translate(0, jumpSpeed, 0);
    }

    //AI attack
    public void RandomAttack() {
        AtkNum = Random.Range(1, 5);
        StartCoroutine(SetAttacking());
    }


    IEnumerator SetAttacking() {
        yield return new WaitForSeconds(Difficlut);
        Attacking = true;
    }
    public void BackToIdle() { }
}
