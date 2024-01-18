using UnityEngine;
using static Player1Action;
public class PlayerActionAI : MonoBehaviour {
    [SerializeField] private float jumpSpeed = 2f;
    [Header("Object")]
    public GameObject Player2;
    public Rigidbody rb;
    public Collider CapsuleCollider;

    [Header("SoundEffect")]
    [SerializeField] private AudioClip PunchWoosh;
    [SerializeField] private AudioClip KickWoosh;


    private Animator Anim;
    private AnimatorStateInfo Player2Layer0;
    private float atkType = 0.0f;
    private AudioSource MyPlayer;
    private bool isBlock = false;

    enum CurrentLocation { 
        OnGround,
        OnAir
    }
    private CurrentLocation location;
    private void Awake() {
        atkType = 6;
        location = CurrentLocation.OnGround;
    }
    void Start() {
        Anim = GetComponent<Animator>();
        MyPlayer = GetComponent<AudioSource>();
        MyPlayer.volume = ModeManager.soundValue;
    }

    // Update is called once per frame
    void Update() {
        if (!RoundStart.started) { return; }
        Player2Layer0 = Anim.GetCurrentAnimatorStateInfo(0);
        Anim.SetFloat("Attack", atkType);

        //AIblock
        if (currentAnimationState == PlayerAction1.P1Atk ) {
            Anim.SetTrigger("BlockOn");
            Anim.SetFloat("BlockType", PlayerAnimatorAI.OppDistance > 0 ? 0 : 1 );
            isBlock = true;
            rb.isKinematic = true;
            CapsuleCollider.enabled = false;
        }
        //AI 
        //if (currentAnimationState == PlayerAction1.P1Jump && PlayerAnimatorAI.canAttack) {
        //    JumpUp();
        //    Anim.SetTrigger("isAttacking");
        //    atkType = PlayerAnimatorAI.OppDistance > 0 ? 5 : 11;
        //}
        //Anim.SetFloat("Attack", atkType);

        //Exit block
        if (currentAnimationState == PlayerAction1.P1Idle || currentAnimationState == PlayerAction1.P1Move)  {
            isBlock = false;
            Anim.SetTrigger("BlockOff");
            CapsuleCollider.enabled = true;
            rb.isKinematic = false;
            Attack();
        }


        //if (SaveScript.Player2Mana >= 1.0f && currentAnimationState != PlayerAction1.P1Block) {
        //    PlayerAnimatorAI.canAttack = false;
        //    Anim.SetTrigger("Combo");
        //    Anim.SetFloat("ComboType", PlayerAnimatorAI.OppDistance > 0 ? 0 : 1);
        //    SaveScript.Player2Mana = 0.0f;
        //}
        if (/*(currentAnimationState == PlayerAction1.P1Move || currentAnimationState == PlayerAction1.P1Idle)*/
                /*&&*/ PlayerAnimatorAI.canAttack) {
            RandomAttack();
            
        }
        void Attack(){
            //if (
            //    (currentAnimationState == PlayerAction1.P1Move || currentAnimationState == PlayerAction1.P1Idle)
            //    && PlayerAnimatorAI.canAttack)  
            //    { 
            //    RandomAttack();
            //    Anim.SetFloat("Attack", atkType);
            //}
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
        atkType = PlayerAnimatorAI.OppDistance >0 ? Random.Range(6, 11) : Random.Range(0, 5);
        
    }
    public void JumpUp() {
        rb.velocity = new Vector3(0, jumpSpeed, 0);
    }

    public void BackToIdle() { }
    
}
