using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public GameObject Chest;
    public int spawnDist;
    GameObject treasure;
    bool spawnIt = true;

    private void FixedUpdate()
    {
        if (spawnIt) // SPAWN IT ONCE
        {
            spawnTreasure();
            spawnIt = false;
        }
    }
    private void Update()
    {
        if (!spawnIt)
        {
            if (Physics.CheckSphere(treasure.transform.position, 10f)) // IF THE TREASURE CHEST GOT INTO SOMETHING EXCEPT LAYER, RESPAWN IT
            {
                Debug.Log("obje içerisinde");
                Destroy(treasure);
                spawnIt = true;
            }
        }

    }
    void spawnTreasure() // SPAWN THE TREASURE CHEST 
    {
        treasure = Instantiate(Chest, new Vector3(Random.Range(-spawnDist, spawnDist), 0.5f, Random.Range(-spawnDist, spawnDist)), Quaternion.identity);

    }
}
