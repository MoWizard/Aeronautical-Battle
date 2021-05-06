using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeHealth : MonoBehaviour
{
    // Retrieve the particle effects
    public GameObject explosion;

    public int SiegeHP = 3;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            if(SiegeHP <= 0)
            {
                // Explode the Siege Enemy
                Instantiate(explosion, transform.position, transform.rotation);
                gameObject.SetActive(false);
            }
            else
            {
                SiegeHP--;
            }
        }
    }
}
