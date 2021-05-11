using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Reference the player
    public GameObject m_player;

    private float m_gameTime = 0;

    // Reference all the enemies in the scene
    public GameObject[] m_casters;
    public GameObject[] m_sieges;
    public GameObject[] m_supers;

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

    // Assign names to integers though enumerations for the stages
    public enum GameStage
    {
        FirstStage,
        SecondStage,
        ThirdStage,
        FourthStage,
        Fifthstage
    };

    // Add ways to reference the Game State across scripts
    private GameStage m_GameStage;
    public GameStage Stage { get { return m_GameStage; } }

    // Start is called before the first frame update
    private void Start()
    {
        m_GameState = GameState.Playing;
        m_GameStage = GameStage.FirstStage;
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
                Debug.Log(m_gameTime);
                if (m_player.activeSelf != true)
                {
                    m_GameState = GameState.GameOver;
                }
                else
                {
                    m_gameTime += Time.deltaTime;
                }
                break;

            case GameState.GameOver:

                break;
        }

        // Check if the player wants to quit
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (m_GameState == GameState.MenuScreen)
            {
                m_GameState = GameState.StartScreen;
                //m_BackButton.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("GAME CLOSED");
                Application.Quit();
            }
        }
    }

    public void NextStage()
    {
        if(m_gameTime < 20)
        {
            m_GameStage = GameStage.FirstStage;
        }
        else if (m_gameTime >= 20 && m_gameTime < 50)
        {
            m_GameStage = GameStage.SecondStage;
        }
        else if (m_gameTime >= 50 && m_gameTime < 80)
        {
            m_GameStage = GameStage.ThirdStage;
        }
        else if (m_gameTime >= 80 && m_gameTime < 120)
        {
            m_GameStage = GameStage.FourthStage;
        }
        else if (m_gameTime >= 120 && m_gameTime < 200)
        {
            m_GameStage = GameStage.Fifthstage;
        }
    }
}
