using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWeaponManager : MonoBehaviour
{
    [Header("My Weapon Slots")]
    [SerializeField] protected GameObject myRightHand;

    [Header("My Weapons")]
    [SerializeField] protected GameObject myMeleeWeapon;
    [SerializeField] protected GameObject myRangedWeapon;
    [SerializeField] protected GameObject myFists;

    [SerializeField] public GameObject myCurrantWeapon;

    [SerializeField] protected GameObject myTargetWeapon;

    [Header("my References")]
    [SerializeField] private GameObject player;
    [SerializeField] protected playerMovement playerMovementRef;

    
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        playerMovementRef = this.gameObject.GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.gameObject.GetComponent<playerMovement>().my_ragdoll_state == character.RagdollState.isRagdoll)
        {
            //change fuctionallity to drop the currantly held weapon
            if(myCurrantWeapon != null)
            {
                dropWeapon(myCurrantWeapon);
            }
        }
        if(myMeleeWeapon != null && myRangedWeapon != null)
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                swapWeapon();
            }
        }

        else if (myMeleeWeapon == null || myRangedWeapon == null)
        {
           // Debug.Log("not enough weapons to switch");
        }

        weaponManager();
    }
    protected void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Weapon>() != null)
        {
            myTargetWeapon = other.gameObject;
        }
    }
    protected void OnTriggerStay(Collider other)
    {
        if (player.GetComponent<playerMovement>().my_ragdoll_state == character.RagdollState.isNotRagdoll && myTargetWeapon != null)
        {
            switch (other.gameObject.tag)
            {
                case "WoneHand":
                    if (myTargetWeapon.name == "weaponMelee" && myMeleeWeapon == null)
                    {
                        myMeleeWeapon = other.gameObject;
                        equipOneHandedWeapon(myTargetWeapon);
                        //Debug.Log("melee equiped");
                    }
                    else if (myTargetWeapon.name == "weaponPistol" && myRangedWeapon == null)
                    {
                        myRangedWeapon = other.gameObject;
                        equipOneHandedWeapon(myTargetWeapon);
                        //Debug.Log("pistol equiped");
                    }
                    break;
            }
        }
        else
        {
            //Debug.Log("cannot interact while in a ragdoll");
        }

    }
    protected void OnTriggerExit(Collider other)
    {
        if(myTargetWeapon != null)
        {
            myTargetWeapon = null;
        }
    }

    protected void equipOneHandedWeapon(GameObject cWeapon)
    {
        myTargetWeapon.GetComponent<Weapon>().rightHand = myRightHand;

        if (myTargetWeapon.GetComponent<Weapon>().isPickedUp != true)
        { 
            if (myCurrantWeapon == null)
            {
                myCurrantWeapon = myTargetWeapon.gameObject;
                myCurrantWeapon.GetComponent<Weapon>().setHand(myRightHand);

                myTargetWeapon.gameObject.transform.SetParent(myRightHand.transform);
            }
            else
            {
                myTargetWeapon.GetComponent<Weapon>().setHand(myRightHand);
                myTargetWeapon.gameObject.transform.SetParent(myRightHand.transform);
                myCurrantWeapon.SetActive(false);
                myCurrantWeapon = myTargetWeapon;
            }

            myTargetWeapon.GetComponent<Weapon>().isPickedUp = true;
        }

        myCurrantWeapon.SetActive(true);
    }

    protected void dropWeapon(GameObject cWeapon)
    {
        //pass through a gameobject and drop the specified gameobject

        //clear up the slots
        switch (cWeapon.name)
        {
            case "weaponMelee":
                myMeleeWeapon = null;
                break;
            case "weaponPistol":
                myRangedWeapon = null;
                break;
        }

        //disconnect object from the player
        myCurrantWeapon.GetComponent<Weapon>().isPickedUp = false;
        myCurrantWeapon.GetComponent<Weapon>().nullHand();
        myCurrantWeapon.transform.parent = null;
        myCurrantWeapon = null;
    }

    public void dropEverything()
    {
        //this function is used when the player runs out of stamina

        if(myMeleeWeapon != null)
        {
            dropWeapon(myMeleeWeapon);
        }

        if(myRangedWeapon != null)
        {
            dropWeapon(myRangedWeapon);
        }
    }

    protected void swapWeapon()
    {
        if(myCurrantWeapon.gameObject == myRangedWeapon)
        {
            myCurrantWeapon = myMeleeWeapon;
        }
        else if(myCurrantWeapon.gameObject == myCurrantWeapon)
        {
            myCurrantWeapon = myRangedWeapon;
        }


    }
    protected void weaponManager()
    {
        if(myCurrantWeapon != null)
        {
            switch (myCurrantWeapon.name)
            {
                case "weaponMelee":
                    print("holding melee");
                    if (myMeleeWeapon != null)
                    {
                        player.GetComponent<playerMovement>().attackMethod = playerMovement.playerAttackMethod.melee;
                        if(myRangedWeapon != null)
                        {
                            myRangedWeapon.SetActive(false);
                        }
                        myMeleeWeapon.SetActive(true);
                    }
                    break;
                case "weaponPistol":
                    if (myRangedWeapon != null)
                    {
                        player.GetComponent<playerMovement>().attackMethod = playerMovement.playerAttackMethod.oneHand;
                        if(myMeleeWeapon != null)
                        {
                            myMeleeWeapon.SetActive(false);
                        }
                        myRangedWeapon.SetActive(true);
                    }
                    break;
                default:
                    player.GetComponent<playerMovement>().attackMethod = playerMovement.playerAttackMethod.none;
                    break;
            }
        }
    }
}


