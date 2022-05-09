using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    [SerializeField] protected float val;

    public enum pickUptype { health, Ammo };
    [SerializeField] public pickUptype pickUpType;

    private void Start()
    {
        //select correct model here
        switch (pickUpType)
        {
            case pickUptype.health:
                val = 50;
                break;
            case pickUptype.Ammo:
                val = 2;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            switch (pickUpType)
            {
                case pickUptype.health:
                    other.gameObject.GetComponent<playerMovement>().healPlayer(val);
                    break;
                case pickUptype.Ammo:
                    if (other.gameObject.GetComponent<playerWeaponManager>() != null)
                    {
                        if(other.gameObject.GetComponent<playerWeaponManager>().myRangedWeapon != null)
                        {
                            int ammo = Mathf.RoundToInt(val);
                            other.gameObject.GetComponent<playerWeaponManager>().myRangedWeapon.gameObject.GetComponent<projectileWeapons>().remainingMagazines += ammo;
                        }
                    }
                    break;
            }
        }
    }
}
