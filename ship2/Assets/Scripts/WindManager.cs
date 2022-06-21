using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
     // THIS SCRIPT IS NOT WORKING CORRECTLY. I WILL FIX THIS AFTER MY THOUGHTS COMPLETED


    [SerializeField]
    private GameObject WindObject;
    [SerializeField]
    private GameObject PlayerShip;
    [SerializeField]
    private GameObject SailObject;
    [SerializeField]
    private GameObject CMCam;
    public GameObject Camera;
    public float windCD;
    public float interested = 2f;
    private int startFOV = 70;
    private int endFOV = 85;
    void Start()
    {
        WindObject.gameObject.SetActive(false);
        WindObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f); 
    }
    void Update()
    {
        windCD += Time.deltaTime;
        if (windCD >= 10) // WIND COOLDOWN
        {
            Wind();
            windCD = 0F;
            StartCoroutine(windShowing());
        }

        ShipHasWind();


        if (PlayerShip.gameObject.GetComponent<PlayerShipMovement>().moveSpeed == 16f) // IF THE SHIP HAS WIND, SAIL IT
        {

            SailObject.transform.localScale = Vector3.Lerp(new Vector3(SailObject.transform.localScale.x, SailObject.transform.localScale.y, SailObject.transform.localScale.z),
                                                            new Vector3(SailObject.transform.localScale.x, 4, SailObject.transform.localScale.z), 3 * Time.deltaTime);
            StartCoroutine(FOVIncrease());
        }
        else // if no
        {
            SailObject.transform.localScale = Vector3.Lerp(new Vector3(SailObject.transform.localScale.x, SailObject.transform.localScale.y, SailObject.transform.localScale.z),
                                                       new Vector3(SailObject.transform.localScale.x, 1, SailObject.transform.localScale.z), 3 * Time.deltaTime);
            StartCoroutine(FOVDecrease());
        }
    }
    IEnumerator FOVDecrease() // THIS CALLED AFTER THE WIND BUFF ENDED
    {
        if (startFOV == 85) // DECREASING THE CAMERA FOV
        {
            while (startFOV > 70)
            {
                startFOV--;
                CMCam.gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.FieldOfView = startFOV;
                yield return new WaitForSeconds(0.02f);

            }
        }
    }
    IEnumerator FOVIncrease()
    {   // INCREASING FOV, CALLED WHEN THE SHIP GOT WIND
        if (startFOV == 70)
        {
            while (startFOV < endFOV)
            {
                startFOV++;
                CMCam.gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.FieldOfView = startFOV;
                yield return new WaitForSeconds(0.02f);

            }
        }
    }
    private void LateUpdate() // particle is following the ship to keep it in camera's view. -- this may change --
    {
        WindObject.transform.position = Vector3.MoveTowards(WindObject.transform.position, PlayerShip.transform.position, interested);
    }
    private void Wind()
    {
        WindObject.gameObject.SetActive(true);
        float getRandom = UnityEngine.Random.Range(17f, 311f); // random rotation to the wind
        WindObject.transform.rotation = Quaternion.Euler(0, getRandom, 0);
    }
    IEnumerator windShowing()
    {
        yield return new WaitForSeconds(5f);
        WindObject.gameObject.SetActive(false);
    }
    IEnumerator Speed()
    {
        yield return new WaitForSeconds(5f);
        PlayerShip.gameObject.GetComponent<PlayerShipMovement>().moveSpeed = 12f;

    }
    public void ShipHasWind()
    {
        Vector3 ship = PlayerShip.transform.eulerAngles;
        Vector3 wind = WindObject.transform.eulerAngles;
        if (Convert.ToInt32(ship.y) == Convert.ToInt32(wind.y)) // if the player ship rotation is equal to the wind rotation
        {
            PlayerShip.gameObject.GetComponent<PlayerShipMovement>().moveSpeed = 16f;
            WindObject.gameObject.SetActive(false);
            WindObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            StartCoroutine(Speed());
        }

    }
}
