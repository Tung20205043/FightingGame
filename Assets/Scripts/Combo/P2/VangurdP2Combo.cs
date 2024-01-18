using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player2Animator;
public class VangurdP2Combo : MonoBehaviour {
    private Animator anim;
    public GameObject def;

    private bool scale;
    private float scaleUp = 1.2f;

    void Start() {
        anim = GetComponentInChildren<Animator>();
        scale = true;
    }


    void Update() {
        if (SaveScript.Player2Mana <= 1) { return; }

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
        if (scale) {
            StartCoroutine(ChangeScale());
        }


    }

    IEnumerator ChangeScale() {
        anim.SetTrigger("Combo");
        anim.SetFloat("ComboType", Player2Animator.directionP2);
        yield return new WaitForSeconds(1.5f);
        Vector3 newScale = transform.localScale*scaleUp;
        Instantiate(def);
        SaveScript.Player2Mana = 0;
        GameManager.DefP2 = 0.05f;       
        transform.localScale = newScale;
        scale = false;
    }
    

    

   
}
