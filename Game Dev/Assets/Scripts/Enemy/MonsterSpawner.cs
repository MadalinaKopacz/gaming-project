using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 10f;
    [SerializeField] private int maxLimitEntities = 3;
    [SerializeField] private bool continueSpawn = true;
    public Transform[] spawnPoints;
    //public GameObject[] monsters;
    public GameObject monster;
    public string monsterTag;
    int randomSpawnPoints;

    public static bool spawnAllowed;

    private int birdNumber;

    // Start is called before the first frame update
    void Start()
    {
        birdNumber = GameObject.FindGameObjectsWithTag(monsterTag).Length;
        spawnAllowed = true;
        InvokeRepeating("SpawnMonster", 0f, spawnRate);
    }

    void SpawnMonster()
    {
        birdNumber = GameObject.FindGameObjectsWithTag(monsterTag).Length;
        if (birdNumber >= maxLimitEntities)
        {
            spawnAllowed = false;
        }
        else if (!spawnAllowed && continueSpawn)
        {
            spawnAllowed = true;
        }

        if (spawnAllowed)
        {
            randomSpawnPoints = Random.Range(0, spawnPoints.Length);
            Instantiate(monster, spawnPoints[randomSpawnPoints].position, Quaternion.identity);
        }
    }
}
