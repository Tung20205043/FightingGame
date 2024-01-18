using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private GameObject P1;
    private GameObject P2;

    public GameObject Player1Character;
    public GameObject Player2Character;
    public Transform Player1Spawn;
    public Transform Player2Spawn;

    public GameObject BackGround;
    public Material newMaterial;

    public Material[] bg;

    void Start()
    {
        newMaterial = bg[BgSelect.currentBgIndex];



        P1 = GameObject.Find(SaveScript.P1Select);
        P1.gameObject.GetComponent<SwitchOnP1>().enabled = true;
        P2 = GameObject.Find(SaveScript.P2Select);
        P2.gameObject.GetComponent<SwitchOnP2>().enabled = true;
        BackGround.gameObject.GetComponent<Renderer>().material = newMaterial;
        StartCoroutine(SpawnPlayer());
    }

    IEnumerator SpawnPlayer() { 
        yield return new WaitForSeconds(0.1f);
        Player1Character = SaveScript.P1Load;
        Player2Character = SaveScript.P2Load;
        Instantiate(Player1Character, Player1Spawn.position, Player1Spawn.rotation);
        Instantiate(Player2Character, Player2Spawn.position, Player2Spawn.rotation);
    }
}
