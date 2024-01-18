using System.Collections;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static bool timeRun = true;
    public static bool timeOut = false;
    public TextMeshProUGUI TimerText;
    public static float SettingTime = 60;
    public float LevelTime;
    
    void Start()
    {
        TimerText.text = "" + SettingTime;
        LevelTime = SettingTime;
        timeRun = true;
        timeOut = false;
    }

    // Update is called once per frame
    void Update() {
        StartCoroutine(TimeRun());
    }
    public void TimeOut() {
        if (timeOut) { LoseWin.endGame = true; }

    }
    IEnumerator TimeRun() {
        yield return new WaitForSeconds(2.3f);
        if (LevelTime > 0 && timeRun == true) {
            LevelTime -= 1 * Time.deltaTime;
        }
        if (LevelTime < 0.01) {
            RoundStart.started = false;
            timeOut = true;
            TimeOut();
        }
        TimerText.text = Mathf.Round(LevelTime).ToString();
    }
}
