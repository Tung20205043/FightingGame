using UnityEngine;
using static Player1Animator;

public class Player1Action : MonoBehaviour {
    [SerializeField] private float jumpSpeed = 10f;
    [Header("Object")]
    public GameObject Player1;
    public Rigidbody rb;

    [Header("SoundEffect")]
    [SerializeField] private AudioClip PunchWoosh;
    [SerializeField] private AudioClip KickWoosh;

    private Animator anim;
    private AnimatorStateInfo Player1Layer0;
    private float atkType = 0.0f;
    private AudioSource MyPlayer;

    private KeyCode[] atkKey = { KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.U };
    public enum PlayerAction1 {
        P1Atk,
        P1Block,
        P1Jump,
        P1Idle,
        P1Crouch,
        P1Move
    }
    public static PlayerAction1 currentAnimationState;

    public static bool isBlock = false;

    void Start() {
        Debug.Log(UImode.modeValue);
        anim = GetComponent<Animator>();
        MyPlayer = GetComponent<AudioSource>();
        MyPlayer.volume = ModeManager.soundValue;

        currentAnimationState = PlayerAction1.P1Idle;
    }

    //Update is called once per frame
    void Update() {
       
        if (!RoundStart.started) { return; }
        //Debug.Log(currentAnimationState);
        //Debug.Log(currentLocation);     
        isBlock = Input.GetKey(KeyCode.O);


        if (currentLocation == CurrentLocation.OnGround 
            && (currentAnimationState == PlayerAction1.P1Idle 
            || currentAnimationState == PlayerAction1.P1Move)) {
            //atk
            foreach (KeyCode key in atkKey) {
                if (Input.GetKeyDown(key)) {
                    currentAnimationState = PlayerAction1.P1Atk;
                    anim.SetTrigger("isAttacking");
                    atkType = System.Array.IndexOf(atkKey, key);
                    SetAttack(directionP1, (int)atkType);
                }
            }
        }
        //blockOn

        if (isBlock && !isCrouch && currentLocation == CurrentLocation.OnGround) {
            currentAnimationState = PlayerAction1.P1Block;
            anim.SetBool("Block", true);
            anim.SetFloat("BlockType", directionP1);
        }

        //blockOff

        if (currentAnimationState == PlayerAction1.P1Block && !isBlock) {
            anim.SetBool("Block", false);
            currentAnimationState = PlayerAction1.P1Idle;
        }
        //crouch atk
        if (currentAnimationState == PlayerAction1.P1Crouch) {
            if (Input.GetKeyDown(atkKey[2])) {
                atkType = 4.0f;
                anim.SetTrigger("isAttacking");
                currentAnimationState = PlayerAction1.P1Atk;
            }

            SetAttack(directionP1, (int)atkType);
        }
        //jump atk
        if (currentAnimationState == PlayerAction1.P1Jump) {
            if (Input.GetKeyDown(atkKey[3])) {
                currentAnimationState = PlayerAction1.P1Atk;
                anim.SetTrigger("isAttacking");
                atkType = 5.0f;
                SetAttack(directionP1, (int)atkType);
            }
        }


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

    public void RandomAttack() {

    }
    public void JumpUp() {
        rb.velocity = new Vector3(0, jumpSpeed, 0);
    }

    public void SetAttack(int dir, int type) {

        float value = dir * 6 + (type);
        anim.SetFloat("Attack", value);
    }
    public void BackToIdle() {
        currentAnimationState = PlayerAction1.P1Idle;
    }

}
