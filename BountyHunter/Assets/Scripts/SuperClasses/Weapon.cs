using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public bool isPickedUp = false;

    [HeaderAttribute("My References")]
    [SerializeField] public GameObject player;
    [SerializeField] protected BoxCollider myCollider;
    [SerializeField] protected Rigidbody myRB;
    [SerializeField] public GameObject rightHand;
    [SerializeField] public GameObject[] grabPoints;

    [SerializeField] protected GameObject rHand;

    [SerializeField] protected bool isPlayerAttacking;

    protected Camera cam;

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        grabPoints = GameObject.FindGameObjectsWithTag("grabPoint");
        myRB = this.gameObject.GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    protected void FixedUpdate()
    {
        if (isPickedUp)
        {
            myRB.isKinematic = true;
            if(rightHand != null)
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

    protected void OnTriggerEnter(Collider other)
    {
        isPlayerAttacking = checkIfAttacking();
        if (other.tag == "Enemy" && isPlayerAttacking)
        {
            other.gameObject.GetComponent<Enemy>().takeDamage(damage);
            isPlayerAttacking = false;
        }

        if (other.gameObject.name == "mixamorig:RightHand")
        {
            rHand = other.gameObject;
            Debug.Log(gameObject.name + " has detected the players hand");
        }
    }

    protected bool checkIfAttacking()
    {
        //Check the parent player object is attacking so the weapon can tell if its just
        //a random collision.
        bool b;
        b = player.GetComponent<player>().isAttacking;
        return b;
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
