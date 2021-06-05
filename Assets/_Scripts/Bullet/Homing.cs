using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
    // The speed and rotation of the bullet 
    public float speed = 15;
    public float rotationSpeed = 1000;

    // The distance at which this object stops following its target and continues on its last known trajectory.
    public float focusDistance = 10;

    // The transform of the target object.
    private Transform target;

    // Returns true if the object should be looking at the target. 
    private bool isLookingAtObject = true;

    // Make it so there isn't a blind spot
    private bool firstShot = true;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Move towards the player
    private void Update()
    {
        Vector3 targetDirection = target.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0F);
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);

        // This prevents anyone getting so close to the enemy that it can't aim at them
        if (firstShot)
        {
            transform.rotation = Quaternion.LookRotation(newDirection);
            firstShot = false;
        }


        if (Vector3.Distance(transform.position, target.position) < focusDistance)
        {
            isLookingAtObject = false;
        }

        if (isLookingAtObject)
        {
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
