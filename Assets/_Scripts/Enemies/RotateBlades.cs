using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlades : MonoBehaviour
{
    // Get the Propeller
    public GameObject mainPropeller;
    public GameObject sidePropeller;
    public GameObject casterEnemy;
    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            isAlive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive == true)
        {
            mainPropeller.transform.Rotate(0, 500 * Time.deltaTime, 0);
            sidePropeller.transform.Rotate(250 * Time.deltaTime, 0, 0);
        }
    }
}
