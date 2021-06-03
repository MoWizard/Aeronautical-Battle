using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    // Get the Player
    private GameObject player;

    // Reference the Game Manager
    public AudioManager m_AudioManager;

    // Retrieve the particle effects
    public GameObject bulletExplosion;
    public GameObject smallExplosion;
    public GameObject explosion;

    void Awake()
    {
        player = GameObject.Find("Player");
        m_AudioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
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
                m_AudioManager.ExplosionAudio.Play();
                
                player.gameObject.SetActive(false);
                break;

            case "Siege":
                if (other.GetComponent<EnemyImmunity>().Immune == false)
                {
                    // Increase the fuel
                    player.GetComponent<PlayerFuel>().IncreaseFuel(3);
                }
                // Create small explosion
                Instantiate(bulletExplosion, transform.position, transform.rotation);
                break;

            case "Caster":
                if (other.GetComponent<EnemyImmunity>().Immune == false)
                {
                    // Increase the fuel
                    player.GetComponent<PlayerFuel>().IncreaseFuel(5);
                    
                    // Blow the enemy up
                    Instantiate(smallExplosion, other.transform.position, other.transform.rotation);
                    other.gameObject.SetActive(false);
                }
                // Create small explosion
                Instantiate(bulletExplosion, transform.position, transform.rotation);
                m_AudioManager.SmallExplosionAudio.Play();
                break;

            case "Super":
                if (other.GetComponent<EnemyImmunity>().Immune == false)
                {
                    // Increase the fuel
                    player.GetComponent<PlayerFuel>().IncreaseFuel(15);

                    // Blow the enemy up
                    Instantiate(smallExplosion, other.transform.position, other.transform.rotation);
                    other.gameObject.SetActive(false);
                }
                // Create small explosion
                Instantiate(bulletExplosion, transform.position, transform.rotation);
                m_AudioManager.SmallExplosionAudio.Play();
                break;

            case "Homing":
                Instantiate(bulletExplosion, transform.position, transform.rotation);
                m_AudioManager.BulletExplosionAudio.Play();

                // Increase the fuel
                player.GetComponent<PlayerFuel>().IncreaseFuel(5);
                break;

            case "Bullet":
                Instantiate(bulletExplosion, transform.position, transform.rotation);
                m_AudioManager.BulletExplosionAudio.Play();

                // Increase the fuel
                player.GetComponent<PlayerFuel>().IncreaseFuel(1);
                break;

            default:
                if(other.transform.parent.tag == "Formation")
                {
                    return;
                }
                break;
        }

        // Remove the bullet from view
        if(gameObject.tag == "Bullet")
        {
            gameObject.SetActive(false);
        }
    }
}
