using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Bob();

        if (transform.position.z < 65)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 65);
        }
    }

    // Bob the enemies up and down by a certain amount
    private void Bob()
    {
        switch (gameObject.tag)
        {
            case "Caster":
                transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) + 20, transform.position.z);
                break;
            case "Siege":
                transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time / Mathf.PI) + 20, transform.position.z);
                break;
            case "Super":
                transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) + 20, transform.position.z);
                break;
            default:
                Debug.LogError(gameObject.tag + "is not part of the switch statement in EnemyMovement.cs");
                break;
        }
    }
}
