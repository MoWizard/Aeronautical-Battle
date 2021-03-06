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
    public bool m_FirstSuper = true;

    private bool EnemiesAlive(GameObject[] form)
    {
        int enemiesLeft = form.Length;

        for (int i = 0; i < form.Length; i++)
        {
            // Check how many enemies are left
            if (form[i].GetComponent<EnemySpawnerCollisions>().isOccupied == false)
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

    public bool m_enemiesAlive;

    // Start is called before the first frame update
    void Start()
    {
        m_GameManager = GetComponent<GameManager>();
    }

    /* Change the formation type each stage                                *
     * Firstly it enables the form that is required and disables the rest. *
     * It then spawns in the enemies for that specific form.               */
    public void CheckForms()
    {
        switch (m_GameManager.Stage)
        {
            case GameManager.GameStage.FirstStage:
                EnableForm(FirstForm);
                DisableForm(SecondForm);
                DisableForm(ThirdForm);
                DisableForm(FourthForm);
                DisableForm(FifthForm);

                SpawnEnemy(FirstForm);
                break;
            case GameManager.GameStage.SecondStage:
                DisableForm(FirstForm);
                EnableForm(SecondForm);
                DisableForm(ThirdForm);
                DisableForm(FourthForm);
                DisableForm(FifthForm);

                SpawnEnemy(SecondForm);
                break;
            case GameManager.GameStage.ThirdStage:
                DisableForm(FirstForm);
                DisableForm(SecondForm);
                EnableForm(ThirdForm);
                DisableForm(FourthForm);
                DisableForm(FifthForm);

                SpawnEnemy(ThirdForm);
                break;
            case GameManager.GameStage.FourthStage:
                DisableForm(FirstForm);
                DisableForm(SecondForm);
                DisableForm(ThirdForm);
                EnableForm(FourthForm);
                DisableForm(FifthForm);

                SpawnEnemy(FourthForm);
                break;
            case GameManager.GameStage.Fifthstage:
                DisableForm(FirstForm);
                DisableForm(SecondForm);
                DisableForm(ThirdForm);
                DisableForm(FourthForm);
                EnableForm(FifthForm);

                SpawnEnemy(FifthForm);
                break;
        }
    }

    // Move the enemies towards the spawn location
    public void SpawnEnemy(GameObject[] formationNumber)
    {
        foreach (GameObject g in formationNumber)
        {
            if (g.GetComponent<EnemySpawnerCollisions>().isOccupied == false && m_GameManager.ChangingStage == false)
            {
                // Create a new enemy 20 units in the z direction away from the spawn location.
                GameObject newEnemy = Instantiate(g.GetComponent<EnemySpawnerCollisions>().enemyType, new Vector3(g.transform.position.x, g.transform.position.y, g.transform.position.z + 20f), new Quaternion(0f, 180f, 0f, 0f));

                // Make new enemy the enemy on spawn
                g.GetComponent<EnemySpawnerCollisions>().enemyOnSpawn = newEnemy;

                // Set the immunity status of the enemy
                newEnemy.GetComponent<EnemyImmunity>().Immune = true;

                // Move the enemy to the position of the spawnpoint
                StartCoroutine(MoveEnemyForward(newEnemy, newEnemy.transform.position.z));

                // Change the script bool to true
                g.GetComponent<EnemySpawnerCollisions>().isOccupied = true;
            }
            else
            {
                if (EnemiesAlive(formationNumber) == false)
                {
                    m_enemiesAlive = false;
                }
                else
                {
                    m_enemiesAlive = true;
                }
            }
        }
    }

    // Move the enemy to the spawner
    public IEnumerator MoveEnemyForward(GameObject enemy, float enemyDistance)
    {
        // Check if this is the first Super enemy
        if (enemy.CompareTag("Super") && m_FirstSuper == false)
        {
            yield return new WaitForSeconds(2f);
        }
        else if (enemy.CompareTag("Super") && m_FirstSuper == true)
        {
            m_FirstSuper = false;
        }

        // Check if they aren't in the game anymore
        if (m_GameManager.State != GameManager.GameState.Playing)
        {
            yield return null;
        }

        while (enemy != null)
        {
            if (!(m_MoveVelocity.z >= -3f && enemy.transform.position.z <= 70))
            {
                // Slowly move the enemy towards the spawn location
                enemy.transform.position = Vector3.SmoothDamp(enemy.transform.position, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemyDistance - 20f), ref m_MoveVelocity, Time.deltaTime * 30f);

                // Wait for each frame to move again - This will enable the movements to be smooth
                yield return new WaitForEndOfFrame();
            }
            else
            {
                break;
            }
        }

        // While the enemy is not on the spawner, move it to the spawner
        //while (!(m_MoveVelocity.z >= -3f && enemy.transform.position.z <= 70)) 
        //{
        //    // Slowly move the enemy towards the spawn location
        //    enemy.transform.position = Vector3.SmoothDamp(enemy.transform.position, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemyDistance - 20f), ref m_MoveVelocity, Time.deltaTime * 30f);

        //    // Wait for each frame to move again - This will enable the movements to be smooth
        //    yield return new WaitForEndOfFrame();
        //}

        // Allow the enemy to be hit again
        enemy.GetComponent<EnemyImmunity>().Immune = false;

        // End the coroutine
        yield return null;
    }

    // Disable the formation gameobjects
    void DisableForm(GameObject[] form)
    {
        for (int i = 0; i < form.Length; i++)
        {
            form[i].SetActive(false);
        }
    }

    // Enable the formation gameobjects
    void EnableForm(GameObject[] form)
    {
        for (int i = 0; i < form.Length; i++)
        {
            form[i].SetActive(true);
        }
    }
}
