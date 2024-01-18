using System.Collections;
using UnityEngine;

public class RoundStart : MonoBehaviour
{
    [SerializeField] private GameObject RoundText;
    [SerializeField] private GameObject Round2Text;
    [SerializeField] private GameObject Round3Text;

    [SerializeField] private GameObject FightText;
    [SerializeField] private AudioSource MyPlayer;
    [SerializeField] private AudioClip Round2Audio;
    [SerializeField] private AudioClip Round3Audio;

    [SerializeField] private AudioSource Music;

    [SerializeField] private AudioClip FightAudio;
    private float PauseTime = 1.0f;

    public static bool started = false;



    void Start()
    {
        
        started = false;
        RoundText.gameObject.SetActive(false);
        FightText.gameObject.SetActive(false);
        StartCoroutine(RoundSet());

        Music.volume = ModeManager.volValue;
        MyPlayer.volume = ModeManager.soundValue;

    }
    


    IEnumerator RoundSet() {
        if (SaveScript.Player1Win+SaveScript.Player2Win == 0) {
            yield return new WaitForSeconds(0.5f);
            RoundText.gameObject.SetActive(true);
            MyPlayer.Play();
            //
            yield return new WaitForSeconds(PauseTime);
            RoundText.gameObject.SetActive(false);
            //
            yield return new WaitForSeconds(0.5f);
            FightText.gameObject.SetActive(true);
            MyPlayer.clip = FightAudio;
            MyPlayer.Play();
            //
            Music.Play();

            yield return new WaitForSeconds(PauseTime);
            FightText.gameObject.SetActive(false);
            SaveScript.TimeOut = false;
            this.gameObject.SetActive(false);

            started = true;
        }

        if (SaveScript.Player1Win + SaveScript.Player2Win == 1) {
            yield return new WaitForSeconds(0.5f);
            Round2Text.gameObject.SetActive(true);
            MyPlayer.clip = Round2Audio; MyPlayer.Play();
            //
            yield return new WaitForSeconds(PauseTime);
            Round2Text.gameObject.SetActive(false);
            //
            yield return new WaitForSeconds(0.5f);
            FightText.gameObject.SetActive(true);
            MyPlayer.clip = FightAudio;
            MyPlayer.Play();
            //
            Music.Play();

            yield return new WaitForSeconds(PauseTime);
            FightText.gameObject.SetActive(false);
            SaveScript.TimeOut = false;
            this.gameObject.SetActive(false);

            started = true;
        }

        if (SaveScript.Player1Win + SaveScript.Player2Win == 2) {
            yield return new WaitForSeconds(0.5f);
            Round3Text.gameObject.SetActive(true);
            MyPlayer.clip = Round3Audio; MyPlayer.Play();
            //
            yield return new WaitForSeconds(PauseTime);
            Round3Text.gameObject.SetActive(false);
            //
            yield return new WaitForSeconds(0.5f);
            FightText.gameObject.SetActive(true);
            MyPlayer.clip = FightAudio;
            MyPlayer.Play();
            //
            Music.Play();

            yield return new WaitForSeconds(PauseTime);
            FightText.gameObject.SetActive(false);
            SaveScript.TimeOut = false;
            this.gameObject.SetActive(false);

            started = true;
        }
    }
    
}
