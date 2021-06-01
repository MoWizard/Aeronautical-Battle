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

    // HUD
    public TextMeshProUGUI m_StageTitleText;
    public TextMeshProUGUI m_StageMessageText;

    // Game Over
    public TextMeshProUGUI m_StageReachedText;

    // Reference the buttons in order to do button-y things
    public Button m_StartButton;
    public Button m_QuitButton;
    public Button m_TryAgainButton;
    public Button m_MainMenuButton;

    // Reference the Panels
    public GameObject m_HUD;
    public GameObject m_MainMenuPanel;
    public GameObject m_GameOverPanel;

    // Reference the player
    public GameObject m_player;
    private PlayerFuel m_PlayerFuel;

    // Create Timers
    public float m_gameTime = 0;
    private float m_startTimer = 3;
    private float m_nextStageTimer = 0;

    public float GameTime { get { return m_gameTime; } }

    // Reference all the enemies in the scene
    private GameObject[] m_CasterArray;
    private GameObject[] m_SiegeArray;
    private GameObject[] m_SuperArray;

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

    public bool ChangingStage = false;

    // Start is called before the first frame update
    private void Start()
    {
        m_EnemyManager = GetComponent<EnemyManager>();

        m_PlayerFuel = m_player.GetComponent<PlayerFuel>();

        m_HUD.gameObject.SetActive(false);
        m_MainMenuPanel.gameObject.SetActive(true);
        m_GameOverPanel.gameObject.SetActive(false);
    }

    public void OnNewGame()
    {
        // Set the Game State to ReadyTransition
        m_GameState = GameState.ReadyTransition;
    }

    // Update is called once per frame
    void Update()
    {
        // Place all the enemies into their arrays
        m_CasterArray = GameObject.FindGameObjectsWithTag("Caster");
        m_SiegeArray = GameObject.FindGameObjectsWithTag("Siege");
        m_SuperArray = GameObject.FindGameObjectsWithTag("Super");

        // Change what is being done according to the state of the game
        switch (m_GameState)
        {
            case GameState.StartScreen:
                m_HUD.gameObject.SetActive(false);
                m_MainMenuPanel.gameObject.SetActive(true);
                m_GameOverPanel.gameObject.SetActive(false);

                // Remove any enemies from view
                if (m_CasterArray != null || m_SiegeArray != null || m_SuperArray != null)
                {
                    for(int i = 0; i < m_CasterArray.Length; i++)
                    {
                        m_CasterArray[i].SetActive(false);
                    }
                    for (int i = 0; i < m_SiegeArray.Length; i++)
                    {
                        m_SiegeArray[i].SetActive(false);
                    }
                    for (int i = 0; i < m_SuperArray.Length; i++)
                    {
                        m_SuperArray[i].SetActive(false);
                    }
                }
                break;

            case GameState.MenuScreen:
                // Can add extra menus here if needed
                break;

            case GameState.ReadyTransition:
                m_HUD.gameObject.SetActive(true);
                m_MainMenuPanel.gameObject.SetActive(false);
                m_GameOverPanel.gameObject.SetActive(false);

                m_startTimer -= Time.deltaTime;

                // Write the get ready message
                m_StageMessageText.text = "Get Ready: " + (int)(m_startTimer + 1);

                if (Input.GetKeyUp(KeyCode.Return) == true || m_startTimer < 0)
                {
                    // reset all the variables
                    m_gameTime = 0;
                    m_startTimer = 3;
                    m_StageMessageText.text = "";
                    m_GameState = GameState.Playing;
                    m_GameStage = GameStage.FirstStage;
                    m_nextStageTimer = 20f;
                }
                break;

            case GameState.Playing:
                // Choose the visual interface
                m_HUD.gameObject.SetActive(true);
                m_MainMenuPanel.gameObject.SetActive(false);
                m_GameOverPanel.gameObject.SetActive(false);

                // Change the stage number accordingly
                m_StageTitleText.text = "Stage " + ((int)m_GameStage + 1);

                // Reduce the players fuel
                if (m_PlayerFuel.reduceFuel == false)
                {
                    StartCoroutine(m_PlayerFuel.DecreaseFuel());
                    m_PlayerFuel.reduceFuel = true;
                }
                if (m_player.activeSelf != true)
                {
                    m_GameState = GameState.GameOver;
                    m_PlayerFuel.reduceFuel = false;
                }
                else
                {
                    m_gameTime += Time.deltaTime;
                    NextStage();
                    m_EnemyManager.ChangeForms();
                }
                break;

            case GameState.GameOver:
                m_HUD.gameObject.SetActive(false);
                m_MainMenuPanel.gameObject.SetActive(false);
                m_GameOverPanel.gameObject.SetActive(true);

                // Write the game over message
                m_StageReachedText.text = "Stage Reached: " + ((int)m_GameStage + 1) + "/5";
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
                OnQuitGame();
            }
        }
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }

    public void OnMainMenuButton()
    {
        m_GameState = GameState.StartScreen;
    }

    // Create timers for when each stage will go by
    public void NextStage()
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
                        m_nextStageTimer = 120f;
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
                        m_GameState = GameState.GameOver;
                    }
                }
                break;
        }
    }
}
