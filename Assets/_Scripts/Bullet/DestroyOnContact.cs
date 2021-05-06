using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    // Get the GameManager and Siege Health
    GameManager m_GameManager;
    SiegeHealth m_SiegeHealth;

    // Retrieve the particle effects
    public GameObject bulletExplosion;
    public GameObject explosion;

    void Awake()
    {
        m_GameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check each tag
        switch (other.gameObject.tag)
        {
            case "Player":
                // Create both explosions
                Instantiate(bulletExplosion, transform.position, transform.rotation);
                Instantiate(explosion, other.transform.position, other.transform.rotation);
                other.gameObject.SetActive(false);
                break;

            case "Siege":
                // Create small explosion and decrease health
                Instantiate(bulletExplosion, transform.position, transform.rotation);
                // Most of this is in its own script
                break;

            case "Caster":
                Instantiate(bulletExplosion, transform.position, transform.rotation);
                other.gameObject.SetActive(false);
                break;

            case "Super":
                Instantiate(bulletExplosion, transform.position, transform.rotation);
                other.gameObject.SetActive(false);
                break;

            case "Bullet":
                Instantiate(bulletExplosion, transform.position, transform.rotation);
                break;

            default:
                break;
        }
        // Remove the bullet from view
        gameObject.SetActive(false);
    }
}
