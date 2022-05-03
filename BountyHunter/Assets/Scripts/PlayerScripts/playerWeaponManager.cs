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
            //change fuctionallity to drop the held weapons
            if(myCurrantWeapon != null)
            {
                dropEverything();
            }
        }

        //if player has more than one weapon player may switch weapons
        if(myMeleeWeapon != null && myRangedWeapon != null)
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                swapWeapon();
            }
        }

        weaponManager();

        //drop current weapon
        if(myCurrantWeapon != null && Input.GetKeyUp(KeyCode.G))
        {
            dropWeapon(myCurrantWeapon);
            if(myRangedWeapon != null)
            {
                myCurrantWeapon = myRangedWeapon;
            }
            if(myMeleeWeapon != null)
            {
                myCurrantWeapon = myMeleeWeapon;
            }
        }
    }
    protected void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Weapon>() != null || other.GetComponent<melee>() != null)
        {
            myTargetWeapon = other.gameObject;
        }
    }
    protected void OnTriggerStay(Collider other)
    {
        //get the gameobject infomation for use in the weapon manager
        if (other.GetComponent<Weapon>() != null || other.GetComponent<melee>() != null)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                //using the target weapon this statement controls what happends when different weapons are picked up
                if (player.GetComponent<playerMovement>().my_ragdoll_state == character.RagdollState.isNotRagdoll)
                {
                    switch (other.gameObject.tag)
                    {
                        case "WoneHand":
                            if (other.name == "weaponMelee")
                            {
                                myMeleeWeapon = other.gameObject;
                                equipOneHandedWeapon(myMeleeWeapon);
                                //Debug.Log("melee equiped");
                            }
                            else if (other.name == "weaponPistol")
                            {
                                myRangedWeapon = other.gameObject;
                                equipOneHandedWeapon(myRangedWeapon);
                                //Debug.Log("pistol equiped");
                            }
                            break;
                    }
                }
            }
        }

    }
    protected void OnTriggerExit(Collider other)
    {
        //empties the target weapon container for use when the player encounters another weapon
        
        if(myTargetWeapon != null)
        {
           
           // myTargetWeapon = null;
        }
    }

    protected void equipOneHandedWeapon(GameObject cWeapon)
    {
        bool pickedUp = cWeapon.GetComponent<Weapon>().isPickedUp;
        //to prevent the player stuck in a constant state of picking up the same weapon the statement checks if the weapon is picked up
        if (!pickedUp)
        {
            //passes through the infomation for the players right hand
            if (cWeapon.GetComponent<Weapon>()!= null){
                cWeapon.GetComponent<Weapon>().rightHand = myRightHand;
            }
            if (cWeapon.GetComponent<melee>() != null)
            {
                cWeapon.GetComponent<melee>().rightHand = myRightHand;
            }


            //set the currantweapon to the new target weapon
            if (myCurrantWeapon == null)
            {
                //sets the currant weapon to be the picked up weapon
                myCurrantWeapon = cWeapon.gameObject;
                myCurrantWeapon.GetComponent<Weapon>().setHand(myRightHand);
                myTargetWeapon.gameObject.transform.SetParent(myRightHand.transform);
            }
            else
            {
                myTargetWeapon = cWeapon;
                myCurrantWeapon = myTargetWeapon;
                myTargetWeapon.GetComponent<Weapon>().setHand(myRightHand);
                myTargetWeapon.gameObject.transform.SetParent(myRightHand.transform);
                myTargetWeapon.transform.rotation = myRightHand.transform.rotation;
                //logic used when picking up a second weapon / replacing a weapon
                switch (cWeapon.gameObject.name)
                {
                    //drop and replace weapon
                    case "weaponMelee":
                        if (myCurrantWeapon.name == cWeapon.name)
                        {
                            //if the new weapon is a melee weapon, drop melee item
                            dropWeapon(myCurrantWeapon);
                        }
                        break;
                    case "weaponPistol":
                        if(myCurrantWeapon.name == cWeapon.name)
                        {
                            dropWeapon(myCurrantWeapon);
                        }
                        break;
                }

                equipOneHandedWeapon(cWeapon);

            }

            myTargetWeapon.GetComponent<Weapon>().isPickedUp = true;
        }

        
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
        if(myCurrantWeapon == myRangedWeapon)
        {
            myCurrantWeapon = myMeleeWeapon;
        }
        else if(myCurrantWeapon == myMeleeWeapon)
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


