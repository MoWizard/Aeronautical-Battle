using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    // Reference the enemy
    public GameObject enemy;

    // Reference the Audio Manager
    private AudioManager m_AudioManager;

    // Reference the bullet and homing bullet prefab and where the bullets spawn
    public GameObject bullet;
    public GameObject homingBullet;
    public Transform bulletSpawn;

    // Find the player
    private GameObject m_player;

    public float fireRate;
    public float superFireRate;
    private float nextFire;

    private void Awake()
    {
        m_player = GameObject.Find("Player");
        m_AudioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if the enemy should shoot or not
        if(m_player.activeSelf == true && enemy.GetComponent<EnemyImmunity>().Immune == false)
        {
            switch (enemy.tag)
            {
                case "Super":
                    if (m_player.activeSelf == true && Time.time > nextFire)
                    {
                        // Repeatedly fire bullets
                        nextFire = Time.time + superFireRate;
                        Instantiate(homingBullet, bulletSpawn.position, bulletSpawn.rotation);
                        m_AudioManager.EnemyShootingAudio.Play();
                    }
                    break;

                case "Siege":
                case "Caster":
                    if (m_player.activeSelf == true && Time.time > nextFire)
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
