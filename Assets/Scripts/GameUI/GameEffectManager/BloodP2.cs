
using UnityEngine;

public class BloodP2 : MonoBehaviour
{
    public GameObject blood;

    private void Start() {
        Instantiate(blood);
    }
    void Update()
    {
        if (SaveScript.Player1Health >= 0 && TimeManager.timeOut == false) {
            SaveScript.Player1Health -= 0.02f * Time.deltaTime;
        }
    }
}
