using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    //Base class variables
    [HeaderAttribute("Variables")]
    [SerializeField] public float damage;
    [SerializeField] public bool isPickedUp = false;
    [SerializeField] public bool canBePickedUp = false;
    [SerializeField] protected bool isPlayerAttacking;

    //UI related
    [SerializeField] private Canvas myPromptCanvas;
    [SerializeField] public Sprite icon;
    [SerializeField] public Image myImagePrompt;
    [SerializeField] public Sprite myPromptSprite;


    [HeaderAttribute("My References")]
    [SerializeField] public GameObject player;
    [SerializeField] protected BoxCollider myCollider;
    [SerializeField] protected Rigidbody myRB;
    //interaction related 
    [SerializeField] public GameObject rightHand;
    [SerializeField] public GameObject grabPoint;
    [SerializeField] protected GameObject rHand;
    [SerializeField] protected GameObject weaponObject;
    [SerializeField] protected GameObject UICanvas;

    [SerializeField] public GameObject[] myWeaponChoices;

    protected enum weaponTeir { basic, good, great, best}
    [SerializeField] protected weaponTeir myWeaponTeir;


    protected void Start()
    {
        player = gameEvents.current.playerObject;
        grabPoint = gameObject;
        myRB = this.gameObject.GetComponent<Rigidbody>();
        //change to enable disable the canvas
        myImagePrompt.enabled = false;

        //random weapon and tier
        myWeaponTeir = weaponTeir.basic;
        UICanvas = gameEvents.current.mainUIContainer;
    }

    protected void FixedUpdate()
    {
        pickupManager();         
    }

    protected void OnTriggerEnter(Collider other)
    {
        isPlayerAttacking = checkIfAttacking();

        if (other.tag == "Enemy" && isPlayerAttacking)
        {
            other.gameObject.GetComponent<Enemy>().takeDamage(damage);
            isPlayerAttacking = false;
        }

        //responsible for informing the weapon where the gameobject for the right hand is located
        if (other.gameObject.tag == "Player")
        {
            canBePickedUp = true;

            myImagePrompt.enabled = true;
            myPromptCanvas.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2.0f, this.gameObject.transform.position.z);
            myImagePrompt.sprite = myPromptSprite;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canBePickedUp = false;

            myImagePrompt.enabled = false;
        }
    }

    void pickupManager()
    {
        if (isPickedUp)
        {
            myRB.isKinematic = true;
            myImagePrompt.enabled = false;
            if (rightHand != null)
            {
                transform.position = rightHand.transform.position;
                transform.rotation = rightHand.transform.rotation;

                //transform.rotation = new Quaternion(0,-transform.rotation.y,0,0);

                myCollider.enabled = false;

            }
        }
        else if (!isPickedUp)
        {
            myRB.isKinematic = false;
            myCollider.enabled = true;
        }
    }

    protected bool checkIfAttacking()
    {
        //Check the parent player object is attacking so the weapon can tell if its just
        //a random collision.
        bool b;
        if(player != null)
        {
            b = player.GetComponent<player>().isAttacking;
            return b;
        }
        else
        {
            player = gameEvents.current.playerObject;
            b = player.GetComponent<player>().isAttacking;
            return b;

        }

    }
    public void setHand(GameObject rh)
    {
        rHand = rh;
    }

    public void nullHand()
    {
        rHand = null;
    }

}
