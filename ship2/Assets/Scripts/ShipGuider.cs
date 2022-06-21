using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGuider : MonoBehaviour
{
    private GameObject nearestEnemy;
    void Update()
    {
        // SHOW THE CLOSEST ENEMY WITH SPRITE
         
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
                Vector3 dir = nearestEnemy.transform.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = lookRotation.eulerAngles;
                transform.rotation = Quaternion.Euler(90f, rotation.y, 0f);
            }
        }
    }
}
