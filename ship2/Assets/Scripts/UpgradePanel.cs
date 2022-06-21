using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
public class UpgradePanel : MonoBehaviour
{
    public Image upgradePanel;
    public int atkSpeedCost = 10;
    public int atkDmgCost = 16;
    public int atkRangeCost = 25;
    public int shipSpeedCost = 20;

    public Text neededGoldText;
    public Button UpgradeButton;
    private void Update()
    {
        // SWITCH CASING FOR EVERY BUTTON -- Idk if this is the way, may be change in the future.
        Button[] upgradeButtons = FindObjectsOfType<Button>();
        foreach (var item in upgradeButtons)
        {
            switch (item.name)
            {
                case "AttackSpeedButton":
                    if (ResourceManager.instance.Coins >= atkSpeedCost)
                    {
                        item.interactable = true;
                        neededGoldText = item.transform.GetChild(1).GetComponent<Text>();
                        neededGoldText.text = atkSpeedCost.ToString();
                    }
                    else
                    {
                        item.interactable = false;
                        neededGoldText = item.transform.GetChild(1).GetComponent<Text>();
                        neededGoldText.text = atkSpeedCost.ToString();
                    }
                    break;
                case "AttackDamageButton":
                    if (ResourceManager.instance.Coins >= atkDmgCost)
                    {
                        item.interactable = true;
                        neededGoldText = item.transform.GetChild(1).GetComponent<Text>();
                        neededGoldText.text = atkDmgCost.ToString();
                    }
                    else
                    {
                        item.interactable = false;
                        neededGoldText = item.transform.GetChild(1).GetComponent<Text>();
                        neededGoldText.text = atkDmgCost.ToString();
                    }
                    break;
                case "AtkRangeButton":
                    if (ResourceManager.instance.Coins >= atkRangeCost)
                    {
                        item.interactable = true;
                        neededGoldText = item.transform.GetChild(1).GetComponent<Text>();
                        neededGoldText.text = atkRangeCost.ToString();
                    }
                    else
                    {
                        item.interactable = false;
                        neededGoldText = item.transform.GetChild(1).GetComponent<Text>();
                        neededGoldText.text = atkRangeCost.ToString();
                    }
                    break;
                case "ShipSpeedButton":
                    if (ResourceManager.instance.Coins >= shipSpeedCost)
                    {
                        item.interactable = true;
                        neededGoldText = item.transform.GetChild(1).GetComponent<Text>();
                        neededGoldText.text = shipSpeedCost.ToString();
                    }
                    else
                    {
                        item.interactable = false;
                        neededGoldText = item.transform.GetChild(1).GetComponent<Text>();
                        neededGoldText.text = shipSpeedCost.ToString();
                    }
                    break;
                default:
                    break;
            }
        }
    }
    public void OpenPanel()// CHECK IF THE UPGRADE BUTTON IS CLICKED
    {
        if (upgradePanel.IsActive())
        {
            upgradePanel.gameObject.SetActive(false);
        }
        else
        {
            upgradePanel.gameObject.SetActive(true);
        }
    }
    public void Upgrade(GameObject upgradeName) // Check which upgrade button is clicked and do upgrade of it. --- not sure of the way, may be change in the future. ---
    {
        switch (upgradeName.name)
        {
            case "AttackSpeedButton":
                Debug.Log("Atkspeed yükseltme");
                ResourceManager.instance.Coins -= atkSpeedCost;
                atkSpeedCost += 6;
                neededGoldText = upgradeName.transform.GetChild(1).GetComponent<Text>();
                neededGoldText.text = atkSpeedCost.ToString();
                PlayerAttackController.instance.PlayerShip.attackSpeed += 0.1f;
                break;
            case "AttackDamageButton":
                Debug.Log("AtkDmg yükseltme");
                ResourceManager.instance.Coins -= atkDmgCost;
                atkDmgCost += 8;
                neededGoldText = upgradeName.transform.GetChild(1).GetComponent<Text>();
                neededGoldText.text = atkDmgCost.ToString();
                PlayerAttackController.instance.PlayerShip.attack += 0.05f;
                break;
            case "AtkRangeButton":
                Debug.Log("Range yükseltme");
                ResourceManager.instance.Coins -= atkRangeCost;
                atkRangeCost += 9;
                neededGoldText = upgradeName.transform.GetChild(1).GetComponent<Text>();
                neededGoldText.text = atkRangeCost.ToString();
                PlayerAttackController.instance.range += 0.1f;
                break;
            case "ShipSpeedButton":
                Debug.Log("Hız yükseltme");
                ResourceManager.instance.Coins -= shipSpeedCost;
                shipSpeedCost += 7;
                neededGoldText = upgradeName.transform.GetChild(1).GetComponent<Text>();
                neededGoldText.text = shipSpeedCost.ToString();
                PlayerShipMovement.instance.moveSpeed += 0.1f;
                break;

            default:
                break;
        }
    }
}
