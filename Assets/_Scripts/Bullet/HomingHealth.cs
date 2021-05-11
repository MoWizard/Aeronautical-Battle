using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingHealth : MonoBehaviour
{
    // Retrieve the particle effects
    public GameObject smallExplosion;

    public int HomingHP = 2;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (HomingHP <= 1)
            {
                // Explode the Siege Enemy
                Instantiate(smallExplosion, transform.position, transform.rotation);
                gameObject.SetActive(false);
            }
            else
            {
                HomingHP--;
            }
        }
    }
}
