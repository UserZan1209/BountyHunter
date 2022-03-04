using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : Weapon
{
    [SerializeField]protected bool isPlayerAttacking;

    protected void OnTriggerEnter(Collider other)
    {
        isPlayerAttacking = checkIfAttacking();
        if (other.tag == "Enemy" && isPlayerAttacking)
        {
            other.gameObject.GetComponent<Enemy>().takeDamage(damage);
            isPlayerAttacking=false;
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
}
