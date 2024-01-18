using UnityEngine;

public class VicScreen : MonoBehaviour {
    public GameObject[] vicChar;
    public GameObject[] vicText;
    public GameObject ynText;

    // Update is called once per frame
    void Update() {
        DisplayVictory(SaveScript.Player1Win, SaveScript.P1Select);
        DisplayVictory(SaveScript.Player2Win, SaveScript.P2Select);
        if (Input.GetKeyDown(KeyCode.J)) { ynText.SetActive(true); }
    }

    void DisplayVictory(float playerWinCount, string playerSelect) {
        if (playerWinCount >= 2) {
            int characterIndex = GetCharacterIndex(playerSelect, SaveScript.Player2Win >= 2);

            if (characterIndex != -1) {
                vicChar[characterIndex].SetActive(true);
                vicText[characterIndex].SetActive(true);
            }
        }
    }

    int GetCharacterIndex(string characterName, bool isPlayer2) {
        string[] characterNamesP1 = { "SoldierP1", "KnightP1", "VampireP1", "RobotP1", "WizardP1", "ZombieP1" };
        string[] characterNamesP2 = { "SoldierP2", "KnightP2", "VampireP2", "RobotP2", "WizardP2", "ZombieP2" };
        string[] characterNames = isPlayer2 ? characterNamesP2 : characterNamesP1;
        return System.Array.IndexOf(characterNames, characterName);
 
    }
}
