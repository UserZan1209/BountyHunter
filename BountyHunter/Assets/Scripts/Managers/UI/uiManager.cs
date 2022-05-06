using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    //References
    [SerializeField] protected GameObject player;
    [Space]
    //Main UI variables
    [SerializeField] protected Text magazinesText;
    [SerializeField] protected GameObject bottomLeftUI;
    [Space]
    //UI bars
    [SerializeField] protected Image expBar;
    [SerializeField] protected Image healthBar;
    [SerializeField] protected Image staminaBar;
    [SerializeField] public Image ammoBar;
    [SerializeField] protected Image icon;
    [Space]

    //NPC interface
    [SerializeField] protected GameObject NPCUI;
    [SerializeField] protected GameObject NPCObject;
    [SerializeField] protected Button startQuestButton;
    //NPC sub menus
    [SerializeField] protected GameObject questMenu;
    //quest components
    [SerializeField] protected Text questName;
    [SerializeField] protected GameObject shopMenu;
    

    // Start is called before the first frame update
    void Start()
    {
        initMe();
    }

    // Update is called once per frame
    void Update()
    {
        calculateExpBar();
        calculateHealthBar();
        calculateStaminaBar();
    }

    private void initMe()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //close projectile weapon section of UI
        bottomLeftUI.SetActive(false);
        //Close npc sub menus
        questMenu.SetActive(false);
        shopMenu.SetActive(false);
        //Close all npc menu
        NPCUI.SetActive(false);

        //listen to events
        gameEvents.openNpcMenu += openNPCUI;
        gameEvents.closeNpcMenu += closeNPCUI;
        gameEvents.sendInfo += infoFromNPC;

        //startQuestButton.onClick = NPCObject
    }
    void calculateExpBar()
    {
        float xpThisLevel = player.GetComponent<playerProgressionn>().XPthisLevel;
        float xpReqthisLevel = player.GetComponent<playerProgressionn>().requiredXP;

        float multiplyer = (100 / xpReqthisLevel);
        expBar.fillAmount = (xpThisLevel * multiplyer) / 100.0f;
        
    }

    void calculateHealthBar()
    {
        float health = player.GetComponent<playerMovement>().health;

        healthBar.fillAmount = health / 10;
    }

    void calculateStaminaBar()
    {
        float stamina = player.GetComponent<playerMovement>().stamina;

        staminaBar.fillAmount = stamina / 100;
    }

    public void updateWeaponUI(int maxAmmo, int curAmmo, int curClips, Sprite weaponIcon)
    {
        //display currant ammo
        float multplyer = 100.0f / maxAmmo;
        ammoBar.fillAmount = (curAmmo * multplyer) / 100.0f;
        if(ammoBar.fillAmount <= 0.3f)
        {
            ammoBar.color = Color.red;
        }
        else
        {
            ammoBar.color = Color.white;
        }
        

        //display currant magazines
        magazinesText.text = curClips.ToString();
        icon.sprite = weaponIcon;
        bottomLeftUI.SetActive(true);
    }

    //open and close the NPC section of the UI
    public void openNPCUI()
    {
        NPCUI.SetActive(true);
    }
    public void closeNPCUI()
    {
        //close quest and shop menus so they are closed upon reinteraction
        questMenu.SetActive(false);
        shopMenu.SetActive(false); 
        NPCUI.SetActive(false);
    }

    //open and close the Quest section of the NPC UI
    public void openNPCUI_Quest()
    {
        shopMenu.SetActive(false);
        questMenu.SetActive(true);
    }
    public void closeNPCUI_Quest()
    {
        questMenu.SetActive(false);
    }

    //Accepting quest from the NPC
    public void acceptNPCQuest()
    {
        gameEvents.current.startNPCQuest();
        questMenu.SetActive(false);
    }

    //retrive Info from NPC
    public void infoFromNPC()
    {
        print(gameEvents.current.questName);
        questName.text = gameEvents.current.questName;
    }
    //open and close the Shop section of the NPC UI
    public void openNPCUI_Shop()
    {
        //NOTE load all shop data and level reqiurement for items here
        questMenu.SetActive(false);
        shopMenu.SetActive(true);
    }
    public void closeNPCUI_Shop()
    {
        shopMenu.SetActive(false);
    }
}
