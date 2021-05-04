using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlades : MonoBehaviour
{
    // Get the Propeller
    public GameObject mainPropeller;
    public GameObject sidePropeller;
    public GameObject casterEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Rotates the blades every frame
    void Update()
    {
        mainPropeller.transform.Rotate(0, 500 * Time.deltaTime, 0);
        sidePropeller.transform.Rotate(250 * Time.deltaTime, 0, 0);
    }
}
