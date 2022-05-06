using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{

    //criteria
    //prompt
    [SerializeField] protected Canvas myCanvas;
    [SerializeField] protected Image myPromptImage;
    [SerializeField] protected Sprite mySpritePrompt;
    //*inventory system using gameobject prefabs

    [SerializeField] protected bool isInteracting;
    //*make the camera focus on NPC 
    //*give a quest // Do later on
    public Quest quest;

    //*shop option

    // Start is called before the first frame update
    void Start()
    {
        myPromptImage.sprite = mySpritePrompt;
        myCanvas.enabled = false;
        gameEvents.startQuest += startQuest;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            sendInfo();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            myCanvas.enabled = true;
            print("a");
            if (Input.GetKeyUp(KeyCode.E))
            {
                print("b");
                gameEvents.current.openNPCMenu();
                isInteracting = false;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            myCanvas.enabled = false;
        }
    }

    private void sendInfo()
    {
        gameEvents.current.sendNPCinfo("s");
    }

    private void startQuest()
    {
        quest.isActive = true;    
    }
}
