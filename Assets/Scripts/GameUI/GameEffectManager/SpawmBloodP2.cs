using UnityEngine;

public class SpawmBloodP2 : MonoBehaviour
{
    public GameObject bloodP2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        Instantiate(bloodP2);
    }

}
