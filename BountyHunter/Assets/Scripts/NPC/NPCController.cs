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

    //text box
    [SerializeField] protected string[] dialogue = new string[0];
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
        gameEvents.completeQuest += endQuest;
        gameEvents.spawnReward += spawnReward;
       
    }
    private void Update()
    {

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
        //use the iscompleted it collect reward
        if (other.tag == "Player")
        {
            myCanvas.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
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
        gameEvents.current.sendNPCinfo(quest, dialogue);
    }

    private void startQuest()
    {
        quest.isActive = true;    
    }

    private void endQuest()
    {
        quest.isCompleted = true;
        quest.isActive = false;
        //spawn prefab
    }

    private void spawnReward()
    {
        //spawn quest.item
        Instantiate(quest.itemGained, gameObject.transform.position, Quaternion.identity);
        Debug.Log("item spawned");
    }
}
