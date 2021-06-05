using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeHealth : MonoBehaviour
{
    // Reference Game Manager
    private GameObject m_GameManager;

    // Reference the player
    private GameObject m_player;

    // Retrieve the particle effects
    public GameObject explosion;

    // Set the HP value
    public int SiegeHP = 5;

    private void Awake()
    {
        m_GameManager = GameObject.Find("GameManager");
        m_player = GameObject.Find("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" && GetComponent<EnemyImmunity>().Immune == false)
        {
            if(SiegeHP <= 1)
            {
                // Explode the Siege Enemy
                Instantiate(explosion, transform.position, transform.rotation);
                m_GameManager.GetComponent<AudioManager>().ExplosionAudio.Play();
                gameObject.SetActive(false);
                m_player.GetComponent<PlayerFuel>().IncreaseFuel(8f);
            }
            else
            {
                SiegeHP--;
            }
        }
    }
}
