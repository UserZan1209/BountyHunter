using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    // https://www.moddb.com/games/unspottable/news/blending-ragdoll-physics-and-animation-in-unity

    /*
     *The movement script for the player, it handles the movement, physics and ragdoll
     *
     *Issues:
     *camera dosnt follow well when using rbs
     *cant jump while moving normally
     */


    //Left Joystick Values
    [Header("Left Joystick values")]
    [SerializeField] private float aH;
    [SerializeField] private float aV;

    [Header("Right Joystick values")]
    [SerializeField] private float bH;
    [SerializeField] private float bV;

    [Header("References")]
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody playerRB;
    [SerializeField] Animator playerAnimator;
    private Rigidbody[] rigidbodyInChildren;
    
    [Header("Player stat values")]
    [SerializeField] private int playerHealth = 10;
    [SerializeField] private int Force = 100;
    [SerializeField] private float playerRotation = 10; // not the var used for camera sensiivity, just to make player face same dir as cam
    [SerializeField] private float defaultFov;


    // player enums for different states
    private enum playerLife { isdead, isalive }
    [HideInInspector]public enum playerRagdollState { isRagdoll, isNotRagdoll }
    private enum playerMovementState { isPhysics, isNotPhysics }
    private enum playerControlMethod { controller, keyboard }

    [Header("Player states")]
    [SerializeField] private playerLife playerLifeState = playerLife.isalive;
    [SerializeField] public playerRagdollState playerRagdollstate = playerRagdollState.isNotRagdoll;
    [SerializeField] private playerMovementState playerMovementstate = playerMovementState.isNotPhysics;
    [SerializeField] private playerControlMethod playerControllerMethod = playerControlMethod.keyboard;


    CharacterController Controller;
    [Header("Camera Controller")]
    [SerializeField] public float Speed;

    [SerializeField] public Transform Cam;


    private void Start()
    {
        playerInit(); // get all references and set fixed values
        // TEMP // 
        playerControllerMethod = playerControlMethod.controller;
    }
    private void Update()
    {
        if(playerHealth > 0)
        {
            toggleRagdoll();
        }

        playerAnimator.SetFloat("aH", aH);
        playerAnimator.SetFloat("aV", aV);
        actions();
        cameraController();
    }

    private void FixedUpdate()
    {
        //Movement
        if (playerHealth > 0)
        {
            // aH and aV are also mapped to the left stick of a controller aswell as w,a,s,d
            aH = Input.GetAxis("Horizontal");
            aV = Input.GetAxis("Vertical");

            if (playerLifeState != playerLife.isdead)
            {
                playerJump();
                toggleRagdoll();
            }

            //add a input for bV
            switch (playerMovementstate)
            {

                case playerMovementState.isPhysics:
                    playerAnimator.SetBool("isRagdoll", true);
                    forcePlayer();
                    break;
                case playerMovementState.isNotPhysics:
                    playerAnimator.SetBool("isRagdoll", false);
                    transformPlayer();
                    break;
            }
        }
        else
        {
            toggleRagdoll();
        }

    }

    private void playerInit()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody>();
        rigidbodyInChildren = GetComponentsInChildren<Rigidbody>();
        playerAnimator = player.GetComponent<Animator>();
    }
    private void transformPlayer()
    {
        transform.Translate(aH * 6 * Time.deltaTime, 0, aV * 7f * Time.deltaTime);
    }

    private void forcePlayer()
    {
        playerJump();

    }

    private void actions()
    {
        if (Input.GetButtonUp("X/Square") || Input.GetKeyUp(KeyCode.Q))
        {
            playerAnimator.SetTrigger("x/square");
        }
        else
        {
            playerAnimator.ResetTrigger("x/square");
        }

        if (Input.GetButtonUp("Y/Triangle") || Input.GetKeyUp(KeyCode.E))
        {
            playerAnimator.SetTrigger("y/triangle");
        }
        else
        {
            playerAnimator.ResetTrigger("y/triangle");
        }
        if (Input.GetButtonUp("A/Cross") || Input.GetKeyUp(KeyCode.R))
        {
            playerAnimator.SetTrigger("a/cross");
        }
        else
        {
            playerAnimator.ResetTrigger("a/cross");
        }

        if (aH != 0 || aV != 0)
        {
            running();
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);

        }

        

    }
    private void playerJump()// ERROR
    {
        if (Input.GetButtonUp("B/Circle"))
        {
            foreach(Rigidbody rb in rigidbodyInChildren)
            {
                //rb.AddForce(Vector3.up * (Force*30) * Time.deltaTime,ForceMode.Impulse);
            }
        }
    }
    private void toggleRagdoll()
    {
        //this function allows the player to manually enter the ragdoll mode
        //this function is re-used when the player dies to ragdoll and then set its life state to dead
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonUp("A/Cross") == true)
        {
            playerAnimator.enabled = !playerAnimator.enabled;

            if (playerAnimator.enabled == false) 
            { 
                playerRagdollstate = playerRagdollState.isRagdoll;
                playerMovementstate = playerMovementState.isPhysics;
            }
            else if(playerAnimator.enabled == true)
            {
                playerRagdollstate = playerRagdollState.isNotRagdoll;
                playerMovementstate = playerMovementState.isNotPhysics;
            }

        }

        if (playerHealth <= 0)
        {
            playerAnimator.enabled = false;
            playerLifeState = playerLife.isdead;
        }
    }

    private void running()
    {
        playerAnimator.SetBool("isRunning", true);
    }
    private void cameraController()
    {
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

        player.transform.eulerAngles = new Vector3(Cam.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z);


    }
}
