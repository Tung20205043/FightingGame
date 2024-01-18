using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboManager : MonoBehaviour {
    //player2ComboCount
    public GameObject comboP1Count;
    public TextMeshProUGUI comboP1Text;
    public static float comboCountP1 = 0;
    float previousComboCountP1;

    public GameObject comboP2Count;
    public TextMeshProUGUI comboP2Text;
    public static float comboCountP2 = 0;
    float previousComboCountP2;

    private Animator animator;
    void Start() {
        comboP1Count.SetActive(false);
        comboP2Count.SetActive(false);
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (SaveScript.Player2Timer <= 0) {
            comboP1Count.SetActive(false);
            comboCountP1 = 0;
        }
        
        if (SaveScript.Player1Timer <= 0) {
            comboP2Count.SetActive(false);
            comboCountP2 = 0;
        }
        //
        if (comboCountP1 >= 2) {
            comboP1Count.SetActive(true);
        }
        if (comboCountP2 >= 2) {
            comboP2Count.SetActive(true);
        }
        //
        if (comboCountP1 != previousComboCountP1) {
            previousComboCountP1 = comboCountP1;
            animator.SetTrigger("ComboEffect");
        }
        if (comboCountP2 != previousComboCountP2) {
            previousComboCountP2 = comboCountP2;
            animator.SetTrigger("ComboEffectP2");
        }


        comboP1Text.text = "Combo x" + comboCountP1;
        comboP2Text.text = "Combo x" + comboCountP2;

    }

}