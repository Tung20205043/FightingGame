using UnityEngine;
using TMPro;

public class SwitchOnP2 : MonoBehaviour
{
    public GameObject P2Character;
    public GameObject AICharacter;
    public string P2Name = "Player2";
    public TextMeshProUGUI P2Text;

    void Start()
    {
        P2Text.text = P2Name;
        if (!GameManager.P1Mode) {
            SaveScript.P2Load = P2Character;
        } else if (GameManager.P1Mode) {
            SaveScript.P2Load = AICharacter;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
