using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    // https://www.moddb.com/games/unspottable/news/blending-ragdoll-physics-and-animation-in-unity

    /*
     *The movement script for the player, it handles the movement, physics and ragdoll
     */


    //Left Joystick Values
    [Header("Left Joystick values")]
    [SerializeField] private float aH;
    [SerializeField] private float aV;

    [Header("References")]
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody playerRB;
    [SerializeField] Animator playerAnimator;

    [Header("Player stat values")]
    [SerializeField] private int playerHealth = 10;
    [SerializeField] private int jumpForce = 100;

    // player enums for different states
    private enum playerLife { isdead, isalive }
    [Header("Player states")]
    [SerializeField] private playerLife playerLifeState = playerLife.isalive;


    CharacterController Controller;
    [Header("Camera Controller")]
    [SerializeField] public float Speed;

    [SerializeField] public Transform Cam;


    private void Start()
    {
        playerInit(); // get all references and set fixed values
    }
    private void Update()
    {
        if (playerLifeState != playerLife.isdead)
        {
            toggleRagdoll();
        }

        cameraController();
    }
    private void FixedUpdate()
    {
        //Movement
        if (playerHealth > 0)
        {
            movePlayer();
        }
        else
        {
            //ragdoll here  
            playerAnimator.enabled = false;
            playerLifeState = playerLife.isdead;
        }

    }

    private void playerInit()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody>();
        playerAnimator = player.GetComponent<Animator>();
        playerRB.isKinematic = true;

        Controller = GetComponent<CharacterController>();
    }
    private void movePlayer()
    {
        aH = Input.GetAxis("Horizontal");
        aV = Input.GetAxis("Vertical");
        transform.Translate(aH * 3 * Time.deltaTime, 0, aV * 3.5f * Time.deltaTime);
    }

    private void toggleRagdoll()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.enabled = !playerAnimator.enabled;
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void cameraController()
    {

        float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
        Movement.y = 0f;



        Controller.Move(Movement);

        if (Movement.magnitude != 0f)
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Cam.GetComponent<playerCameraMovement>().sensivity * Time.deltaTime);


            Quaternion CamRotation = Cam.rotation;
            CamRotation.x = 0f;
            CamRotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);


        }
    }
}
