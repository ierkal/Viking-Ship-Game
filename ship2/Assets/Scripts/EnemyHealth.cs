using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shipstat;
public class EnemyHealth : MonoBehaviour
{
    public static EnemyHealth instance;
    public ShipStats basicShip;
    public float shipHealth;
    public float xpGain;
    public float attackSpeed;
    public GameObject shipObject;
    public GameObject HealthFill;
    public GameObject HealthBar;
    public float maxHealth=0;
    private void Start()
    {
        // setting the scriptableobject references to this
        HealthBar.SetActive(false);
        shipObject = gameObject;
        instance = this;
        switch (gameObject.name) // reason of the switch-casing is if I add another ships to the game
        {
            case "BasicShip":
                shipHealth = basicShip.health;
                xpGain = basicShip.xpGain;
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if (shipHealth > maxHealth) 
        {
            shipHealth = maxHealth;
        }
        HealthBarFiller();
        ColorChanger();
    }
    void HealthBarFiller()
    {
        HealthFill.transform.localScale = Vector3.Lerp(new Vector3(HealthFill.transform.localScale.x, 1f, 1f), new Vector3(shipHealth/maxHealth, 1f, 1f), 3f * Time.deltaTime);
    }
    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, shipHealth / maxHealth);
        HealthFill.transform.GetChild(0).GetComponent<SpriteRenderer>().color = healthColor;
    }

}
