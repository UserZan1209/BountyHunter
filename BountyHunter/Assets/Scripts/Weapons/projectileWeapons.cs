using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileWeapons : Weapon
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "mixamorig:RightHand")
        {
            rHand = collision.gameObject;
            print("test");
        }
    }
}
