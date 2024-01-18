using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player1Animator;

public class VamPireP1Combo : MonoBehaviour
{
    private Animator anim;
    public GameObject atk;

    void Start() {
        anim = GetComponentInChildren<Animator>();
    }

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
        if (Player2React.p1vampireMode) {
            return;
        }
        anim.SetTrigger("Combo");
        anim.SetFloat("ComboType", Player1Animator.directionP1);
        StartCoroutine(WaitCombo());
        
    }
    IEnumerator WaitCombo() {
        yield return new WaitForSeconds(3.3f);
        SaveScript.Player1Mana = 0.0f;
        SaveScript.Player1Health /= 2;
        GameManager.DmgAmtP1 *= 1.5f;
        Player2React.p1vampireMode = true;
        Instantiate(atk);
    }

}
