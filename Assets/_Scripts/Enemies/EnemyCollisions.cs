using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    // Reference Enemy Types
    public GameObject m_Caster;
    public GameObject m_Siege;
    public GameObject m_Super;

    // Check if there is an enemy on spawn
    [HideInInspector]
    public bool isOccupied = false;
    [HideInInspector]
    public GameObject enemyOnSpawn = null;

    [HideInInspector]
    public GameObject enemyType;
    
    //[HideInInspector]
    //public Transform self;

    void Start()
    {
        if (gameObject.tag == "CasterSpawn")
        {
            enemyType = m_Caster;
        }
        else if (gameObject.tag == "SiegeSpawn")
        {
            enemyType = m_Siege;
        }
        else if (gameObject.tag == "SuperSpawn")
        {
            enemyType = m_Super;
        }
        else
        {
            Debug.LogError("There is no enemyType on " + gameObject.name + " in " + gameObject.transform.parent.name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //self = gameObject.transform;

        if (other.tag == "Caster" && gameObject.tag == "CasterSpawn")
        {
            enemyOnSpawn = other.gameObject;
            isOccupied = true;
        }
        else if (other.tag == "Siege" && gameObject.tag == "SiegeSpawn")
        {
            enemyOnSpawn = other.gameObject;
            isOccupied = true;
        }
        else if (other.tag == "Super" && gameObject.tag == "SuperSpawn")
        {
            enemyOnSpawn = other.gameObject;
            isOccupied = true;
        }
        else if (other.tag == "Bullet" || other.tag == "Player" || other.tag == "FuelUp" || other.tag == "CasterSpawn" || other.tag == "SiegeSpawn" || other.tag == "SuperSpawn")
        {
            return;
        }
        else
        {
            isOccupied = false;
            Debug.LogError("There is no enemyType on " + gameObject.name + " in " + gameObject.transform.parent.name);
        }
    }

    void Update()
    {
        if(enemyOnSpawn != null)
        {
            if (enemyOnSpawn.activeSelf == true)
            {
                isOccupied = true;
            }
            else
            {
                Destroy(enemyOnSpawn);
                enemyOnSpawn = null;
                isOccupied = false;
            }
        }
        else
        {
            isOccupied = false;
        }
        
    }
}
