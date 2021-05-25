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

    private bool m_enemiesAlive(GameObject[] form, GameObject spawner)
    {
        int enemiesLeft = form.Length;

        for (int i = 0; i < form.Length; i++)
        {
            // Check how many enemies are left
            if (spawner.GetComponent<EnemyCollisions>().isOccupied == false)
            {
                enemiesLeft--;
            }
        }

        // Change game stage if there are no more enemies
        if (enemiesLeft <= 0)
        {
            return false;
        }

        return true;
    }

    public bool EnemiesAlive;

    // Start is called before the first frame update
    void Start()
    {
        m_GameManager = GetComponent<GameManager>();
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
            if (g.GetComponent<EnemyCollisions>().isOccupied == false && m_GameManager.ChangingStage == false)
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
            else
            {
                if(m_enemiesAlive(formationNumber, g) == false)
                {
                    EnemiesAlive = false;
                }
                else
                {
                    EnemiesAlive = true;
                }
            }
        }
    }

    // Move the enemy to the spawner
    IEnumerator MoveEnemyForward(GameObject enemy, GameObject spawner)
    {
        while(!Mathf.Approximately(enemy.transform.position.z, spawner.transform.position.z))
        {
            // Slowly move the enemy towards the spawn location
            enemy.transform.position = Vector3.SmoothDamp(enemy.transform.position, spawner.transform.position, ref m_MoveVelocity, Time.deltaTime * 100f);

            // Check if the enemy is really close to the spawner
            if (m_MoveVelocity.z <= 0.001 && enemy.transform.position.z <= 66)
            {
                break;
            }
            // Wait for each frame to move again - This will enable the movements to be smooth, since this is a while loop
            yield return new WaitForEndOfFrame();
        }
        // End the coroutine
        yield return null;
    }
}
