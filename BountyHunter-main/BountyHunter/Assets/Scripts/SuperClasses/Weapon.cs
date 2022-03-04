using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public GameObject player;
    [SerializeField] public bool isPickedUp = false;

    [SerializeField] protected BoxCollider myCollider;
    [SerializeField] protected Rigidbody myRb;

    [SerializeField] public GameObject rightHand;
    

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected void Update()
    {
        if (isPickedUp)
        {
            // myCollider.enabled = false;
            if(rightHand != null)
            {
                gameObject.transform.position = rightHand.transform.position;
            }
            myRb.useGravity = false;
        }
        else
        {
            myCollider.enabled = true;
            myRb.useGravity = true;
        }
    }
}
