using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player2Action;
using static Player2Animator;
public class MagicianP2Combo : MonoBehaviour
{
    private Animator anim;
    public Transform FbSpawn;
    public GameObject FireballP1;
    public GameObject FireballP2;

    void Start() {
        anim = GetComponentInChildren<Animator>();
    }
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
        anim.SetFloat("ComboType", Player2Animator.directionP2);
        StartCoroutine(WaitCombo());
    }

    public void SpawnFB1() {
        Instantiate(FireballP1, FbSpawn.position, FbSpawn.rotation);
    }

    public void SpawnFB2() {
        Instantiate(FireballP2, FbSpawn.position, FbSpawn.rotation);
    }
    IEnumerator WaitCombo() {
        currentAnimationState = PlayerAction2.P2Atk;
        yield return new WaitForSeconds(2.8f);
        SaveScript.Player2Mana = 0f;
        
    }

}
