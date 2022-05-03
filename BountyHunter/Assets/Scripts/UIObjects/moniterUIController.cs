using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moniterUIController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform playerHead;

    [SerializeField] public GameObject computerScreen;
    [SerializeField] public Canvas computerCanvas;
    [SerializeField] public Image Screen;
    [SerializeField] public Image interactPromptKimage;
    [SerializeField] public Sprite interactPromptK;
    [SerializeField] public Sprite interactPromptC;


    private bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        interactPromptKimage.sprite = interactPromptK;
        interactPromptKimage.enabled = false;
        computerScreen.SetActive(false);
        cam = Camera.main;
        playerHead = cam.GetComponent<playerCameraMovement>().lookAt;

    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (Input.GetKeyUp(KeyCode.E) && computerScreen.activeInHierarchy == false)
            {
                isOn = true;
                cam.GetComponent<playerCameraMovement>().lookAt = computerScreen.transform;
                cam.GetComponent<playerCameraMovement>().distance = 3;
                computerScreen.SetActive(true);
            }
            else if(Input.GetKeyUp(KeyCode.E) && computerScreen.activeInHierarchy == true)
            {
                isOn = false;
                cam.GetComponent<playerCameraMovement>().lookAt = playerHead;
                computerScreen.SetActive(false);
            }
        }

        if (isOn)
        {
            //computer behaviour to select levels goes here
            interactPromptKimage.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactPromptKimage.enabled = true;
            player = other.gameObject;
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(player != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactPromptKimage.enabled = false;
                //Debug.Log("interacting..");
                //display prompts here

                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (player != null)
        {
            computerScreen.SetActive(false);
            cam.GetComponent<playerCameraMovement>().distance = 5;
            cam.GetComponent<playerCameraMovement>().lookAt = playerHead;
            interactPromptKimage.enabled = false;
            player = null;
        }
    }

}
