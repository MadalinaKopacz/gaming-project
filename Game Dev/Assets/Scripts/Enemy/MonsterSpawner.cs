using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 10f;
    [SerializeField] private int maxLimitEntities;
    [SerializeField] private bool continueSpawn = true;
    public Transform[] spawnPoints;

    public GameObject cage;

    public GameObject monster;
    public string monsterTag;
    int randomSpawnPoints;

    public static bool spawnAllowed;

    private int birdNumber;

    // Start is called before the first frame update
    void Start()
    {
        birdNumber = GameObject.FindGameObjectsWithTag(monsterTag).Length;
        //print(birdNumber);
        int nr = numberOfObjectsSpawned();
        birdNumber = nr;
        print(nr);
        spawnAllowed = true;
        InvokeRepeating("SpawnMonster", 0f, spawnRate);
    }

    void SpawnMonster()
    {
        birdNumber = GameObject.FindGameObjectsWithTag(monsterTag).Length;
        //print(birdNumber);

        int nr = numberOfObjectsSpawned();
        print(nr);
        birdNumber = nr; 

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

            var newObj = Instantiate(monster, spawnPoints[randomSpawnPoints].position, Quaternion.identity);
            //newObj.transform.parent = GameObject.Find("BirdCage1").transform;
            newObj.transform.parent = cage.transform;
        }
    }

    int numberOfObjectsSpawned()
    {
        int nr = 0;
        //GameObject cage = GameObject.Find("BirdCage1");
        Transform t = cage.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == monsterTag)
            {
                nr++;
            }
            
               
        }
        return nr;
    }

   
}
