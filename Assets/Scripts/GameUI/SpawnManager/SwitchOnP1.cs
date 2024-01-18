using UnityEngine;
using TMPro;

public class SwitchOnP1 : MonoBehaviour
{
    public GameObject P1Character;
    public string P1Name = "Player1";
    public TextMeshProUGUI P1Text;

    void Start()
    {
        P1Text.text = P1Name;
        SaveScript.P1Load = P1Character;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
