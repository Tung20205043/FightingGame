using UnityEngine;
using static Player1Animator;
using static Player1Action;
using System;
using TMPro;
public class Player1React : MonoBehaviour
{
    private Animator anim;
    [Header("Object")]
    public Collider CapsuleCollider;
    public Rigidbody rb;
    [Header("SoundEffect")]
    [SerializeField] private AudioClip LightPunch;
    [SerializeField] private AudioClip HeavyPunch;
    [SerializeField] private AudioClip LightKick;
    [SerializeField] private AudioClip HeavyKick;
    private AudioSource MyPlayer;
    private float dmg;

    public static bool p2vampireMode;
    string[] TriggerTags = { "LightPunch", "HeavyPunch", "LightKick", "HeavyKick", "FireBall" };

    void Start()
    {
        p2vampireMode = false;
        anim = GetComponentInChildren<Animator>();
        MyPlayer = GetComponentInChildren<AudioSource>();
        MyPlayer.volume = ModeManager.soundValue;
        dmg = GameManager.DmgAmtP2;
    }

    void Update() {
        //dmg = GameManager.DmgAmtP2 - GameManager.DefP1;

    }

    void HandleAttack(string animationTrigger, int value, AudioClip soundClip, float damage, bool falling) {
        anim.SetTrigger(animationTrigger);
        anim.SetFloat("ReactType", value);
        // sound
        MyPlayer.clip = soundClip;
        MyPlayer.Play();
        //dmg
        SaveScript.Player1Health -= (damage - GameManager.DefP1);
        ComboManager.comboCountP2 += 1;
        //Debug.Log(ComboManager.comboCountP2);

        if (SaveScript.Player1Timer < 2.0f) {
            SaveScript.Player1Timer += 2.0f;
        }
        if (p2vampireMode) {
            SaveScript.Player2Health += (damage - GameManager.DefP1);
        }
        if (falling) {
            anim.SetFloat("ReactType", value + 1);

        }

    }
    void OnTriggerEnter(Collider other) {
        if (isBlock) { return;  }
        dmg = GameManager.DmgAmtP2;
        string tag = other.gameObject.tag;
        int kickType = tag == "HeavyKick" ? 1 : 0;
        if (Array.Exists(TriggerTags, t => t == tag)) {
            if (tag == "FireBall") {
                
                dmg *= 5;
            }

            HandleAttack("React", directionP1 * 3 + kickType,
                         tag == "HeavyPunch" ? HeavyPunch : tag == "LightKick" ? LightKick : LightPunch,
                         dmg, tag == "HeavyKick");
        }
       
    }

    
}
