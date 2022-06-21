using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    public static Attacking instance;
    private Transform target;
    private Vector3 dir;
    private float playerShipDamage = 1f;
    private void Awake()
    {
        instance = this;
    }
    private void FixedUpdate()
    {
        if (target == null)
        {
            target = PlayerAttackController.instance.target;
        }
        if (target != null)
        {
            dir = target.position - transform.position;
        }
        if (dir.magnitude <= 25f && target != null)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            transform.Translate(dir.normalized * (40f * Time.deltaTime), Space.World);
        }
        else
        {
            Destroy(gameObject);
        }

        // check the shotobj and target pos, if gets close than 2F
        if (dir.magnitude <= 2f)
        {
            HitTarget(); 
            return;
        }
    }
    void HitTarget()
    {


        if (target != null)
        {
            PlayerAttackController.instance.AttackingEnemies(target.gameObject, playerShipDamage); // damage it
            Debug.Log("attakc");
            target.gameObject.GetComponent<EnemyHealth>().HealthBar.SetActive(true); 
            Destroy(gameObject);
            Destroy(GameObject.Find("Light(Clone)"), 0.5f);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
