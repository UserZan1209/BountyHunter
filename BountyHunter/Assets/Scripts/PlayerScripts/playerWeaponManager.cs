using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWeaponManager : MonoBehaviour
{
    [Header("My Weapon Slots")]
    [SerializeField] protected GameObject myRightHand;

    [Header("My Currant Weapons")]
    [SerializeField] protected GameObject myMeleeWeapon;

    [Header("my References")]
    [SerializeField] private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.gameObject.GetComponent<playerMovement>().my_ragdoll_state == character.RagdollState.isRagdoll)
        {
            //change fuctionallity to drop the currantly held weapon
            dropWeapon();
        }
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

                    if(other.transform.childCount > 1)
                    {
                        // when the weapon has more than one grab point on the weapon
                    }
                    else
                    {
                        myMeleeWeapon.gameObject.transform.localPosition = myRightHand.transform.localPosition;
                        myMeleeWeapon.gameObject.transform.localRotation = myRightHand.transform.localRotation;

                    }
                }

                other.GetComponent<Weapon>().isPickedUp = true;
                break;
        }
    }

    protected void dropWeapon()
    {
        myMeleeWeapon.GetComponent<Weapon>().isPickedUp = false;
        myMeleeWeapon.transform.parent = null;
    }
}


