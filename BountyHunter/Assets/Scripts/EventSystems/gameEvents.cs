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

    public static event Action onTargetDeath;
    public void targetDeath()
    {
        onTargetDeath?.Invoke();
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
    [HideInInspector]public string questName;
    [HideInInspector]public string questDesc;
    [HideInInspector]public float questExp;
    public void sendNPCinfo(string t)
    {
        questName = t;
        sendInfo?.Invoke();
    }
    //make action for open levels menu
}
