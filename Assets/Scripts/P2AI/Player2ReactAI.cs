using System.Collections;
using UnityEngine;

public class Player2ReactAI : MonoBehaviour
{
    private Animator anim;
    private AnimatorStateInfo Player2Layer0;

    public Collider CapsuleCollider;
    public Rigidbody rb;

    [SerializeField] private AudioClip LightPunch;
    [SerializeField] private AudioClip HeavyPunch;
    [SerializeField] private AudioClip LightKick;
    [SerializeField] private AudioClip HeavyKick;
    private AudioSource MyPlayer;
    public static int AIDef = 0;
    protected bool IsBlocking = false;

    private string currentTag ;
    void Start()
    {
        Player2React.p1vampireMode = false;
        anim = GetComponentInChildren<Animator>();
        MyPlayer = GetComponentInChildren<AudioSource>();
    }

    void Update() {
        Player2Layer0 = anim.GetCurrentAnimatorStateInfo(0);   
        
        if (AIDef == 2 ) {
            IsBlocking = true;
            if (IsBlocking == true) {
                anim.SetTrigger("BlockOn");
                StartCoroutine(EndBlock());
                
            }
        }

        if ( IsBlocking == true) {
            rb.isKinematic = true;
            CapsuleCollider.enabled = false;
        } else {
            CapsuleCollider.enabled = true;
            rb.isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("LightPunch")) {
            anim.SetTrigger("HeadReact");
            //sound
            MyPlayer.clip = LightPunch;
            MyPlayer.Play();
            //dmg
            SaveScript.Player2Health -= GameManager.DmgAmtP1;
            if (Player2React.p1vampireMode) { SaveScript.Player1Health += GameManager.DmgAmtP1; }
            if (SaveScript.Player2Timer < 2.0f) {
                SaveScript.Player2Timer += 2.0f;
            }
            AIDef  = Random.Range(1, 4);
        }
        if (other.gameObject.CompareTag("HeavyPunch")) {
            anim.SetTrigger("HeadReact");
            //sound
            MyPlayer.clip = HeavyPunch;
            MyPlayer.Play();
            //dmg
            SaveScript.Player2Health -= GameManager.DmgAmtP1;
            if (Player2React.p1vampireMode) { SaveScript.Player1Health += GameManager.DmgAmtP1; }
            if (SaveScript.Player2Timer < 2.0f) {
                SaveScript.Player2Timer += 2.0f;
            }
            AIDef  = Random.Range(1, 4);
        }
        if (other.gameObject.CompareTag("LightKick")) {
            anim.SetTrigger("HeadReact");
            //sound
            MyPlayer.clip = LightKick;
            MyPlayer.Play();
            //dmg
            SaveScript.Player2Health -= GameManager.DmgAmtP1;
            if (Player2React.p1vampireMode) { SaveScript.Player1Health += GameManager.DmgAmtP1; }
            if (SaveScript.Player2Timer < 2.0f) {
                SaveScript.Player2Timer += 2.0f;
            }
            AIDef  = Random.Range(1, 4);
        }
        if (other.gameObject.CompareTag("HeavyKick")) {
            anim.SetTrigger("FallingBack");
            //sound
            MyPlayer.clip = HeavyKick;
            MyPlayer.Play();
            //dmg
            SaveScript.Player2Health -= GameManager.DmgAmtP1;
            if (Player2React.p1vampireMode) { SaveScript.Player1Health += GameManager.DmgAmtP1; }
            if (SaveScript.Player2Timer < 2.0f) {
                SaveScript.Player2Timer += 2.0f;
            }
            AIDef  = Random.Range(1, 4);
        }

        

    }


    IEnumerator EndBlock() {
        yield return new WaitForSeconds(5.0f);
        IsBlocking = false;
        anim.SetTrigger("BlockOff");
        AIDef = 0;
    }
}
