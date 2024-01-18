using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player2Animator;
public class VampireP2Combo : MonoBehaviour
{
    private Animator anim;
    private bool vamForm = false;
    public GameObject atk;
    void Start() {
        anim = GetComponentInChildren<Animator>();    
    }
    // Update is called once per frame
    void Update() {
        if (SaveScript.Player2Mana <= 1.0f) { return; }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            RecordAction("J");
            CheckForCombo();
        }
        if ((directionP2 == 1) ? Input.GetKeyDown(KeyCode.LeftArrow) : Input.GetKeyDown(KeyCode.RightArrow)) {
            RecordAction("K");
            CheckForCombo();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
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
        if (actionsCount == combo.Length) {
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
        if (Player1React.p2vampireMode) {
            return;
        }
        anim.SetTrigger("Combo");
        anim.SetFloat("ComboType",directionP2);
        StartCoroutine(WaitCombo());
    }    
    IEnumerator WaitCombo() {
        yield return new WaitForSeconds(3.3f);
        SaveScript.Player2Mana = 0.0f;
        SaveScript.Player2Health /= 2;
        GameManager.DmgAmtP2 *= 1.5f;
        Player1React.p2vampireMode = true;
        Instantiate(atk);
    }
}
