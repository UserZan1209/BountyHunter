using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWeaponManager : MonoBehaviour
{
    [Header("My Weapon Slots")]
    [SerializeField] protected GameObject myRightHand;

    [Header("My Weapons")]
    [SerializeField] public GameObject myMeleeWeapon;
    [SerializeField] public GameObject myRangedWeapon;
    [SerializeField] public GameObject myFists;

    [SerializeField] public GameObject myCurrantWeapon;

    [SerializeField] protected GameObject myTargetWeapon;

    [Header("my References")]
    [SerializeField] private GameObject player;
    [SerializeField] protected playerMovement playerMovementRef;
    [SerializeField] protected GameObject weaponUI;

    
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        playerMovementRef = this.gameObject.GetComponent<playerMovement>();

        weaponUI = gameEvents.current.bottomUI;
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
        if(other.GetComponent<Weapon>() != null)
        {
            myTargetWeapon = other.gameObject;
        }
    }
    protected void OnTriggerStay(Collider other)
    {
        //get the gameobject infomation for use in the weapon manager
        if (myTargetWeapon != null)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Y/Triangle"))
            {
                if (other.name == "WeaponGrabPoint" && other.gameObject == myTargetWeapon.gameObject)
                {
                    print("b");
                    myMeleeWeapon = other.gameObject;
                    equipOneHandedWeapon(myMeleeWeapon);
                    //Debug.Log("melee equiped");
                }
                else if (other.name == "weaponPistol" && other.gameObject == myTargetWeapon.gameObject)
                {
                    myRangedWeapon = other.gameObject;
                    equipOneHandedWeapon(myRangedWeapon);
                    weaponUI.SetActive(true);
                    myRangedWeapon.GetComponent<pistol>().updateUI();
                    //Debug.Log("pistol equiped");
                }
            }
        }

    }
    protected void OnTriggerExit(Collider other)
    {
        //empties the target weapon container for use when the player encounters another 
        if(myTargetWeapon != null)
        {
            if (other.gameObject == myTargetWeapon.gameObject)
            {
                myTargetWeapon = null;
            }
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
                //myTargetWeapon.transform.rotation = new Quaternion(myRightHand.transform.rotation.x, myRightHand.transform.rotation.y + 90, myRightHand.transform.rotation.z, myRightHand.transform.rotation.w);

            }
            else
            {
                myTargetWeapon = cWeapon;
                myCurrantWeapon = myTargetWeapon;
                myTargetWeapon.GetComponent<Weapon>().setHand(myRightHand);
                myTargetWeapon.gameObject.transform.SetParent(myRightHand.transform);
                //myTargetWeapon.transform.rotation = new Quaternion (myRightHand.transform.rotation.x, myRightHand.transform.rotation.y + 90, myRightHand.transform.rotation.z, myRightHand.transform.rotation.w);
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
                weaponUI.SetActive(false);
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
            weaponUI.SetActive(false);

        }
        else if(myCurrantWeapon == myMeleeWeapon)
        {
            myCurrantWeapon = myRangedWeapon;
            weaponUI.SetActive(true);

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


