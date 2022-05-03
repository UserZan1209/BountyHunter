using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileWeapons : Weapon
{
    [SerializeField] float fireRate;
    [SerializeField] GameObject projectile;
    [SerializeField] protected Canvas uiCanvas;

    [SerializeField] protected GameObject bulletStartObj;

    [SerializeField] protected int maxCurrantMagazine;
    [SerializeField] protected int currantMagazineAmmo;
    [SerializeField] protected int remainingMagazines;

    //used for the delay while reloading
    [SerializeField]protected float timer;
   
    private void Update()
    {
       
        //checks if the weapon has been picked up and prevents shooting when all magazines are empty
        if (isPickedUp)
        {
            updateUI();
            if(timer > 0)
            {
                timer -= Time.deltaTime;
                uiCanvas.GetComponent<uiManager>().ammoBar.color = Color.cyan;
            }


            if (Input.GetKeyDown(KeyCode.Mouse0) && currantMagazineAmmo > 0)
            {
                if(timer <= 0)
                {
                    fireWeapon(fireRate);
                    updateUI();
                }                
            }
            else if(Input.GetKeyDown(KeyCode.R) || currantMagazineAmmo <= 0)
            {
                //the weapon will reload automatically when the currant magazine is empty
                //or when the player presses "R" manually
                reloadWeapon();
                updateUI();
            }

            //zooms in when aiming
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                cam.GetComponent<playerCameraMovement>().distance = 2;

            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                cam.GetComponent<playerCameraMovement>().distance = 5;

            }
        }
        else
        {
            //Debug.Log("No ammo remaining");
        }
    }
    void fireWeapon(float fireRate)
    {
        if(currantMagazineAmmo > 0)
        {
            //instanstiate projectile prefabs and creates more depending on fire rate
            for (int i = 0; i < fireRate; i++)
            {
                print(transform.GetChild(0).gameObject.name);

                GameObject inst = Instantiate(projectile, transform.position, Quaternion.identity);

                inst.transform.SetParent(this.transform);
                inst.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100.0f);

                //decrease ammo
                currantMagazineAmmo--;

            }
        }
        else
        {
            //attempt to reload if player fire with an empty magazine
            reloadWeapon();
        }

    }

    void reloadWeapon()
    {   
        if(remainingMagazines > 0)
        {
            currantMagazineAmmo = maxCurrantMagazine;
            timer += 2.0f;
            remainingMagazines--;
        }
    }

    void updateUI()
    {
        //sends weapon info to the UI
        uiCanvas.GetComponent<uiManager>().updateWeaponUI(maxCurrantMagazine, currantMagazineAmmo, remainingMagazines, icon);
    }
}


