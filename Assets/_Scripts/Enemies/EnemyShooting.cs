using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    // Reference the enemy
    public GameObject enemy;

    // Reference the bullet and homing bullet prefab and where the bullets spawn
    public GameObject bullet;
    public GameObject homingBullet;
    public Transform bulletSpawn;

    // Find the player
    private GameObject player;

    public float fireRate;
    public float superFireRate;
    private float nextFire;

    private void Awake()
    {
        player = GameObject.Find("PlayerMesh");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            switch (enemy.tag)
            {
                case "Super":
                    if (player.activeSelf == true && Time.time > nextFire)
                    {
                        // Repeatedly fire bullets
                        nextFire = Time.time + superFireRate;
                        Instantiate(homingBullet, bulletSpawn.position, bulletSpawn.rotation);
                    }
                    break;

                case "Siege":
                case "Caster":
                    if (player.activeSelf == true && Time.time > nextFire)
                    {
                        // Repeatedly fire bullets
                        nextFire = Time.time + fireRate;
                        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                    }
                    break;
            }
        }
    }
}
