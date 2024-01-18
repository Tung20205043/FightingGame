using UnityEngine;

public class Player1Trigger : MonoBehaviour
{
    protected bool EmitFX = false;
    public ParticleSystem Particles;
    public string ParticleType = "P21";

    public bool Player1 = true;

    private GameObject ChosenParticles;
    void Start()
    {
        ChosenParticles = GameObject.Find(ParticleType);
        Particles = ChosenParticles.gameObject.GetComponent<ParticleSystem>();

    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (Player1 == true) {
            if (other.gameObject.CompareTag("Player2")) {
                if (EmitFX == true) {
                    Particles.Play();
                }
            }
        } else if (Player1 == false) {
            if (other.gameObject.CompareTag("Player1")) {
                if (EmitFX == true) {
                    Particles.Play();
                }
            }
        }
    }
    
}
