using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moniterUIController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform playerHead;

    [SerializeField] public Image computerScreen;
    [SerializeField] public Image interactPromptK;
    [SerializeField] public Image interactPromptC;

    private bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        computerScreen.enabled = false;
        cam = Camera.main;
        playerHead = cam.GetComponent<playerCameraMovement>().lookAt;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (Input.GetKeyUp(KeyCode.E) && computerScreen.enabled == false)
            {
                isOn = true;
                cam.GetComponent<playerCameraMovement>().lookAt = computerScreen.transform;
                cam.GetComponent<playerCameraMovement>().distance = 3;
                computerScreen.enabled = true;
            }
            else if(Input.GetKeyUp(KeyCode.E) && computerScreen.enabled == true)
            {
                isOn = false;
                cam.GetComponent<playerCameraMovement>().lookAt = playerHead;
                computerScreen.enabled = false;
            }
        }

        if (isOn)
        {
            //computer behaviour to select levels goes here
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            player = other.gameObject;
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(player != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("interacting..");
                //display prompts here

                computerScreen.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (player != null)
        {
            computerScreen.enabled = false;
            cam.GetComponent<playerCameraMovement>().distance = 5;
            cam.GetComponent<playerCameraMovement>().lookAt = playerHead;
            player = null;
        }
    }
}
