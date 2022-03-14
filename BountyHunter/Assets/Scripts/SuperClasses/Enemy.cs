using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : character 
{ 
    public void takeDamage(float d)
    {
        health -= d;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
