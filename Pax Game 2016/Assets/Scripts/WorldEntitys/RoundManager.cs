﻿using UnityEngine;
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
    public float spawnFactor;
    private int maxEnemiesPerFrame = 5;

    private int round;
    private int spawnableEnemies()
    {
        return round * 10;
    }
    private int activeEnemies;
    private int spawnedEnemies;

    private bool roundStarted = true;
    private bool canSpawn = false;
    
    private void Start()
    {
        ManageRound();
    }

    private void ManageRound()
    {
        if (roundStarted)
        {
            spawnedEnemies = 0;
            round++;
            canSpawn = true;
            roundStarted = false;
        }

        if (canSpawn)
        {
            StartCoroutine( SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        int enemiesThisFrame = 0;
        
        for (int enemyIndex = 0; enemyIndex < 3; enemyIndex++)
        {
            for (int enemyCount = enemies[round].melee; enemyCount > 0; enemyCount--)
            {

                Vector3 location;
                if (enemyIndex == 3)
                {
                    location = bossSpawner.position;
                }
                else
                {
                    location = spawnPositions[Random.Range(0, spawnPositions.Count)].position;
                }


                if (spawn(ref enemiesThisFrame, enemyList[enemyIndex],location))
                {
                    yield return new WaitForSeconds(spawnRate);
                }
                spawnedEnemies++;
                activeEnemies++;
            }
        }
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