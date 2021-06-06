using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Reference other scripts
    private EnemyManager m_EnemyManager;

    // Reference the overlay text to display some cool text
    // Main Menu
    public TextMeshProUGUI m_TitleText;

    // Credits
    public Button m_BackButton;

    // HUD
    public TextMeshProUGUI m_StageTitleText;
    public TextMeshProUGUI m_StageMessageText;

    // Game Over
    public TextMeshProUGUI m_GameOverText;
    public TextMeshProUGUI m_StageReachedText;

    // Reference the Panels
    public GameObject m_HUD;
    public GameObject m_MainMenuPanel;
    public GameObject m_GameOverPanel;
    public GameObject m_PausePanel;
    public GameObject m_CreditsPanel;

    // Reference the player
    public GameObject m_player;
    private PlayerFuel m_PlayerFuel;

    // Create Timers
    public float m_gameTime = 0;
    private float m_startTimer = 3;
    private float m_nextStageTimer = 0;

    // Toggle pause
    private bool PauseToggle = false;


    public float GameTime { get { return m_gameTime; } }

    // Reference all the enemies in the scene
    private GameObject[] m_CasterArray;
    private GameObject[] m_SiegeArray;
    private GameObject[] m_SuperArray;
    private GameObject[] m_BulletArray;
    private GameObject[] m_HomingArray;

    // Assigning names to integers though enumerations. StartScreen = 0, SplashScreen = 1, Start = 2, Playing = 3 and GameOver = 4
    public enum GameState
    {
        StartScreen,
        MenuScreen,
        ReadyTransition,
        Playing,
        Paused,
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
        Fifthstage,
        Win
    };

    // Add ways to reference the Game State across scripts
    private GameStage m_GameStage;
    public GameStage Stage { get { return m_GameStage; } }

    public bool ChangingStage = false;

    // Start is called before the first frame update
    private void Start()
    {
        m_EnemyManager = GetComponent<EnemyManager>();

        m_PlayerFuel = m_player.GetComponent<PlayerFuel>();

        m_HUD.SetActive(false);
        m_MainMenuPanel.SetActive(true);
        m_GameOverPanel.SetActive(false);
        m_CreditsPanel.SetActive(false);
        m_PausePanel.SetActive(false);
    }

    // Starts a new game
    public void OnNewGame()
    {
        // Make sure the game isn't paused and fix it if it is
        m_PausePanel.SetActive(false);
        Time.timeScale = 1;
        PauseToggle = false;
        PauseToggle = false;
        m_PlayerFuel.reduceFuel = false;
        StopCoroutine(m_PlayerFuel.DecreaseFuel());
        m_PlayerFuel.IncreaseFuel(100);

        // Set player to active and removes enemies
        RemoveEnemies();
        m_player.SetActive(true);

        // Set the Game State to ReadyTransition
        m_GameState = GameState.ReadyTransition;
    }

    // Update is called once per frame
    void Update()
    {
        // Secret developer tool
        if (Input.GetKeyDown(KeyCode.F8))
        {
            RemoveEnemies();
            m_GameStage = GameStage.Fifthstage;
            m_nextStageTimer = 80f;
            ChangingStage = false;
            m_EnemyManager.m_FirstSuper = true;
        }

        if (m_GameState == GameState.Playing && Input.GetKeyDown(KeyCode.Tab) && PauseToggle == false)
        {
            OnPauseGame();
        }


        // Place all the enemies into their arrays
        m_CasterArray = GameObject.FindGameObjectsWithTag("Caster");
        m_SiegeArray = GameObject.FindGameObjectsWithTag("Siege");
        m_SuperArray = GameObject.FindGameObjectsWithTag("Super");
        m_BulletArray = GameObject.FindGameObjectsWithTag("Bullet");
        m_HomingArray = GameObject.FindGameObjectsWithTag("Homing");

        // Change what is being done according to the state of the game
        switch (m_GameState)
        {
            case GameState.StartScreen:
                m_HUD.SetActive(false);
                m_MainMenuPanel.SetActive(true);
                m_GameOverPanel.SetActive(false);

                // Remove any enemies from view and brings the player back
                RemoveEnemies();
                m_player.SetActive(true);
                break;

            case GameState.MenuScreen:

                break;

            case GameState.ReadyTransition:
                m_HUD.SetActive(true);
                m_MainMenuPanel.SetActive(false);
                m_GameOverPanel.SetActive(false);

                m_startTimer -= Time.deltaTime;

                // Write the get ready message
                m_StageMessageText.text = "Get Ready: " + (int)(m_startTimer + 1);

                if (Input.GetKeyUp(KeyCode.Return) == true || m_startTimer < 0)
                {
                    // reset all the variables
                    m_gameTime = 0;
                    m_startTimer = 3;
                    m_StageMessageText.text = "";

                    // Change the states of the game
                    m_GameState = GameState.Playing;
                    m_GameStage = GameStage.FirstStage;

                    // Set the first stage to 20 seconds and makes sure it's starting a new stage
                    m_nextStageTimer = 20f;
                    ChangingStage = false;

                    // Reduce the players fuel
                    m_PlayerFuel.reduceFuel = true;
                    StartCoroutine(m_PlayerFuel.DecreaseFuel());
                }
                break;

            case GameState.Playing:
                // Choose the visual interface
                m_HUD.SetActive(true);
                m_MainMenuPanel.SetActive(false);
                m_GameOverPanel.SetActive(false);

                // Change the stage number accordingly
                m_StageTitleText.text = "Stage " + ((int)m_GameStage + 1);
                if (m_player.activeSelf == false)
                {
                    // Sets everything for game over
                    m_GameOverText.text = "Game Over";
                    m_GameState = GameState.GameOver;

                    // Resets the PlayerFuel script
                    m_PlayerFuel.reduceFuel = false;
                    StopCoroutine(m_PlayerFuel.DecreaseFuel());
                    m_PlayerFuel.IncreaseFuel(100);
                }
                else
                {
                    m_gameTime += Time.deltaTime;
                    CheckStage();
                    m_EnemyManager.CheckForms();
                }
                break;

            case GameState.Paused:
                if (Input.GetKeyDown(KeyCode.Tab) && PauseToggle == true)
                {
                    PauseToggle = false;
                    OnContinueGame();
                }
                else
                {
                    PauseToggle = true;
                }
                break;

            case GameState.GameOver:
                m_HUD.SetActive(false);
                m_MainMenuPanel.SetActive(false);
                m_GameOverPanel.SetActive(true);

                // Write the game over message
                m_StageReachedText.text = "Stages Completed: " + ((int)m_GameStage) + "/5";
                break;
        }

        // Check if the player wants to quit
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            switch (m_GameState)
            {
                case GameState.MenuScreen:
                    m_GameState = GameState.StartScreen;
                    break;

                case GameState.Paused:
                    if (PauseToggle == true)
                    {
                        PauseToggle = false;
                        OnContinueGame();
                    }
                    else
                    {
                        PauseToggle = true;
                    }
                    break;

                case GameState.Playing:
                    if (PauseToggle == false)
                    {
                        PauseToggle = true;
                        OnPauseGame();
                    }
                    else
                    {
                        PauseToggle = false;
                    }
                    
                    break;

                default:
                    OnQuitGame();
                    break;
            }
        }
    }

    // When the player pauses the game
    public void OnPauseGame()
    {
        m_GameState = GameState.Paused;
        m_PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void OnContinueGame()
    {
        PauseToggle = false;
        m_GameState = GameState.Playing;
        m_PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    // Quit the game
    public void OnQuitGame()
    {
        Application.Quit();
    }

    // Go to start screen when the Main Menu button is pressed
    public void OnMainMenuButton()
    {
        m_PausePanel.SetActive(false);
        Time.timeScale = 1;
        m_GameState = GameState.StartScreen;
    }

    // Open the credits
    public void OnCreditsButton()
    {
        m_GameState = GameState.MenuScreen;
        m_MainMenuPanel.SetActive(false);
        m_CreditsPanel.SetActive(true);
    }

    public void OnBackButton()
    {
        m_GameState = GameState.StartScreen;
        m_CreditsPanel.SetActive(false);
        m_MainMenuPanel.SetActive(true);
    }

    // Deletes all the enemies on screen when a new game is starting
    public void RemoveEnemies()
    {
        foreach(GameObject i in m_CasterArray)
        {
            Destroy(i);
        }
        foreach (GameObject i in m_SiegeArray)
        {
            Destroy(i);
        }
        foreach (GameObject i in m_SuperArray)
        {
            Destroy(i);
        }
        foreach (GameObject i in m_BulletArray)
        {
            Destroy(i);
        }
        foreach (GameObject i in m_HomingArray)
        {
            Destroy(i);
        }
    }

    // Create timers for when each stage will go by
    public void CheckStage()
    {
        // Change the stages according to the time and the enemies alive
        switch (m_GameStage)
        {
            case GameStage.FirstStage:
                // Wait for x amount of seconds before moving onto the next stage
                if (m_nextStageTimer > 0)
                {
                    m_nextStageTimer -= Time.deltaTime;
                }

                // Once the timer hits 0 we can wait for the player to clear all the enemies and move onto the next stage
                if (m_nextStageTimer <= 0)
                {
                    ChangingStage = true;
                    if (m_EnemyManager.m_enemiesAlive == false)
                    {
                        m_GameStage = GameStage.SecondStage;
                        m_nextStageTimer = 40f;
                        ChangingStage = false;
                        m_EnemyManager.m_FirstSuper = true;
                    }
                }
                break;
            
            case GameStage.SecondStage:
                // Wait for x amount of seconds before moving onto the next stage
                if (m_nextStageTimer > 0)
                {
                    m_nextStageTimer -= Time.deltaTime;
                }

                // Once the timer hits 0 we can wait for the player to clear all the enemies and move onto the next stage
                if (m_nextStageTimer <= 0)
                {
                    ChangingStage = true;
                    if (m_EnemyManager.m_enemiesAlive == false)
                    {
                        m_GameStage = GameStage.ThirdStage;
                        m_nextStageTimer = 60f;
                        ChangingStage = false;
                        m_EnemyManager.m_FirstSuper = true;
                    }
                }
                break;
            
            case GameStage.ThirdStage:
                // Wait for x amount of seconds before moving onto the next stage
                if (m_nextStageTimer > 0)
                {
                    m_nextStageTimer -= Time.deltaTime;
                }

                // Once the timer hits 0 we can wait for the player to clear all the enemies and move onto the next stage
                if (m_nextStageTimer <= 0)
                {
                    ChangingStage = true;
                    if (m_EnemyManager.m_enemiesAlive == false)
                    {
                        m_GameStage = GameStage.FourthStage;
                        m_nextStageTimer = 30f;
                        ChangingStage = false;
                        m_EnemyManager.m_FirstSuper = true;
                    }
                }
                break;
            
            case GameStage.FourthStage:
                // Wait for x amount of seconds before moving onto the next stage
                if (m_nextStageTimer > 0)
                {
                    m_nextStageTimer -= Time.deltaTime;
                }

                // Once the timer hits 0 we can wait for the player to clear all the enemies and move onto the next stage
                if (m_nextStageTimer <= 0)
                {
                    ChangingStage = true;
                    if (m_EnemyManager.m_enemiesAlive == false)
                    {
                        m_GameStage = GameStage.Fifthstage;
                        m_nextStageTimer = 80f;
                        ChangingStage = false;
                        m_EnemyManager.m_FirstSuper = true;
                    }
                }
                break;
            
            case GameStage.Fifthstage:
                // Wait for x amount of seconds before moving onto the next stage
                if (m_nextStageTimer > 0)
                {
                    m_nextStageTimer -= Time.deltaTime;
                }

                // Once the timer hits 0 we can wait for the player to clear all the enemies and finish the game
                if (m_nextStageTimer <= 0)
                {
                    ChangingStage = true;
                    if (m_EnemyManager.m_enemiesAlive == false)
                    {
                        m_GameOverText.text = "You Win";
                        m_GameStage = GameStage.Win;
                        m_GameState = GameState.GameOver;
                        ChangingStage = false;
                    }
                }
                break;
        }
    }
}
