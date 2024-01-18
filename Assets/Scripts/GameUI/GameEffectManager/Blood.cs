
using UnityEngine;

public class Blood : MonoBehaviour
{
    public GameObject blood;

    private void Start() {
        Instantiate(blood);
    }
    void Update()
    {
        if (SaveScript.Player2Health >= 0 && TimeManager.timeOut == false) {
            SaveScript.Player2Health -= 0.02f * Time.deltaTime;
        }
        
    }
}
