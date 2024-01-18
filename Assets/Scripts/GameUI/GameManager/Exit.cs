using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SaveScript.Player1Win = 0;
            SaveScript.Player2Win = 0;
            ModeManager.alreadyStart = true;
            SceneManager.LoadScene(0);

        }
    }
}
