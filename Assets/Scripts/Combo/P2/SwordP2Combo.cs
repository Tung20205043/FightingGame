using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player2Action;
using static Player2Animator;
public class SwordP2Combo : MonoBehaviour
{
    private Animator anim;
    private float moveSpeed = 1.5f;
    public Rigidbody rb;
    

    void Start() {
        anim = GetComponentInChildren<Animator>();
    }

    // Trong script xử lý input
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
        anim.SetFloat("ComboType", Player2Animator.directionP2);
        StartCoroutine(MoveCharacterDuringCombo());
    }

    IEnumerator MoveCharacterDuringCombo() {
        currentAnimationState = PlayerAction2.P2Atk;
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + new Vector3((directionP2 == 0) ? 0.7f : -0.7f, 0f, 0f);

        while (elapsedTime < 2f) {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / 2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        transform.position = targetPosition;

        SaveScript.Player2Mana = 0;
    }

}
