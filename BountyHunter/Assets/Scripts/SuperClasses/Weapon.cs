using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public bool isPickedUp = false;

    [HeaderAttribute("My References")]
    [SerializeField] public GameObject player;
    //[SerializeField] protected BoxCollider myCollider;
    [SerializeField] protected Rigidbody myRB;
    [SerializeField] public GameObject rightHand;
    [SerializeField] public GameObject[] grabPoints;

    [SerializeField] protected GameObject rHand;

    [SerializeField] protected bool isPlayerAttacking;

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        grabPoints = GameObject.FindGameObjectsWithTag("grabPoint");
        myRB = this.gameObject.GetComponent<Rigidbody>();
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
                
            }
        }
        else if (!isPickedUp)
        {
            myRB.isKinematic = false;
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
    }

    bool checkIfAttacking()
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
