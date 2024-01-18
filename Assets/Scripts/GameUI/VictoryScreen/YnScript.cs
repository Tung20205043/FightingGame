using UnityEngine;
using UnityEngine.SceneManagement;

public class YnScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) {
            SaveScript.Player1Win = 0;
            SaveScript.Player2Win = 0;
            SceneManager.LoadScene(0); 
        }
        if (Input.GetKeyDown(KeyCode.N)) { SceneManager.LoadScene(5); 
        }

    }
}
