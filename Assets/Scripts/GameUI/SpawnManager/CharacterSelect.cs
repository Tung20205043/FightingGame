
using TMPro;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] characterImages;  
    public GameObject[] redFrames;
    public GameObject[] characterPrefab;
    public TextMeshProUGUI p1Name;
    public string[] playerName;
    private int currentIndex = 0;

    public GameObject CharSelectP1;
    public GameObject CharSelectP2;

    public AudioSource ChangeSound;
    public AudioClip ChooseSound;

    public AudioSource Volume;

    void Start() {
        SetActiveFrame(currentIndex);
        ChangeSound.volume = ModeManager.soundValue;
        Volume.volume = ModeManager.volValue;
        
    }

    void Update() {
        p1Name.text = playerName[currentIndex];
        if (Input.GetKeyDown(KeyCode.D)) {
            MoveCharacter(1);
            ChangeSound.Play();
        } else if (Input.GetKeyDown(KeyCode.A)) {
            MoveCharacter(-1);
            ChangeSound.Play();
        } else if (Input.GetKeyDown(KeyCode.W)) {
            MoveCharacter(-3);
            ChangeSound.Play();
        } else if (Input.GetKeyDown(KeyCode.S)) {
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
        string[] charsName = { "SoldierP1", "KnightP1", "VampireP1", "RobotP1", "WizardP1", "ZombieP1" };
        if (Input.GetKeyDown(KeyCode.J)) {
            ChangeSound.clip = ChooseSound;
            ChangeSound.Play();
            SaveScript.P1Select = charsName[currentIndex];
            CharSelectP1.SetActive(false);
            CharSelectP2.SetActive(true);

        }
    }

   


}
