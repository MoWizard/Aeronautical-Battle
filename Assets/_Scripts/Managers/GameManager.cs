using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Reference the player
    public GameObject m_player;
    public PlayerFuel m_PlayerFuel;

    // Create Timer
    public float m_gameTime = 0;
    public float GameTime { get { return m_gameTime; } }

    // Check if the Enemy Spawn is occupied or not
    //public EnemyCollisions m_EnemyCollisions;

    // Reference Enemy Types
    public GameObject m_Caster;
    public GameObject m_Siege;
    public GameObject m_Super;

    // Reference each formation
    //public GameObject Formation1;
    //public GameObject Formation2;
    //public GameObject Formation3;
    //public GameObject Formation4;
    //public GameObject Formation5;

    // Put each formation into an array
    public GameObject[] FirstForm;
    public GameObject[] SecondForm;
    public GameObject[] ThirdForm;
    public GameObject[] FourthForm;
    public GameObject[] FifthForm;

    // Reference all the enemies in the scene
    public GameObject[] m_CasterArray;
    public GameObject[] m_SiegeArray;
    public GameObject[] m_SuperArray;

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
        m_PlayerFuel = m_player.GetComponent<PlayerFuel>();

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
                //Debug.Log(m_gameTime);
                if(m_PlayerFuel.reduceFuel == false)
                {
                    StartCoroutine(m_PlayerFuel.DecreaseFuel());
                    m_PlayerFuel.reduceFuel = true;
                }
                if (m_player.activeSelf != true)
                {
                    m_GameState = GameState.GameOver;
                }
                else
                {
                    m_gameTime += Time.deltaTime;
                    MoveToPosition();
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

    // Move the enemies towards the spawn location
    public void MoveToPosition()
    {
        /*
        switch (m_GameStage)
        {
            case GameStage.FirstStage:
                for (int i = 0; i < FirstForm.Length; i++)
                {
                    Debug.LogWarning(m_EnemyCollisions.isOccupied);
                    if (m_EnemyCollisions.isOccupied == false)
                    {
                        // Create a new enemy 20 units in the z direction away from the spawn location.
                        GameObject newEnemy = Instantiate(m_EnemyCollisions.enemyType, new Vector3(m_EnemyCollisions.self.position.x, m_EnemyCollisions.self.position.y, m_EnemyCollisions.self.position.z - 20), new Quaternion(0f, 180f, 0f, 0f));

                        // Move the enemy to the position of the spawnpoint
                        newEnemy.transform.position = Vector3.Lerp(newEnemy.transform.position, new Vector3(newEnemy.transform.position.x, newEnemy.transform.position.y, m_EnemyCollisions.self.position.z), 1);
                    }
                }
                break;
        }
        */
    }
}
