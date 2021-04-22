using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float m_DampTime = 5f;

    // Reference the Camera and Game Manager
    public Camera m_Camera;
    public GameManager m_GameManager;

    // Get the player's position
    public Transform m_Player;

    // I actually don't really know what this is for yet, but we need it
    private Vector3 m_MoveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DefaultPosition();
    }

    private void FixedUpdate()
    {
        // add the switch of each game screen
    }

    private void DefaultPosition() // MAKE SURE TO CHANGE THIS IN ORDER TO MSKE IT SCROLL INSTEAD OF FOLLOW THE PLAYER
    {
        // Zoom the camera to the game zoom and move + rotate the camera to the Boat / default position smoothly
        transform.position = Vector3.SmoothDamp(transform.position, m_Player.position, ref m_MoveVelocity, Time.deltaTime * m_DampTime);
        //transform.rotation = Quaternion.Lerp(transform.rotation, m_DefaultPosition.rotation, Time.deltaTime * m_LongerDampTime);
    }
}
