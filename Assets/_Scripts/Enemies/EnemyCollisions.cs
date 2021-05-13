using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    // Reference Enemy Types
    public GameObject m_Caster;
    public GameObject m_Siege;
    public GameObject m_Super;

    public bool isOccupied = false;
    public GameObject enemyType;
    public Transform self;

    void OnTriggerEnter(Collider other)
    {
        self = gameObject.transform;

        if (other.tag == "Caster" && gameObject.tag == "Caster")
        {
            isOccupied = true;
            enemyType = m_Caster;
        }
        else if (other.tag == "Siege" && gameObject.tag == "Siege")
        {
            isOccupied = true;
            enemyType = m_Siege;
        }
        else if (other.tag == "Super" && gameObject.tag == "Super")
        {
            isOccupied = true;
            enemyType = m_Super;
        }
        else
        {
            isOccupied = false;
            enemyType = null;
        }
    }
}
