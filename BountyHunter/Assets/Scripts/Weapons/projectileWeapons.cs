using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileWeapons : Weapon
{
    [SerializeField] float fireRate;
    [SerializeField] GameObject projectile;

    [SerializeField] protected GameObject bulletStartObj;
   
    private void Update()
    {
       
        if (isPickedUp)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                fireWeapon(fireRate);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                cam.GetComponent<playerCameraMovement>().distance = 2;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                cam.GetComponent<playerCameraMovement>().distance = 5;
            }
        }
    }
    void fireWeapon(float fireRate)
    {
        for(int i = 0; i < fireRate; i++)
        {
            print(transform.GetChild(3).gameObject.name);

            GameObject inst = Instantiate(projectile,transform.position ,bulletStartObj.transform.rotation);

            inst.transform.SetParent(this.transform);

        }
    }
}
