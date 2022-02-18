using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : player
{

    // https://www.moddb.com/games/unspottable/news/blending-ragdoll-physics-and-animation-in-unity

    playerControlMethod playerControllerMethod = playerControlMethod.controller;

    CharacterController Controller;
    [Header("Camera Controller")]
    [SerializeField] public float Speed;

    [SerializeField] public Transform Cam;


    private void Start()
    {
        InitMe();
        
    }
    private void Update()
    {
        if(health > 0)
        {
            toggleRagdoll();
        }

        myAnim.SetFloat("aH", base.aH);
        myAnim.SetFloat("aV", base.aV);

        inputActions();
        cameraController();
    }

    private void FixedUpdate()
    {
        //Movement
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
