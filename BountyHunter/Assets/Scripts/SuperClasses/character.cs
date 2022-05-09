using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class character : MonoBehaviour
{
    [SerializeField] public float maxhealth = 100.0f;
    [SerializeField] public float health = 100.0f;
    [SerializeField] public float maxstamina = 100.0f;
    [SerializeField] public float stamina = 100.0f;
    [SerializeField] protected float speed = 6.0f;
    [SerializeField] protected float defSpeed;


    [Header("my References")]
    [SerializeField] protected GameObject myGameObject;
    [SerializeField] protected Rigidbody myRB;
    [SerializeField] protected Animator myAnim;
    [SerializeField] public bool isAiming;

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
        defSpeed = speed;
    }

    protected virtual void toggleRagdoll()
    {
        if(myAnim != null)
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
}
