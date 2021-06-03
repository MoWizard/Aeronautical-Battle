using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    // Reference the enemy
    public GameObject enemy;

    // Reference the Audio Manager
    public AudioManager m_AudioManager;

    // Reference the bullet and homing bullet prefab and where the bullets spawn
    public GameObject bullet;
    public GameObject homingBullet;
    public Transform bulletSpawn;

    // Find the player
    private GameObject player;

    public float fireRate;
    public float superFireRate;
    private float nextFire;

    private bool readyToShoot = false;

    private void Awake()
    {
        player = GameObject.Find("Player");
        m_AudioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check to see if the enemy is ready to shoot or not
        if(((tag == "Caster" && other.CompareTag("CasterSpawn")) || (tag == "Siege" && other.tag == "SiegeSpawn") || (tag == "Super" && other.tag == "SuperSpawn")))
        {
            readyToShoot = true;
        }
        else
        {
            readyToShoot = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if the enemy should shoot or not
        if(player.activeSelf == true && readyToShoot == true && enemy.GetComponent<EnemyImmunity>().Immune == false)
        {
            switch (enemy.tag)
            {
                case "Super":
                    if (player.activeSelf == true && Time.time > nextFire)
                    {
                        // Repeatedly fire bullets
                        nextFire = Time.time + superFireRate;
                        Instantiate(homingBullet, bulletSpawn.position, bulletSpawn.rotation);
                        m_AudioManager.EnemyShootingAudio.Play();
                    }
                    break;

                case "Siege":
                case "Caster":
                    if (player.activeSelf == true && Time.time > nextFire)
                    {
                        // Repeatedly fire bullets
                        nextFire = Time.time + fireRate;
                        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                        m_AudioManager.EnemyShootingAudio.Play();
                    }
                    break;
            }
        }
    }
}
