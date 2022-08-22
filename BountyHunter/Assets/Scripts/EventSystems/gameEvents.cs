using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameEvents : MonoBehaviour
{
    public static gameEvents current;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    //player related actions and variables
    [SerializeField] public GameObject playerObject;
    public static event Action getPlayer;
    public void setPlayer(GameObject player)
    {
        playerObject = player;
        getPlayer?.Invoke();
    }

    //UI manager related actions and variables
    [HideInInspector] public GameObject mainUIContainer;
    public static event Action getUI;
    public void setMainUI(GameObject mainUI)
    {
        mainUIContainer = mainUI;
        getUI?.Invoke();
    }

    public static event Action onPause;
    public void togglePauseMenu()
    {
        onPause?.Invoke();
    }
    public static event Action onLevelUp;
    public void changeLevelUI()
    {
        onLevelUp?.Invoke();
    }

    public static event Action onHealthUpdate;
    [HideInInspector] public float health;
    public void updateHealth(float h)
    {
        health = h;
        onHealthUpdate?.Invoke();
    }

    public static event Action onStaminaUpdate;
    [HideInInspector] public float stamina;
    public void updateStamina(float s)
    {
        stamina = s;
        onStaminaUpdate?.Invoke();
    }

    public static event Action onTargetDeath;
    public void targetDeath()
    {
        onTargetDeath?.Invoke();
    }

    public static event Action updateUIObjective;
    public void mainMissionEnd()
    {
       updateUIObjective?.Invoke();
    }

    //open npc menu
    public static event Action openNpcMenu;
    public void openNPCMenu()
    {
        openNpcMenu?.Invoke();
    }

    //close npc menu
    public static event Action closeNpcMenu;
    public void closeNPCMenu()
    {
        closeNpcMenu?.Invoke();
    }

    //start quest
    public static event Action startQuest;
    public void startNPCQuest()
    {
        startQuest?.Invoke();
    }

    //send quest info to UI Manager
    public static event Action sendInfo;
    [HideInInspector]public Quest questRef;
    [HideInInspector]public string questName;
    [HideInInspector]public string questDesc;
    [HideInInspector]public float questExp;
    [HideInInspector]public GameObject itemPref;
    [HideInInspector]public string[] dialogeOption = new string[0];
    public void sendNPCinfo(Quest q, string[] st)
    {
        //Store the recent NPC in the gameEvents
        dialogeOption = new string[st.Length];

        //store all the dialogue choices
        for(int i = 0; i < st.Length; i++)
        {
            dialogeOption[i] = st[i];
        }

        questRef = q;
        questName = q.questName;
        questDesc = q.questDesc;
        questExp = q.expGained;
        itemPref = q.itemGained;

        sendInfo?.Invoke();
    }
    //complete quest upon trigger
    public static event Action completeQuest;
    public void finishQuest()
    {
        completeQuest?.Invoke();
    }

    public static event Action spawnReward;
    public void spawnItem()
    {
        spawnReward?.Invoke();  
    }

    public SaveData recentSaveData;
    public void getSaveData(SaveData sD)
    {
        recentSaveData = sD;
    }
    public static event Action loadSave;
    public void loadGame()
    {
        loadSave?.Invoke();
    }

    public static event Action getBottomUI;
    [HideInInspector]public GameObject bottomUI;
    public void triggerBottomUi(GameObject bUI)
    {
        bottomUI = bUI;
        getBottomUI?.Invoke();
    }
    public static event Action updateWeaponUI;
    public void triggerWeaponUIUpdate()
    {
        updateWeaponUI?.Invoke();
    }

    public static event Action autoSave;
    public void triggerAutoSave()
    {
        autoSave?.Invoke();
    }
    
    //make action for open levels menu
}
