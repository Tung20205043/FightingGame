using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour {
    [SerializeField] private GameObject ControlPanel;
    [SerializeField] private GameObject OptionlPanel;
    [SerializeField] private GameObject StartPanel;

    public Slider volume;
    public Slider soundEff;
    public AudioSource sound;

    public static float volValue;
    public static float soundValue;

    public TextMeshProUGUI time;

    public Button plusTime;
    public Button minusTime;

    private Button minusTimeButton;
    private Button plusTimeButton;

    public static bool alreadyStart = false;
    public void Awake() {
        plusTime.onClick.AddListener(() => OnClick(10));
        minusTime.onClick.AddListener(() => OnClick(-10));
    }
    void Start()
    {
        plusTimeButton = plusTime.GetComponent<Button>();
        minusTimeButton = minusTime.GetComponent<Button>();

        volume.value = 0.2f;
        soundEff.value = 0.5f;
        if (alreadyStart) { 
            StartPanel.SetActive(false);
        }
  
    }



    // Update is called once per frame
    void Update()
    {
        volValue = volume.value;
        soundValue = soundEff.value;
        sound.volume = volume.value;
        time.text = "" + TimeManager.SettingTime;
    }

    public void VScom () {
        GameManager.P1Mode = true;
        SceneManager.LoadScene(1);
    }
    public void VSHuman() {
        GameManager.P1Mode = false;
        SceneManager.LoadScene(1);
    }

    public void Control() { 
        ControlPanel.SetActive(true);
    }
    public void Option() {
        OptionlPanel.SetActive(true);
    }
    public void Ready() {
        StartPanel.SetActive(false);
        alreadyStart = true;
    }
    public void Xbutton() {
        ControlPanel.SetActive(false);
    }

    public void Exit() { 
        Application.Quit();
    }
    //option
    public void Xbutton2() {
        OptionlPanel.SetActive(false);
    }
    // time
    void OnClick(int time) {
        int newTime = (int)TimeManager.SettingTime + time;
        if (newTime > 10 && newTime <= 120) { 
            TimeManager.SettingTime = newTime;
        }
        if (TimeManager.SettingTime >= 120) {
            plusTimeButton.gameObject.SetActive(false);
        } else {
            plusTimeButton.gameObject.SetActive(true);
        }
        if (TimeManager.SettingTime <= 20) {
            minusTimeButton.gameObject.SetActive(false);
        } else {
            minusTimeButton.gameObject.SetActive(true);
        }
    }
    
}
