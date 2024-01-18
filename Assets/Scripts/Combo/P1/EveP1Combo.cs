using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player1Action;
using static Player1Animator;
public class EveP1Combo : MonoBehaviour {
    private Animator anim;

    void Start() {
        anim = GetComponentInChildren<Animator>();
    }

    // Trong script xử lý input
    void Update() {
        if (SaveScript.Player1Mana <= 1.0f) { return; }

        if (Input.GetKeyDown(KeyCode.S)) {
            RecordAction("J");
            CheckForCombo();
        }
        if (directionP1 == 0 ? Input.GetKeyDown(KeyCode.D) : Input.GetKeyDown((KeyCode.A))) {
            RecordAction("K");
            CheckForCombo();
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            RecordAction("L");
            CheckForCombo();
        }
    }

    List<string> playerActions = new List<string>();

    void RecordAction(string action) {
        playerActions.Add(action);
    }

    void CheckForCombo() {


        string requiredCombo = "JKL";

        // Chuyển List các hành động thành chuỗi để so sánh
        string currentActionSequence = string.Join("", playerActions);
        if (currentActionSequence == requiredCombo) {
            ExecuteCombo();
            playerActions.Clear();
        } else if (!requiredCombo.StartsWith(currentActionSequence)) {
            playerActions.Clear();
        } else if (currentActionSequence.Length > requiredCombo.Length) {
            playerActions.Clear();
        }

        string[] combo = new string[] { "J", "K", "L" };

        int actionsCount = playerActions.Count;
        if (actionsCount >= combo.Length) {
            bool comboMatch = true;
            for (int i = 0; i < combo.Length; i++) {
                if (playerActions[actionsCount - combo.Length + i] != combo[i]) {
                    comboMatch = false;
                    break;
                }
            }
            if (comboMatch) {
                ExecuteCombo();
                playerActions.Clear();
            }
        }
    }

    void ExecuteCombo() {
        anim.SetTrigger("Combo");
        anim.SetFloat("ComboType", Player1Animator.directionP1);
        currentAnimationState = PlayerAction1.P1Atk;
        StartCoroutine(ResetMana());
    }
    IEnumerator ResetMana() {
        yield return new WaitForSeconds(2.5f);
        SaveScript.Player1Mana = 0.0f;
        currentAnimationState = PlayerAction1.P1Idle;
    }

}
