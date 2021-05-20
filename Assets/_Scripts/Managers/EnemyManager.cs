using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Reference other scripts
    private GameManager m_GameManager;

    // Put each formation into an array
    public GameObject[] FirstForm;
    public GameObject[] SecondForm;
    public GameObject[] ThirdForm;
    public GameObject[] FourthForm;
    public GameObject[] FifthForm;

    private Vector3 m_MoveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        m_GameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeForms()
    {
        switch (m_GameManager.Stage)
        {
            case GameManager.GameStage.FirstStage:
                SpawnEnemy(FirstForm);
                break;
            case GameManager.GameStage.SecondStage:
                SpawnEnemy(SecondForm);
                break;
            case GameManager.GameStage.ThirdStage:
                SpawnEnemy(ThirdForm);
                break;
            case GameManager.GameStage.FourthStage:
                SpawnEnemy(FourthForm);
                break;
            case GameManager.GameStage.Fifthstage:
                SpawnEnemy(FifthForm);
                break;
        }
    }

    // Move the enemies towards the spawn location
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

                // Move the enemy to the position of the spawnpoint
                StartCoroutine(MoveEnemyForward(newEnemy, g));

                // Change the script bool to true
                g.GetComponent<EnemyCollisions>().isOccupied = true;
            }
        }
    }

    // Move the enemy to the spawner
    IEnumerator MoveEnemyForward(GameObject enemy, GameObject spawner)
    {
        Debug.LogWarning("Inside the coroutine");

        while (enemy.transform.position != spawner.transform.position)
        {
            Debug.LogWarning("Supposedly Moving");
            enemy.transform.position = Vector3.SmoothDamp(enemy.transform.position, new Vector3(enemy.transform.position.x, enemy.transform.position.y, spawner.transform.position.z), ref m_MoveVelocity, Time.deltaTime * 0.2f);
            yield return new WaitForSeconds(5f);
        }
        
        yield break;
    }
}
