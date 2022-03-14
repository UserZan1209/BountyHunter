using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : character
{
    [Header("Left Joystick Values")]
    [SerializeField] protected float aH;
    [SerializeField] protected float aV;

    [Header("Right Joystick Values")]
    [SerializeField] protected float bH;
    [SerializeField] protected float bV;

    [Header("My Camera Stat Values")]
    [SerializeField] protected float playerRotation = 10; // not the var used for camera sensiivity, just to make player face same dir as cam
    [SerializeField] protected float defaultFov;

    [Header("My Combat related values")]
    [SerializeField] public bool isAttacking;

    protected enum playerControlMethod { controller, keyboard }

    //determines how the player will move based on the health and movement states
    protected void movementController()
    {
        // aH and aV are also mapped to the left stick and right stick of a controller aswell as w,a,s,d
        aH = Input.GetAxis("Horizontal");
        aV = Input.GetAxis("Vertical");

        if (my_life_state != LifeState.isdead)
        {
            playerJump();
            toggleRagdoll();
        }

        switch (my_movement_state)
        {
            case MovementState.isPhysics:
                myAnim.SetBool("isRagdoll", true);
                forcePlayer();
                break;
            case MovementState.isNotPhysics:
                myAnim.SetBool("isRagdoll", false);
                transformPlayer();
                break;
        }
    }
    //used to move the player outside of ragdolling
    protected void transformPlayer()
    {
        transform.Translate(aH * 6 * Time.deltaTime, 0, aV * 7f * Time.deltaTime);
    }
    //used to add a force upwards
    protected void forcePlayer()
    {
        playerJump();

    }
    //jump (dosnt work at the moment)
    protected void playerJump()
    {
        if (Input.GetButtonUp("B/Circle") || Input.GetKeyUp(KeyCode.J))
        {
            foreach (Rigidbody rb in rigidbodyInChildren)
            {
                //rb.AddForce(Vector3.up * (Force*30) * Time.deltaTime,ForceMode.Impulse);
            }
        }
    }

    protected override void toggleRagdoll()
    {
        //this function allows the player to manually enter the ragdoll mode
        //this function is re-used when the player dies to ragdoll and then set its life state to dead
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonUp("A/Cross") == true)
        {
            myAnim.enabled = !myAnim.enabled;

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
            // UPDATE ^^^^^^^^^^^^^^^ : https://answers.unity.com/questions/39793/enable-or-disable-rigid-body-at-runtime.html
        }

        if (health <= 0)
        {
            myAnim.enabled = false;
            my_life_state = LifeState.isdead;

        }
    }

    protected void running()
    {
        myAnim.SetBool("isRunning", true);
    }

    protected void inputActions()
    {
        if (Input.GetButtonUp("X/Square") || Input.GetKeyUp(KeyCode.Q))
        {
            myAnim.SetTrigger("x/square");
            isAttacking = true;
        }
        else
        {
            myAnim.ResetTrigger("x/square");
        }

        if (Input.GetButtonUp("Y/Triangle") || Input.GetKeyUp(KeyCode.E))
        {
            myAnim.SetTrigger("y/triangle");
            isAttacking = true;
        }
        else
        {
            myAnim.ResetTrigger("y/triangle");
        }
        if (Input.GetButtonUp("A/Cross") || Input.GetKeyUp(KeyCode.R))
        {
            myAnim.SetTrigger("a/cross");
        }
        else
        {
            myAnim.ResetTrigger("a/cross");
        }

        if (aH != 0 || aV != 0)
        {
            running();
        }
        else
        {
            myAnim.SetBool("isRunning", false);

        }
    }

}
