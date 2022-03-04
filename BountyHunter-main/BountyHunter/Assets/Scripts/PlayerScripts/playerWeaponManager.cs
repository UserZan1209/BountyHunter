using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWeaponManager : MonoBehaviour
{
    [Header("My Weapon Slots")]
    [SerializeField] protected GameObject myRightHand;

    [Header("My Currant Weapons")]
    [SerializeField] protected GameObject myMeleeWeapon;
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Wmelee":

                print("Colliding with a melee weapon");
                other.GetComponent<Weapon>().rightHand = myRightHand;

                if (other.GetComponent<Weapon>().isPickedUp != true)
                {
                    
                    if(myMeleeWeapon == null)
                    {
                        myMeleeWeapon = other.gameObject;
                    }
                    else
                    {
                        //ask player to swap weapons
                    }

                    print("aaa");
                    other.gameObject.transform.SetParent(myRightHand.transform);
                    myMeleeWeapon.gameObject.transform.localPosition = myRightHand.transform.localPosition;
                    myMeleeWeapon.gameObject.transform.localRotation = myRightHand.transform.localRotation;
                }

                other.GetComponent<Weapon>().isPickedUp = true;

                break;
        }
    }
}
