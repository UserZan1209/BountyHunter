using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistol : projectileWeapons
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        grabPoints = GameObject.FindGameObjectsWithTag("grabPoint");
        myRB = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
