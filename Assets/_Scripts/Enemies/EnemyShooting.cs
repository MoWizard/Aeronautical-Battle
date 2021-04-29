using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    // Reference the enemy
    public GameObject casterEnemy;
    public GameObject player;

    // Reference the bullet prefab and where the bullets spawn
    public GameObject bullet;
    public Transform bulletSpawn;

    public float fireRate;
    private float nextFire;

    private Vector3 m_LookPosition;

    void Start()
    {
        //GameObject player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(casterEnemy != null) //NEED TO WORK ON THIS
        {
            //m_LookPosition.z = player.transform.position.z - transform.position.z;
            //transform.rotation.z = Mathf.SmoothDamp(transform.rotation.z, m_LookPosition.z, Time.deltaTime);
            Debug.Log(player);
        }
    }
}
