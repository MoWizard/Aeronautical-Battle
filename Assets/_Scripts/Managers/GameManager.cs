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

    // Reference Enemy Types
    public GameObject m_Caster;
    public GameObject m_Siege;
    public GameObject m_Super;

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

                // Reduce the players fuel
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
                    NextStage();
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

    // Create timers for when each stage will go by
    /*
    WHAT TO CHANGE:
     - Instead of set times, use relative times (nextStageTimer -= Time.time)
     - Once timer is up, don't spawn anymore enemies
     - Change game stage and set timer
     - Repeat
     */
    public void NextStage()
    {
        if(m_gameTime < 20)
        {
            Debug.Log("First Stage");
            m_GameStage = GameStage.FirstStage;
        }
        else if (m_gameTime >= 20 && m_gameTime < 50)
        {
            if(GameObject.FindGameObjectsWithTag("Caster") == null || GameObject.FindGameObjectsWithTag("Siege") == null || GameObject.FindGameObjectsWithTag("Super") == null)
            {
                Debug.Log("Second Stage");
                m_GameStage = GameStage.SecondStage;
            }
        }
        else if (m_gameTime >= 50 && m_gameTime < 80)
        {
            if (GameObject.FindGameObjectsWithTag("Caster") == null || GameObject.FindGameObjectsWithTag("Siege") == null || GameObject.FindGameObjectsWithTag("Super") == null)
            {
                Debug.Log("Third Stage");
                m_GameStage = GameStage.ThirdStage;
            }
        }
        else if (m_gameTime >= 80 && m_gameTime < 120)
        {
            if (GameObject.FindGameObjectsWithTag("Caster") == null || GameObject.FindGameObjectsWithTag("Siege") == null || GameObject.FindGameObjectsWithTag("Super") == null)
            {
                Debug.Log("Fourth Stage");
                m_GameStage = GameStage.FourthStage;
            }
        }
        else if (m_gameTime >= 120 && m_gameTime < 200)
        {
            if (GameObject.FindGameObjectsWithTag("Caster") == null || GameObject.FindGameObjectsWithTag("Siege") == null || GameObject.FindGameObjectsWithTag("Super") == null)
            {
                Debug.Log("Fifth Stage");
                m_GameStage = GameStage.Fifthstage;
            }
        }
    }

    // Move the enemies towards the spawn location
    public void MoveToPosition()
    {
        switch (m_GameStage)
        {
            case GameStage.FirstStage:
                SpawnEnemy(FirstForm);
                break;
            case GameStage.SecondStage:
                SpawnEnemy(SecondForm);
                break;
            case GameStage.ThirdStage:
                SpawnEnemy(ThirdForm);
                break;
            case GameStage.FourthStage:
                SpawnEnemy(FourthForm);
                break;
            case GameStage.Fifthstage:
                SpawnEnemy(FifthForm);
                break;
        }   
    }

    public void SpawnEnemy(GameObject[] formationNumber)
    {
        foreach (GameObject g in formationNumber)
        {
            if (g.GetComponent<EnemyCollisions>().isOccupied == false)
            {
                // Create a new enemy 20 units in the z direction away from the spawn location.
                GameObject newEnemy = Instantiate(g.GetComponent<EnemyCollisions>().enemyType, new Vector3(g.transform.position.x, g.transform.position.y, g.transform.position.z + 20f), new Quaternion(0f, 180f, 0f, 0f));

                // Make new enemy the enemy on spawn
                g.GetComponent<EnemyCollisions>().enemyOnSpawn = newEnemy;

                bool isDone = false;

                // Move the enemy to the position of the spawnpoint
                while (isDone == false)
                {
                    newEnemy.transform.position = Vector3.MoveTowards(newEnemy.transform.position, new Vector3(newEnemy.transform.position.x, newEnemy.transform.position.y, g.transform.position.z), 0.5f);
                    if (newEnemy.transform.position == g.transform.position)
                    {
                        isDone = true;
                    }
                }

                // Change the script bool to true
                g.GetComponent<EnemyCollisions>().isOccupied = true;
            }
        }
    }
}
