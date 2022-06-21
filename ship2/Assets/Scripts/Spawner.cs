using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public Transform PlayerShip;
    public GameObject[] EnemyPrefabs;
    public GameObject EnemyParent;
    public List<GameObject> Spawners = new List<GameObject>();
    private int shipIncrease = 1;
    private float SpawnTime=4f;
    private int shipSpawnIndex=1;
    void Start()
    {

    }
    void Update()
    {
        if(EnemyParent.transform.childCount>0) 
        {
            for (int i = 0; i < EnemyParent.transform.childCount; i++)
            {
                GameObject shipHealth = EnemyParent.transform.GetChild(i).gameObject;
                shipHealth.transform.GetChild(3).LookAt(Camera.main.transform); // SHIP HEALTH SPRITE
            }
        }
        if (EnemyParent.transform.childCount<5) // SPAWN MAX 5 ENEMY SHIPS FOR EVERY 5 SECONDS.
        {
            if (SpawnTime <= 0)
            {
                spawning();
                SpawnTime = 5f;
            }                               // SPAWN COOLDOWN
        }
        
        SpawnTime -= Time.deltaTime;
    }

    void spawning()
    {
        for (int i = 0; i < shipSpawnIndex; i++)
        {
            spawnEnemies();
        }
    }
    void spawnEnemies()
    {
        int ship = Random.Range(0, shipIncrease);
        int randomSpawner = Random.Range(0, Spawners.Count);   // THERE IS SPAWN POINTS IN THE MAP -- this may change to randomly spawning --
        GameObject shipSpawn = Instantiate(EnemyPrefabs[ship], Spawners[randomSpawner].transform.position, Quaternion.identity, EnemyParent.transform);
        shipSpawn.name = EnemyPrefabs[ship].name;
    }
}
