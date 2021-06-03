using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameManager m_GameManager;
    public GameObject Player;

    //public AudioSource

    // Reference the bullet prefab and where the bullets spawn
    public GameObject bullet;
    public Transform bulletSpawn;

    public float fireRate;
    private float nextFire;

    void Awake()
    {
        m_GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire && m_GameManager.State == GameManager.GameState.Playing && bulletSpawn.parent != null)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            m_GameManager.GetComponent<AudioManager>().PlayerShootingAudio.Play();
        }
    }
}
