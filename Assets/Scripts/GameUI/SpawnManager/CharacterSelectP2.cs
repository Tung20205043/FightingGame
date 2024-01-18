
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectP2 : MonoBehaviour
{
    public GameObject[] characterImages;  
    public GameObject[] redFrames;
    public GameObject[] characterPrefab;
    public TextMeshProUGUI p2Name;
    public string[] playerName;
    private int currentIndex = 0;

    public GameObject CharSelectP2;

    public AudioSource ChangeSound;

    void Start() {
        SetActiveFrame(currentIndex);

        ChangeSound.volume = ModeManager.soundValue;
    }

    void Update() {
        p2Name.text = playerName[currentIndex];
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            MoveCharacter(1);
            ChangeSound.Play();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            MoveCharacter(-1);
            ChangeSound.Play();
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            MoveCharacter(-3);
            ChangeSound.Play();
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            MoveCharacter(3);
            ChangeSound.Play();
        }
        CharaterDisplay();


    }

    void MoveCharacter(int offset) {
        int newIndex = Mathf.Clamp(currentIndex + offset, 0, characterImages.Length - 1);
        redFrames[currentIndex].SetActive(false);
        characterPrefab[currentIndex].SetActive(false);
        currentIndex = newIndex;      
        SetActiveFrame(currentIndex);
    }

    void SetActiveFrame(int index) {
        redFrames[index].SetActive(true);
        characterImages[index].SetActive(true);
        characterPrefab[currentIndex].SetActive(true) ;
    }
    void CharaterDisplay() {
        string[] charsName = { "SoldierP2", "KnightP2", "VampireP2", "RobotP2", "WizardP2", "ZombieP2" };
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            CharSelectP2.SetActive(false);
            SaveScript.P2Select = charsName[currentIndex];
            SceneManager.LoadScene(2);
        }
    }
}
