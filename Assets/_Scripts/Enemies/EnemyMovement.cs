using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Reference the Game Manager
    private GameManager m_GameManager;

    // Reference Enemy Types
    public GameObject m_Caster;
    public GameObject m_Siege;
    public GameObject m_Super;

    // Reference each formation
    private GameObject Formation1;
    private GameObject Formation2;
    private GameObject Formation3;
    private GameObject Formation4;
    private GameObject Formation5;

    private Transform[] FirstForm;
    private Transform[] SecondForm;
    private Transform[] ThirdForm;
    private Transform[] FourthForm;
    private Transform[] FifthForm;

    private void Awake()
    {
        // Find all the formation groups in the scene
        //Formation1 = Find("Formation1");  DO THIS ASAP
    }

    private void Start()
    {
        // Put all the transform values into their arrays
        FirstForm = Formation1.GetComponentsInChildren<Transform>();
        SecondForm = Formation2.GetComponentsInChildren<Transform>();
        ThirdForm = Formation3.GetComponentsInChildren<Transform>();
        FourthForm = Formation4.GetComponentsInChildren<Transform>();
        FifthForm = Formation5.GetComponentsInChildren<Transform>();

        // Retrieve the Game Manager
        m_GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Bob();
        MoveToPosition();
    }

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

    private void MoveToPosition()
    {
        switch (m_GameManager.Stage)
        {
            case GameManager.GameStage.FirstStage:
                for(int i = 0; i < FirstForm.Length; i++)
                {
                    if(m_Caster.transform != FirstForm[i].transform)
                    {
                        // Create a new enemy 20 units in the z direction away from the spawn location.
                        GameObject newEnemy = Instantiate(m_Caster, new Vector3(FirstForm[i].position.x, FirstForm[i].position.y, FirstForm[i].position.z - 20), new Quaternion(0f, 180f, 0f, 0f));

                        // Move the enemy to the position of the spawnpoint
                        newEnemy.transform.position = Vector3.Lerp(newEnemy.transform.position, new Vector3(newEnemy.transform.position.x, newEnemy.transform.position.y, FirstForm[i].position.z), 1);
                    }
                }
                break;
        }
    }
}
