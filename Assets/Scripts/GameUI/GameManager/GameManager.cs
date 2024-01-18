using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class GameManager : MonoBehaviour {
    public static GameManager Instance = null;
    
    public static float DmgAmtP1 = 0.1f;
    public static float DmgAmtP2 = 0.1f;

    public static float DefP1 = 0f;
    public static float DefP2 = 0f;
    

    public static bool P1Mode = false;

    private void Awake() {
        DmgAmtP1 = 0.08f;
        DmgAmtP2 = 0.08f;
        DefP1 = 0f;
        DefP2 = 0f;
        if (Instance == null)
            Instance = this;
        else
            Instance = null;
    }

    
}
