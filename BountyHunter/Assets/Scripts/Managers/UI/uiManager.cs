using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    //References
    [SerializeField] protected GameObject player;
    [Space]
    //Main UI variables
    [SerializeField] protected Text magazinesText;
    [SerializeField] protected GameObject bottomLeftUI;
    [SerializeField] protected Text textBoxText;
    [Space]

    //pausemenu
    [SerializeField] protected GameObject pauseContainer;

    [Space]
    //UI bars
    [SerializeField] protected Image expBar;
    [SerializeField] protected Image healthBar;
    [SerializeField] protected Image staminaBar;
    [SerializeField] public Image ammoBar;
    [SerializeField] public Image icon;
    [SerializeField] public Text level;
    [SerializeField] public Text reserveMagText;
    [Space]

    //NPC interface
    [SerializeField] protected GameObject NPCUI;
    [SerializeField] protected GameObject startQuestButton;
    [SerializeField] protected GameObject getQuestRewardButton;
    [Space]

    //MainMission and SideMission UI text
    [SerializeField] protected Text mainGoalText;
    [SerializeField] protected Text sideGoalText;
    //NPC sub menus
    [SerializeField] protected GameObject questMenu;
    //quest components
    [SerializeField] GameObject topRight;
    [SerializeField] protected Text questNameUI;
    [SerializeField] protected Text questDescUI;
    [SerializeField] protected Text questEXPUI;
    [SerializeField] protected Text questItemNameUI;

    [SerializeField] protected GameObject shopMenu;
    

    // Start is called before the first frame update
    void Start()
    {
        initMe();

        gameEvents.current.setMainUI(gameObject);

        if(SceneManager.GetActiveScene().name != "MainMenu")
        {
            topRight.SetActive(true);
        }
        else
        {
            topRight.SetActive(false);
        }

        //if player is null attempt to find it by tag
        if (player != null)
        {

            mainGoalText.text = "Eliminate Target";
            level.text = player.GetComponent<playerProgressionn>().level.ToString();
            sideGoalText.enabled = false;
        }
        else
        {
            gameEvents.current.setPlayer(GameObject.FindGameObjectWithTag("Player"));
            player = gameEvents.current.playerObject;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            calculateExpBar();
            calculateHealthBar();
            calculateStaminaBar();
        }
        else
        {
            //if player is null attempt to find it by tag
            Debug.Log("Player cannot be found on the UI Manager Script");
            gameEvents.current.setPlayer(GameObject.FindGameObjectWithTag("Player"));
            player = gameEvents.current.playerObject;
        }

    }

    private void initMe()
    {
        pauseContainer.SetActive(false);
        //close projectile weapon section of UI
        bottomLeftUI.SetActive(false);
        //Close npc sub menus
        getQuestRewardButton.SetActive(false);
        startQuestButton.SetActive(false);
        questMenu.SetActive(false);
        shopMenu.SetActive(false);
        //Close all npc menu
        NPCUI.SetActive(false);

        //listen to events
        gameEvents.onHealthUpdate += calculateExpBar;
        gameEvents.onStaminaUpdate += calculateStaminaBar;
        gameEvents.openNpcMenu += openNPCUI;
        gameEvents.closeNpcMenu += closeNPCUI;
        gameEvents.sendInfo += infoFromNPC;
        gameEvents.completeQuest += onQuestComplete;
        gameEvents.onPause += togglePauseMenu;
        gameEvents.updateUIObjective += onTargetDeathUI;
        gameEvents.onLevelUp += updateLevelUI;
        gameEvents.getPlayer += setPlayer;
        gameEvents.updateWeaponUI += updateWeaponUI;
        gameEvents.current.triggerBottomUi(bottomLeftUI);

    }

    protected void setPlayer()
    {
        player = gameEvents.current.playerObject;
    }

    protected void updateLevelUI()
    {
        level.text = player.GetComponent<playerProgressionn>().level.ToString();
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
        float health = gameEvents.current.health;
        healthBar.fillAmount = health / 100;

    }

    void calculateStaminaBar()
    {
        float stamina = gameEvents.current.stamina;
        staminaBar.fillAmount = stamina / 100;
    }

    public void updateWeaponUI()
    {

    }

    //open and close the NPC section of the UI
    public void openNPCUI()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        NPCUI.SetActive(true);
    }
    public void closeNPCUI()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        //if the player has started quest give reminder
        if (gameEvents.current.questRef.isActive && !gameEvents.current.questRef.isCompleted)
        {
            //output to text box the desc
            closeNPCUI_Quest();
        }
        //if the quest "isActive = false" and " is completed"
        //      allow the player to finish quest
        else if (!gameEvents.current.questRef.isActive && gameEvents.current.questRef.isCompleted)
        {
            if (!gameEvents.current.questRef.isClaimed)
            {
                getQuestRewardButton.SetActive(true);
                startQuestButton.SetActive(false);
            }
        }
        else if (!gameEvents.current.questRef.isActive && !gameEvents.current.questRef.isCompleted)
        {
            //first time opening quest menu
            getQuestRewardButton.SetActive(false);
            startQuestButton.SetActive(true);
        }
    }
    public void closeNPCUI_Quest()
    {
        getQuestRewardButton.SetActive(false);
        startQuestButton.SetActive(false);
        questMenu.SetActive(false);
    }

    //Accepting quest from the NPC
    public void acceptNPCQuest()
    {
        gameEvents.current.startNPCQuest();
        sideGoalText.enabled = true;
        sideGoalText.text = gameEvents.current.questDesc;
        questMenu.SetActive(false);
    }

    //retrive Info from NPC
    public void infoFromNPC()
    {
        // do the shop data like this <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        print(gameEvents.current.questName);
        questNameUI.text = gameEvents.current.questName;
        questDescUI.text = gameEvents.current.questDesc;
        questEXPUI.text = gameEvents.current.questExp.ToString();
        questItemNameUI.text = gameEvents.current.itemPref.name;

        //select some random dialoge, add more in the inspector
        int r = Random.Range(0, gameEvents.current.dialogeOption.Length);
        textBoxText.text = gameEvents.current.dialogeOption[r];

    }

    protected void onQuestComplete()
    {
        //update side ui
        sideGoalText.text = "Collect rewards";
    }

    public void claimRewards()
    {
        gameEvents.current.questRef.isClaimed = true;
        gameEvents.current.spawnItem();
        sideGoalText.enabled = false;
        closeNPCUI_Quest();
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

    protected void onTargetDeathUI()
    {
        mainGoalText.text = "Misson Complete. Returning to hub";
    }

    public void togglePauseMenu()
    {
        if(pauseContainer != null)
        {
            pauseContainer.SetActive(!pauseContainer.activeInHierarchy);

            if (pauseContainer.activeInHierarchy)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void loadMainMenu()
    {
        //used to load mainMenu without any saving
        SceneManager.LoadScene("MainMenu");
        
    }

    public void loadHUB()
    {
        //used to load HUB without any saving
        SceneManager.LoadScene("S_HubArea");

    }

    public void toggleBottomHalfUI()
    {
        bottomLeftUI.SetActive(!bottomLeftUI.activeInHierarchy);
    }

    public void updateWeaponUI(float cm, float tm, Sprite i)
    {
        ammoBar.fillAmount = cm / 10;
        magazinesText.text = tm.ToString();
        icon.sprite = i;
    }
}
