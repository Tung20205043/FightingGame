using UnityEngine;

public class SpawmBlood : MonoBehaviour
{
    public GameObject blood;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        Instantiate(blood);
    }

}
