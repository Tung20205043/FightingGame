using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseWin : MonoBehaviour {
    [Header("Text")]
    public GameObject WinText;
    public GameObject LoseText;
    public GameObject DrawText;
    public GameObject P1WinText;
    public GameObject P2WinText;
    [Header("Sound")]
    public AudioSource MyPlayer;
    public AudioClip LoseAudio;
    public AudioClip P1WinAudio;
    public AudioClip P2WinAudio;
    public float PauseTime = 1.0f;
    public static bool endGame = false;
    private bool hasGameEnded = false;

    
    void Start() {
        SaveScript.TimeOut = false;
        WinText.gameObject.SetActive(false);
        LoseText.gameObject.SetActive(false);
        DrawText.gameObject.SetActive(false);
        endGame = false;
        hasGameEnded = false;
        MyPlayer.volume = ModeManager.soundValue;


    }
    void Update() {
        if (endGame && !hasGameEnded) {
            endGame = false;
            StartCoroutine(WinSet());

        }
        if (SaveScript.Player1Win >= 2 || SaveScript.Player2Win >= 2) {
            StartCoroutine(WaitLoadScene());
            //SceneManager.LoadScene(4);
        }

    }

    // Update is called once per frame
    IEnumerator WinSet() {
        
        hasGameEnded = true;
        TimeManager.timeRun = false;
        yield return new WaitForSeconds(0.5f);
        if (SaveScript.Player1Health > SaveScript.Player2Health && GameManager.P1Mode == true) {
            WinText.gameObject.SetActive(true);
            MyPlayer.Play();
            SaveScript.Player1Win+=1;
        }
        if (SaveScript.Player1Health > SaveScript.Player2Health && GameManager.P1Mode == false) {
            P1WinText.gameObject.SetActive(true);
            MyPlayer.clip = P1WinAudio;
            MyPlayer.Play();
            SaveScript.Player1Win+=1;
        }
        if (SaveScript.Player1Health < SaveScript.Player2Health && GameManager.P1Mode == true) {
            LoseText.gameObject.SetActive(true);
            MyPlayer.clip = LoseAudio;
            MyPlayer.Play();
            SaveScript.Player2Win+=1;
        }
        if (SaveScript.Player1Health < SaveScript.Player2Health && GameManager.P1Mode == false) {
            P2WinText.gameObject.SetActive(true);
            MyPlayer.clip = P2WinAudio;
            MyPlayer.Play();
            SaveScript.Player2Win+=1;
        }

        if (SaveScript.Player1Health == SaveScript.Player2Health) {
            DrawText.gameObject.SetActive(true);
            SaveScript.Player1Win += 0.5f;
            SaveScript.Player2Win += 0.5f;
        }
        yield return new WaitForSeconds(PauseTime);
        DrawText.gameObject.SetActive(false);
        WinText.gameObject.SetActive(false);
        LoseText.gameObject.SetActive(false);
        

        //SaveScript.TimeOut = false;
        yield return new WaitForSeconds(3.5f);
        LoadNewScene();
        

    }

    IEnumerator WaitLoadScene() {
        SaveScript.currentP1Mana = 0;
        SaveScript.currentP2Mana = 0;
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(4);
    }
    void LoadNewScene() {
        SaveScript.currentP1Mana = SaveScript.Player1Mana;
        SaveScript.currentP2Mana = SaveScript.Player2Mana;
        if (SaveScript.Player1Win >= 2 || SaveScript.Player2Win >= 2) { 
            return; 
        }
        SceneManager.LoadScene(3);
    }
}
