using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    public GameManager m_GameManager;
    public GameObject bulletExplosion;
    public GameObject playerExplosion;

    public bool casterDestroyed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        Instantiate(bulletExplosion, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            m_GameManager.NextState();
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
    void OnLeaveBoundary()
    {
        Destroy(gameObject);
    }
}
