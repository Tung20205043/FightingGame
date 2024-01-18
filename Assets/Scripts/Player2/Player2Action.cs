using UnityEngine;
using static Player2Animator;

public class Player2Action : MonoBehaviour {
    [SerializeField] private float jumpSpeed = 2f;
    [Header("Object")]
    public GameObject Player2;
    public Rigidbody rb;

    [Header("SoundEffect")]
    [SerializeField] private AudioClip PunchWoosh;
    [SerializeField] private AudioClip KickWoosh;


    private Animator Anim;
    private AnimatorStateInfo Player2Layer0;
    private float atkType = 0.0f;
    private AudioSource MyPlayer;

    private KeyCode[] atkKey = { KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4 };
    public enum PlayerAction2 {
        P2Atk,
        P2Block,
        P2Jump,
        P2Idle,
        P2Crouch,
        P2Move
    }
    public static PlayerAction2 currentAnimationState;
    public static bool isBlock = false;

    void Start() {
        Anim = GetComponent<Animator>();
        MyPlayer = GetComponent<AudioSource>();
        MyPlayer.volume = ModeManager.soundValue;

        currentAnimationState = PlayerAction2.P2Idle;

    }

    // Update is called once per frame
    void Update() {
        if (!RoundStart.started) { return; }
        //Debug.Log(currentAnimationState);
        isBlock = Input.GetKey(KeyCode.Keypad6);
        if (currentLocation == CurrentLocation.OnGround && 
            (currentAnimationState == PlayerAction2.P2Idle || currentAnimationState == PlayerAction2.P2Move)) {
            //atk
            foreach (KeyCode key in atkKey) {
                if (Input.GetKeyDown(key)) {

                    Anim.SetTrigger("isAttacking");
                    atkType = System.Array.IndexOf(atkKey, key);
                    SetAttack(directionP2, (int)atkType);

                    currentAnimationState = PlayerAction2.P2Atk;
                }
            }
        }
        //blockOn

        if (isBlock && !isCrouch && currentLocation == CurrentLocation.OnGround) {
            currentAnimationState = PlayerAction2.P2Block;
            Anim.SetBool("Block", true);
            Anim.SetFloat("BlockType", directionP2);
        }


        //blockOff

        if (currentAnimationState == PlayerAction2.P2Block && !isBlock) {
            Anim.SetBool("Block", false);
            currentAnimationState = PlayerAction2.P2Idle;
        }
        //crouch atk
        if (currentAnimationState == PlayerAction2.P2Crouch) {
            if (Input.GetKeyDown(atkKey[2])) {
                atkType = 4.0f;
                Anim.SetTrigger("isAttacking");
                currentAnimationState = PlayerAction2.P2Atk;
            }
            SetAttack(directionP2, (int)atkType);
        }
        //jump atk
        if (currentAnimationState == PlayerAction2.P2Jump) {
            if (Input.GetKeyDown(atkKey[3])) {
                currentAnimationState = PlayerAction2.P2Atk;
                Anim.SetTrigger("isAttacking");
                atkType = 5.0f;
                SetAttack(directionP2, (int)atkType);
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
        Anim.SetFloat("Attack", value);
    }

    public void BackToIdle() {
        currentAnimationState = PlayerAction2.P2Idle;
    }
}
