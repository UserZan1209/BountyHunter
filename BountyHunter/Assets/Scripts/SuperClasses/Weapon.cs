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
    

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        grabPoints = GameObject.FindGameObjectsWithTag("grabPoint");
        myRB = gameObject.GetComponent<Rigidbody>();
    }

    protected void FixedUpdate()
    {
        if (isPickedUp)
        {
            myRB.isKinematic = true;
        }
        else if (!isPickedUp)
        {
            myRB.isKinematic = false;
        }
    }
}
