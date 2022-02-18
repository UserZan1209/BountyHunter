using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class character : MonoBehaviour
{
    [SerializeField] protected float health = 10;
    [SerializeField] protected float speed = 6;


    [Header("my References")]
    [SerializeField] protected GameObject myGameObject;
    [SerializeField] protected Rigidbody myRB;
    [SerializeField] protected Animator myAnim;

    // enums for different states
    protected enum LifeState { isdead, isalive }
    [HideInInspector] public enum RagdollState { isRagdoll, isNotRagdoll }
    protected enum MovementState { isPhysics, isNotPhysics }

    [Header("my states")]
    [SerializeField] protected LifeState my_life_state = LifeState.isalive;
    //this enum is public due to the camera accessing it to prevent player rotoation while ragdolled
    [SerializeField] public RagdollState my_ragdoll_state = RagdollState.isNotRagdoll;
    [SerializeField] protected MovementState my_movement_state = MovementState.isNotPhysics;

    protected Rigidbody[] rigidbodyInChildren;


    protected void InitMe()
    {
        myGameObject = this.gameObject;
        myRB = myGameObject.GetComponent<Rigidbody>();
        rigidbodyInChildren = GetComponentsInChildren<Rigidbody>();
        myAnim = myGameObject.GetComponent<Animator>();
    }

    protected virtual void toggleRagdoll()
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
    }
}
