using System.Collections;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    [SerializeField] private GameObject FireBall;
    private float speed = 1f;
    

    // Update is called once per frame
    void Update()
    {
        FireBall.transform.Translate(speed * Time.deltaTime, 0, 0);
        DesFB();
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(FireBall.gameObject, 0.5f);
    }

    IEnumerator DesFB() {
        yield return new WaitForSeconds(1.5f);
        Destroy(FireBall.gameObject);
    }
}
