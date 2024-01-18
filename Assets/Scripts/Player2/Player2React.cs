using System;
using UnityEngine;
using static Player2Action;
using static Player2Animator;

public class Player2React : MonoBehaviour {

    [Header("Object")]
    public Collider CapsuleCollider;
    public Rigidbody rb;
    [Header("SoundEffect")]
    [SerializeField] private AudioClip LightPunch;
    [SerializeField] private AudioClip HeavyPunch;
    [SerializeField] private AudioClip LightKick;
    [SerializeField] private AudioClip HeavyKick;

    private AudioSource MyPlayer;
    private Animator anim;
    private AnimatorStateInfo PlayerLayer0;
    private float dmg;

    public static bool p1vampireMode;

    string[] TriggerTags = { "LightPunch", "HeavyPunch", "LightKick", "HeavyKick", "FireBall" };
    void Start() {
        p1vampireMode = false;
        anim = GetComponentInChildren<Animator>();
        MyPlayer = GetComponentInChildren<AudioSource>();
        MyPlayer.volume = ModeManager.soundValue;
        dmg = GameManager.DmgAmtP1;
    }

    void Update() {
        //dmg = GameManager.DmgAmtP1 - GameManager.DefP2;
    }
    void HandleAttack(string animationTrigger, int value, AudioClip soundClip, float damage, bool falling) {
        anim.SetTrigger(animationTrigger);
        anim.SetFloat("ReactType", value);

        // sound
        MyPlayer.clip = soundClip;
        MyPlayer.Play();

        //dmg
        SaveScript.Player2Health -= (damage-GameManager.DefP2);
        ComboManager.comboCountP1 += 1;

        if (SaveScript.Player2Timer < 2.0f) {
            SaveScript.Player2Timer += 2.0f;
        }

        if (p1vampireMode) { SaveScript.Player1Health += (damage - GameManager.DefP2); }

        if (falling) {
            anim.SetFloat("ReactType", value + 1);

        }

    }
    void OnTriggerEnter(Collider other) {
        if (isBlock) { return;  }
        dmg = GameManager.DmgAmtP1;
        string tag = other.gameObject.tag;
        int kickType = tag == "HeavyKick" ? 1 : 0;
        if (Array.Exists(TriggerTags, t => t == tag)) {
            dmg = tag == "FireBall" ? dmg * 5 : dmg;
            HandleAttack("React", directionP2 * 3 + kickType,
                         tag == "HeavyPunch" ? HeavyPunch : tag == "LightKick" ? LightKick : LightPunch,
                         dmg, tag == "HeavyKick");
        }
        
    }
    



}
