using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : character 
{
    [SerializeField] public int baseExp;

    virtual public void takeDamage(float d)
    {
        health -= d;
    }
}
