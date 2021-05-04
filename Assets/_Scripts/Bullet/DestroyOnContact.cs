using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    // Get the GameManager
    public GameManager m_GameManager;

    // Retrieve the particle effects
    public GameObject bulletExplosion;
    public GameObject playerExplosion;

    // Give the siege enemy HP
    public int m_siegeHP = 0;

    void OnTriggerEnter(Collider other)
    {
        // Has it gone out of bounds?
        if (other.tag == "Boundary")
        {
            gameObject.SetActive(false);
            return;
        }

        Instantiate(bulletExplosion, transform.position, transform.rotation);

        // Did a bullet hit the player?
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            m_GameManager.NextState();
        }

        Debug.LogWarning(other.gameObject.tag);
        
        // Is the enemy a siege enemy?
        if(other.tag == "Siege")
        {
            if(m_siegeHP < 3)
            {
                m_siegeHP++;
                Debug.LogWarning(m_siegeHP);
            }
            else
            {
                other.gameObject.SetActive(false);
            }
        }
        else
        {
            other.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    // Destroy all bullets after 5 seconds and their particle effects after 7 seconds
    void Update()
    {
        Destroy(gameObject, 5);
        //Destroy(bulletExplosion, 7); // FIX THIS
    }
}
