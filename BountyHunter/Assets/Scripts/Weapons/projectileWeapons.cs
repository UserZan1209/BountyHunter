using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileWeapons : Weapon
{
    [SerializeField] float fireRate;
    [SerializeField] GameObject projectile;
    [SerializeField] protected Canvas uiCanvas;

    [SerializeField] public GameObject startObject;
    [SerializeField] public static Transform spawnPoint;
    [SerializeField] protected GameObject UIobject;

    [SerializeField] protected int maxCurrantMagazine;
    [SerializeField] protected int currantMagazineAmmo;
    [SerializeField] public int remainingMagazines;

    [SerializeField] protected Camera cam;

    //used for the delay while reloading
    [SerializeField]protected float timer;
    private void Awake()
    {
        UIobject = gameEvents.current.mainUIContainer;
    }
    private void Update()
    {
        
        //checks if the weapon has been picked up and prevents shooting when all magazines are empty
        if (isPickedUp)
        {
            
            if(timer > 0)
            {
                timer -= Time.deltaTime;
                UICanvas.GetComponent<uiManager>().ammoBar.color = Color.cyan;
            }
            else
            {
                UICanvas.GetComponent<uiManager>().ammoBar.color = Color.white;
            }


            if (Input.GetKeyDown(KeyCode.Mouse0) && currantMagazineAmmo > 0)
            {
                print("fire");
                updateUI();
                if (timer <= 0)
                {
                    fireWeapon(fireRate);
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
            if(cam != null)
            {
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
                cam = Camera.main;
                Debug.Log("camera reference saved");
            }

        }
        else
        {
            //Debug.Log("No ammo remaining");
        }

        
    }
    void fireWeapon(float fireRate)
    {
        spawnPoint = startObject.transform;
        if (currantMagazineAmmo > 0)
        {
            //instanstiate projectile prefabs and creates more depending on fire rate
            for (int i = 0; i < fireRate; i++)
            {
                GameObject inst = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
                

                //inst.transform.SetParent(this.transform);
                inst.transform.parent = null;
                //decrease ammo
                currantMagazineAmmo--;
            }
            updateUI();
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

    public void updateUI()
    {
        UIobject.gameObject.GetComponent<uiManager>().updateWeaponUI(currantMagazineAmmo,remainingMagazines,icon);
    }

}


