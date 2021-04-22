using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Create a Speed Variable
    public float m_Speed = 15f;
    public float m_TurnSpeed = 0.5f;

    public float m_RotationTarget = 40f;

    // Reference the Game Manager and Rigidbody
    public GameManager m_GameManager;
    private Rigidbody m_Rigidbody;

    // Moving up, down, left and right
    private float m_VerticalInputMovement;
    private float m_HorizontalInputMovement;

    private float m_ReferenceThing;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_VerticalInputMovement = Input.GetAxis("Vertical");
        m_HorizontalInputMovement = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if(m_GameManager.State != GameManager.GameState.Playing)
        {
            Move();
            Turn();
        }
    }

    private void Move()
    {
        Vector3 horizontalmovement = transform.right * m_HorizontalInputMovement * m_Speed * Time.deltaTime;
        Vector3 verticalmovement = transform.forward * m_VerticalInputMovement * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + horizontalmovement + verticalmovement);
    }

    private void Turn()
    {
        // Create the Smooth Damp to turn
        float turnRotation = Mathf.SmoothDampAngle(transform.rotation.z, -1 * m_RotationTarget * m_HorizontalInputMovement * Time.deltaTime, ref m_ReferenceThing, m_TurnSpeed);

        Debug.Log("turnRotation: " + turnRotation);
        Debug.Log("Transform Rotation: " + transform.rotation.z);

        // Rotate the Rigidbody by the Euler angle provided
        m_Rigidbody.MoveRotation(Quaternion.Euler(0, 0, turnRotation));
    }
}
