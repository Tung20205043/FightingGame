using UnityEngine;


public class HealthBars : MonoBehaviour {
    public UnityEngine.UI.Image Player1Green;
    public UnityEngine.UI.Image Player2Green;
    public UnityEngine.UI.Image Player1Red;
    public UnityEngine.UI.Image Player2Red;

    public UnityEngine.UI.Image Player1Mana;
    public UnityEngine.UI.Image Player2Mana;

    // Update is called once per frame
    void Update() {
        
        
        Player1Green.fillAmount = SaveScript.Player1Health;
        Player2Green.fillAmount = SaveScript.Player2Health;

        Player1Mana.fillAmount = SaveScript.Player1Mana;
        Player2Mana.fillAmount= SaveScript.Player2Mana;

        if (SaveScript.Player1Timer > 0) {
            SaveScript.Player1Timer -= 2.0f * Time.deltaTime;
        }
        if (SaveScript.Player1Timer <= 0 && Player1Red.fillAmount > SaveScript.Player1Health) {
            Player1Red.fillAmount -= 0.003f;
        }

        if (SaveScript.Player2Timer > 0) {
            SaveScript.Player2Timer -= 2.0f * Time.deltaTime;
        }

        if (SaveScript.Player2Timer <= 0 && Player2Red.fillAmount > SaveScript.Player2Health) {
            Player2Red.fillAmount -= 0.003f;
        }

        if (RoundStart.started ) {
            if (LoseWin.endGame || TimeManager.timeOut || 
                SaveScript.Player1Health <= 0.01f || SaveScript.Player2Health <= 0.01f) 
                { return; }
            if (SaveScript.Player1Mana < 1.0f) {
                SaveScript.Player1Mana += 0.1f * Time.deltaTime;
            }
            if (SaveScript.Player2Mana < 1.0f) {
                SaveScript.Player2Mana += 0.1f * Time.deltaTime;
            }
        }


        }
    
}
