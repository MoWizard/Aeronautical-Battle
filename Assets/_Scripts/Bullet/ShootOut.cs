using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootOut : MonoBehaviour
{
    // Speed of the bullet
    public float m_Speed;

   void Start()
    {
        // Create the rigidbody
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // Set the speed of the bullet
        rigidbody.velocity = transform.forward * m_Speed;
    }
}
