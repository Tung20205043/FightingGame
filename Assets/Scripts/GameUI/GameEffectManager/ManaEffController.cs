using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaEffController : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(1,1);
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.Player1Mana >= 1) {
            anim.SetBool("FullMana", true);
        } else { anim.SetBool("FullMana", false); }

        if (SaveScript.Player2Mana >= 1) {
            anim.SetBool("FullManaP2", true);
        } else { anim.SetBool("FullManaP2", false); }
    }
}
