using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shipstat;
using TMPro;

public class PlayerAttackController : MonoBehaviour
{
    public static PlayerAttackController instance;

    public GameObject LevelPassRock;

    [Space(20)]
    [Header("Attack & Target")]
    public GameObject ShotObj;
    public GameObject ShotParticle;
    public Transform target;
    GameObject nearestEnemy;
    public float range = 12f;
    public ShipStats PlayerShip;
    public GameObject hitEffect;
    public GameObject explosionEffect;
    float shortestDistance;

    [Space(20)]
    [Header("XP & Loot Text References")]
    [Space(20)]

    public float XP;
    public Text XPText;
    public Image FillingXP;
    public int maxLevelXP = 50;
    float totalXP = 0f;
    [SerializeField] Text LootedText;


    [Space(20)]
    [Header("Loot")]
    [Space(20)]


    public GameObject basicLoot;
    public GameObject basicLootIdleParticle;
    public GameObject LootPickParticle;

    int gold;
    int wood;

    bool inRange;
    public int Level=1;
    // SHIP STATS

    public float attackSpeed;
    public float attackDmg;
    public float fireRate = 3f;
    private void Awake()
    {
        attackDmg = PlayerShip.attack;        // getting stats from scriptable object
        attackSpeed = PlayerShip.attackSpeed; //
        instance = this;
    }

    private void Start()
    {
        InvokeRepeating("updateTarget", 0f, 0.5f);
        //InvokeRepeating("AttackTarget", 0f, attackSpeed);
        FillingXP.fillAmount = 0;
        XPText.text = XP.ToString() + "/" + maxLevelXP.ToString();
    }

    private void Update()
    {
        attackSpeed += Time.deltaTime; 
        //  Debug.Log(attackSpeed);
        if (attackSpeed > fireRate) // atkspeed cooldown
        {
            AttackTarget();
            attackSpeed = PlayerShip.attackSpeed; // reset

        }
        /*int layermask = 1 << 10;

        Collider[] hit = Physics.OverlapSphere(transform.position, 5f, layermask);
        foreach (var collider in hit)
        {
            GameObject temp = Instantiate(LootPickParticle, target.position, Quaternion.identity);
            Destroy(collider.gameObject, 0.5f);
            Destroy(temp, 1f);
        }*/
    }
    public void updateTarget()
    {
        // GET THE CLOSEST ENEMY TARGET AND CHECK IF ITS IN RANGE
         
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        shortestDistance = Mathf.Infinity;
        nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

            }
            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
                inRange = true;
            }
            else
            {
                target = null;
                inRange = false;
            }
        }
    }
    void AttackTarget()
    {
        if (inRange)
        {
            AttackObjSpawn(); // Instantiate arrows
        }
    }
    void AttackObjSpawn()
    {
        Instantiate(ShotObj, new Vector3(transform.position.x, ShotObj.transform.position.y, transform.position.z), Quaternion.identity);
    }
    public void AttackingEnemies(GameObject shipName, float damage)
    {
        // switch-casing w/ getting shipname from target object. 

        if (target != null)
        {
            switch (shipName.name)
            {
                case "BasicShip":
                    Debug.Log("Attacking: " + shipName.name + " health: " + shipName.GetComponent<EnemyHealth>().shipHealth);
                    shipName.GetComponent<EnemyHealth>().shipHealth -= damage;
                    if (shipName.GetComponent<EnemyHealth>().shipHealth > 0)
                    {
                        Instantiate(hitEffect.transform.GetChild(0), target.position, Quaternion.identity); // hit particle for every hit

                    }
                    if (shipName.GetComponent<EnemyHealth>().shipHealth == 0)
                    {
                        XP = shipName.GetComponent<EnemyHealth>().xpGain;
                        Instantiate(explosionEffect.transform.GetChild(0), target.position, Quaternion.identity);
                        Debug.Log(shipName.GetComponent<EnemyHealth>().shipHealth + " dead.");
                        xpGaining(XP);
                        XP = 0f;
                        Destroy(shipName);
                        GameObject temp = Instantiate(basicLoot, new Vector3(target.position.x, basicLoot.transform.position.y, target.position.z), Quaternion.identity);
                        Instantiate(basicLootIdleParticle, new Vector3(target.position.x, basicLoot.transform.position.y, target.position.z), Quaternion.identity, temp.transform);
                    }
                    break;
                default:
                    break;
            }
        }
    }
    public void xpGaining(float xp)
    {
        // FILLING THE XP BAR 
        FillingXP.fillAmount += (xp / 50);
        totalXP += xp;
        XPText.text = totalXP.ToString() + "/" + maxLevelXP.ToString();
        if (FillingXP.fillAmount > 0.53f)
        {
            XPText.color = Color.black;
        }
        else
        {
            XPText.color = Color.white;
        }
        if (FillingXP.fillAmount==1f)
        {
            Level++;
            FillingXP.fillAmount = 0f;
            XPText.color = Color.white;
            totalXP = 0;
            maxLevelXP += maxLevelXP;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        gold = Random.Range(3, 9); // these min and max range will be variables when I add the several enemies.
        wood = Random.Range(2, 5);
        if (other.CompareTag("Loot"))
        {
            ResourceManager.instance.AddResources(other.transform.position, gold, wood);
            Instantiate(LootPickParticle, other.transform.position, Quaternion.identity,other.transform);
            Destroy(other.gameObject,0.5f);

            StartCoroutine(ShowLootText());
        }
        if (other.CompareTag("LevelPass"))
        {
            GameObject.Find("ReqLevelText").GetComponent<TMP_Text>().enabled = true;
            GameObject.Find("ReqLevelText").GetComponent<TMP_Text>().text = "Level "+(Level+1)+" required.";
        }
        
    }
    
     private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LevelPass"))
        {
            GameObject.Find("ReqLevelText").GetComponent<TMP_Text>().enabled = false;
        }
    }
    IEnumerator ShowLootText()
    {
        LootedText.gameObject.SetActive(true);
        LootedText.text = "+" + gold + " Gold " + "\n" + "+" + wood + " Wood";
        yield return new WaitForSeconds(1.5f);
        LootedText.gameObject.SetActive(false);
        LootedText.text = "";
    }
   
}
