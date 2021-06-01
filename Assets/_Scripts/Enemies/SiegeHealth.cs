using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeHealth : MonoBehaviour
{
    // Reference itself
    public GameObject enemy;

    // Retrieve the particle effects
    public GameObject explosion;

    public int SiegeHP = 5;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" && enemy.GetComponent<EnemyImmunity>().Immune == false)
        {
            if(SiegeHP <= 1)
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
