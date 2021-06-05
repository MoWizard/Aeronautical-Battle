using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create the Boundaries of the game in a new class to call later
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerMovement : MonoBehaviour
{
    // Create the Speed and TurnSpeed Variable
    public float m_Speed = 15f;
    public float m_TurnSpeed = 0.5f;

    // Start Position
    public Transform m_StartPosition;

    // Create a target for the rotation
    public float m_RotationTarget = 40f;

    // Reference the Game Manager, Boundary and Rigidbody
    public GameManager m_GameManager;
    public Boundary boundary;
    private Rigidbody m_Rigidbody;

    // Moving up, down, left and right
    private float m_VerticalInputMovement;
    private float m_HorizontalInputMovement;

    // I need this but i don't need this
    private float m_ReferenceThing;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the movement commands
        m_VerticalInputMovement = Input.GetAxis("Vertical");
        m_HorizontalInputMovement = Input.GetAxis("Horizontal");

        // Stay within the bounds of the camera
        transform.position = new Vector3(Mathf.Clamp(m_Rigidbody.position.x, boundary.xMin, boundary.xMax), transform.position.y, Mathf.Clamp(m_Rigidbody.position.z, boundary.zMin, boundary.zMax));
    }

    private void FixedUpdate()
    {
        // Change again once the game manager is being worked on
        if(m_GameManager.State == GameManager.GameState.Playing)
        {
            Move();
            Turn();
        }
        else
        {
            transform.position = m_StartPosition.position;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Move()
    {
        // Move up and down, left and right.
        transform.Translate(Vector3.right * m_HorizontalInputMovement * m_Speed * Time.deltaTime, Space.World);
        transform.Translate(transform.forward * m_VerticalInputMovement * m_Speed * Time.deltaTime, Space.Self);
    }

    private void Turn()
    {
        // Create the Smooth Damp to turn
        float turnRotation = Mathf.SmoothDamp(transform.rotation.z, -1000 * m_RotationTarget * m_HorizontalInputMovement * Time.deltaTime, ref m_ReferenceThing, m_TurnSpeed);

        // Rotate the Rigidbody by the Euler angle provided
        transform.localRotation = Quaternion.Euler(0, 0, turnRotation);
    }
}
