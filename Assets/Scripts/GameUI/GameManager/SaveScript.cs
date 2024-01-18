using UnityEngine;

public class SaveScript : MonoBehaviour
{
    public static float Player1Health = 1.0f;
    public static float Player2Health = 1.0f;
    public static float Player1Timer = 2.0f;
    public static float Player2Timer = 2.0f;

    public static float Player1Mana = 1.0f;
    public static float Player2Mana = 1.0f;
    public static float currentP1Mana = 0;
    public static float currentP2Mana = 0f;
    public static bool TimeOut = true;
    

    public static float Player1Win = 0;
    public static float Player2Win = 0;

    public static string P1Select;
    public static string P2Select;
    public static GameObject P1Load;
    public static GameObject P2Load;

    public void Start() {
        
        Player1Health = 1.0f;
        Player2Health = 1.0f;

        Player1Mana = currentP1Mana;
        Player2Mana = currentP2Mana;
    }
}
