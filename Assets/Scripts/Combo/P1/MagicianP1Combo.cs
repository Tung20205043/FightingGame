﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player1Action;
using static Player1Animator;
public class MagicianP1Combo : MonoBehaviour {
    private Animator anim;
    public Transform FbSpawn;
    public GameObject FireballP1;
    public GameObject FireballP2;

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
        StartCoroutine(WaitCombo());

    }

    public void SpawnFB1() {
        Instantiate(FireballP1, FbSpawn.position, FbSpawn.rotation);
    }

    public void SpawnFB2() {
        Instantiate(FireballP2, FbSpawn.position, FbSpawn.rotation);
    }
    IEnumerator WaitCombo() {
        currentAnimationState = PlayerAction1.P1Atk;
        yield return new WaitForSeconds(2.8f);
        SaveScript.Player1Mana = 0;
        currentAnimationState = PlayerAction1.P1Idle;

    }

}
