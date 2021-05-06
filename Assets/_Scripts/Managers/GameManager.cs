using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Reference the player
    public GameObject player;

    // Assigning names to integers though enumerations. StartScreen = 0, SplashScreen = 1, Start = 2, Playing = 3 and GameOver = 4
    public enum GameState
    {
        StartScreen,
        MenuScreen,
        ReadyTransition,
        Playing,
        GameOver
    };

    // Add ways to reference the Game State across scripts
    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }

    // Start is called before the first frame update
    private void Start()
    {
        m_GameState = GameState.StartScreen;
    }

    public void OnNewGame()
    {
        // Set the Game State to Start and the text to nothing
        m_GameState = GameState.ReadyTransition;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_GameState)
        {
            case GameState.StartScreen:

                break;

            case GameState.MenuScreen:

                break;

            case GameState.ReadyTransition:

                break;

            case GameState.Playing:
                if (player.activeSelf != true)
                {
                    m_GameState = GameState.GameOver;
                }
                break;

            case GameState.GameOver:

                break;
        }
    }

    public void NextState()
    {
        m_GameState = m_GameState++;
    }
}
