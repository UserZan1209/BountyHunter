using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager
{
    [SerializeField] public bool isTargetAlive;
    // Start is called before the first frame update
    void Start()
    {
        isTargetAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void killTarget()
    {
        isTargetAlive = false;
        Debug.Log("Target has been killed");
    }
}
