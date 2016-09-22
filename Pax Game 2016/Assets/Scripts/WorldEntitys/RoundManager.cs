using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class RoundManager : MonoBehaviour
{
    public List<Transform> spawnPositions = new List<Transform>();
    public Transform bossSpawner;
    public List<GameObject> enemyList = new List<GameObject>();
    [System.Serializable]
    public struct roundEnemies
    {
        public string name;
        public int boss;
        public int ranged;
        public int melee;
    }
    public roundEnemies[] enemies;

    public float spawnRate = 1f;
    //public float spawnFactor;
    private int maxEnemiesPerFrame = 5;
    public float roundStartDellay =1f;

    public UnityEvent onStart,onRoundEnd, onRoundBegin;

    private int round;
    private int spawnableEnemies()
    {
        return round * 10;
    }
    private int activeEnemies;
    private int spawnedEnemies;

    //private bool roundStarted = true;
    
    private void Start()
    {
        beginSpawning();
    }

    void beginSpawning()
    {
        onStart.Invoke();
        ManageRound();
    }

    private void ManageRound()
    {        

        if (activeEnemies == 0)
        {
            if (round > 0)
            {
                onRoundEnd.Invoke();
            }
            StartCoroutine(StepNextRound(roundStartDellay));
        }
    }

    IEnumerator StepNextRound(float intitalWait)
    {
        yield return new WaitForSeconds(intitalWait);    
        startNewRound();
    }

    void startNewRound()
    {     

        spawnedEnemies = 0;
        round++;
        onRoundBegin.Invoke();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        int enemiesThisFrame = 0;
        
        for (int enemyIndex = 0; enemyIndex < 3; enemyIndex++)
        {
           

            for (int enemyCount = getEnemyCount(enemyIndex); enemyCount > 0; enemyCount--)
            {

                Vector3 location;
                if (enemyIndex == 2)
                {
                    location = bossSpawner.position;
                }
                else
                {
                    location = spawnPositions[Random.Range(0, spawnPositions.Count)].position;
                }

                spawnedEnemies++;
                activeEnemies++;
                if (spawn(ref enemiesThisFrame, enemyList[enemyIndex],location))
                {
                    yield return new WaitForSeconds(spawnRate);
                }               
            }
        }
    }

    private int getEnemyCount(int enemyIndex)
    {
        int enemyCount = -1;

        if (enemyIndex == 0)
        {
            enemyCount = enemies[round].melee;
        }
        else if (enemyIndex == 1)
        {
            enemyCount = enemies[round].ranged;
        }
        else if (enemyIndex == 2)
        {
            enemyCount = enemies[round].boss;
        }

        return enemyCount;
    }

    private bool spawn(ref int enemiesThisFrame,GameObject prefab,Vector3 location)
    {
        enemiesThisFrame++;
        Instantiate(prefab,location, Quaternion.identity);
        return (enemiesThisFrame < maxEnemiesPerFrame);
    }
    
    public void OnEnemyDeath()
    {        
        activeEnemies--;
        ManageRound();
    }
}