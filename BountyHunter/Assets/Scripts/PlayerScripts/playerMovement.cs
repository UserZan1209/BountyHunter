using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : player
{
   // playerControlMethod playerControllerMethod = playerControlMethod.controller;

    [Header("Camera Controller")]
    [SerializeField] public float Speed;
    [SerializeField] public Transform Cam;
    [SerializeField] protected playerWeaponManager weaponManager;
    [SerializeField] protected Camera cameraRef;
    [SerializeField] public bool canRotate;
    protected string[] attackChecks = { "animPunch", "animAttack" };

    public enum playerControlMethod { controller, keyboard }
    public enum playerAttackMethod { none, melee, oneHand }
    [SerializeField] public playerAttackMethod attackMethod = playerAttackMethod.none;

    private void Start()
    {
        InitMe();
        weaponManager = this.gameObject.GetComponent<playerWeaponManager>();
        cameraRef = Camera.main;
        defaultFov = cameraRef.fieldOfView;
        aimingFov = defaultFov += 10.0f;
        canRotate = true;
    }
    private void Update()
    {
        //if player is alive then allow stamina to recover and ragdoll toggle is avaliable
        if(health > 0)
        {
            if(stamina > 0.5f)
            {
                toggleRagdoll();
            }
            
            if(my_ragdoll_state != RagdollState.isRagdoll && stamina < 100.0f)
            {
                stamina += Time.deltaTime * 2.5f;
            }
            
        }

        //if the stamina is too low then drop all weapons and ragdoll
        if(stamina <= 0.5f)
        {
            this.GetComponent<playerWeaponManager>().dropEverything();
            my_ragdoll_state = RagdollState.isRagdoll;
            my_movement_state = MovementState.isPhysics;
            myAnim.enabled = false;
            stamina = 1.0f;
        }

        //send axis aH, aV to the animator
        myAnim.SetFloat("aH", aH);
        myAnim.SetFloat("aV", aV);

        //the function is incharge of managing the inputs
        inputActions();
        cameraController();

        //lets the animator know when the player is aiming
        aimingManager();

        for(int i = 0; i < attackChecks.Length; i++)
        {
            //uses the name of the animation currantly playing to determine if the player is attacking
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName(attackChecks[i]))
            {
                isAttacking = true;
            }
        }
        if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("animIdle"))
        {
            isAttacking = false;
        }
    }

    private void FixedUpdate()
    {

        movementManager();
    }

    protected void movementManager()
    {
        if (health > 0)
        {
            movementController();
        }
        else
        {
            toggleRagdoll();
        }
    }
    private void cameraController()
    {
        if (canRotate)
        {
            //allows the camera to control the rotation of the player
            float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
            float Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

            Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
            Movement.y = 0f;

            if (Movement.magnitude != 0f)
            {
                transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Cam.GetComponent<playerCameraMovement>().sensivity * Time.deltaTime);

                Quaternion CamRotation = Cam.rotation;
                CamRotation.x = 0f;
                CamRotation.z = 0f;

                transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);
            }

            myGameObject.transform.eulerAngles = new Vector3(Cam.transform.rotation.x, myGameObject.transform.rotation.y, myGameObject.transform.rotation.z);

        }
    }

    public void healPlayer(float h)
    {
        health += h;
    }
}
