using UnityEngine;

public class Player2TriggerAI : MonoBehaviour
{
    protected bool EmitFX = false;
    public ParticleSystem Particles;
    public string ParticleType = "P11";

    public bool Player2 = true;

    private GameObject ChosenParticles;

    private void Start() {
        ChosenParticles = GameObject.Find(ParticleType);
        Particles = ChosenParticles.gameObject.GetComponent<ParticleSystem>();
    }


    void Update() {

    }
    private void OnTriggerEnter(Collider other) {
        if (Player2 == true) {
            if (other.gameObject.CompareTag("Player1")) {
                if (EmitFX == true) {
                    Particles.Play();
                }
            }
        } else if (Player2 == false) {
            if (other.gameObject.CompareTag("Player2")) {
                if (EmitFX == true) {
                    Particles.Play();
                }
            }
        }
    }
}
