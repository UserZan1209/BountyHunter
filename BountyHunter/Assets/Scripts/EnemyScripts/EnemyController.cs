using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        InitMe();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            // the object is currently being destroyed at 0 health in "Enemy.cs"
            toggleRagdoll();
            my_life_state = LifeState.isdead;
        }


        if (myAnim.enabled == false)
        {
            my_ragdoll_state = RagdollState.isRagdoll;
            my_movement_state = MovementState.isPhysics;
        }
        else if (myAnim.enabled == true)
        {
            my_ragdoll_state = RagdollState.isNotRagdoll;
            my_movement_state = MovementState.isNotPhysics;
        }

    }
}
