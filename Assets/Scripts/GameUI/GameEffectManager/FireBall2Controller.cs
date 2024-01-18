using UnityEngine;

public class FireBall2Controller : MonoBehaviour
{
    [SerializeField] private GameObject FireBall;
    private float speed = 1f;
    

    // Update is called once per frame
    void Update()
    {
        FireBall.transform.Translate(-speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(FireBall.gameObject, 0.5f);
    }
}
