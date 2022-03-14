using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupHealth : pickup
{
    [Header("My Values")]
    [SerializeField] protected float healthRecover;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                other.gameObject.GetComponent<playerMovement>().healPlayer(healthRecover);
                Destroy(gameObject);
                break;
        }
    }
}
