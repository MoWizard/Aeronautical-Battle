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
    public TextMeshProUGUI m_TitleText;
    public TextMeshProUGUI m_StageMessageText;

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

    // Create Timer
    public float m_gameTime = 0;
    private float m_startTimer = 3;
    public float GameTime { get { return m_gameTime; } }

    // Reference Enemy Types
    public GameObject m_Caster;
    public GameObject m_Siege;
    public GameObject m_Super;

    /* Put each formation into an array
    public GameObject[] FirstForm;
    public GameObject[] SecondForm;
    public GameObject[] ThirdForm;
    public GameObject[] FourthForm;
    public GameObject[] FifthForm;*/

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

    // Create the smoothdamp effect
    private Vector3 m_MoveVelocity;
    private float m_DampTime = 0.5f;

    // Start is called before the first frame update
    private void Start()
    {
        m_EnemyManager = GetComponent<EnemyManager>();

        m_PlayerFuel = m_player.GetComponent<PlayerFuel>();

        m_HUD.gameObject.SetActive(false);
        m_MainMenuPanel.gameObject.SetActive(true);
        m_GameOverPanel.gameObject.SetActive(false);

        //m_GameState = GameState.Playing;
        //m_GameStage = GameStage.FirstStage;
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
                m_HUD.gameObject.SetActive(false);
                m_MainMenuPanel.gameObject.SetActive(true);
                m_GameOverPanel.gameObject.SetActive(false);
                break;

            case GameState.MenuScreen:

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
                }
                break;

            case GameState.Playing:
                // Choose the visual interface
                m_HUD.gameObject.SetActive(true);
                m_MainMenuPanel.gameObject.SetActive(false);
                m_GameOverPanel.gameObject.SetActive(false);

                // Reduce the players fuel
                if (m_PlayerFuel.reduceFuel == false)
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
                    m_EnemyManager.ChangeForms();
                }
                break;

            case GameState.GameOver:
                m_HUD.gameObject.SetActive(false);
                m_MainMenuPanel.gameObject.SetActive(false);
                m_GameOverPanel.gameObject.SetActive(true);

                // Write the game over message
                m_StageMessageText.text = "Game Over";
                m_StageMessageText.transform.position = Vector3.SmoothDamp(transform.position, m_TitleText.transform.position, ref m_MoveVelocity, Time.deltaTime * m_DampTime);
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
        m_HUD.gameObject.SetActive(false);
        m_MainMenuPanel.gameObject.SetActive(true);
        m_GameOverPanel.gameObject.SetActive(false);
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
}
