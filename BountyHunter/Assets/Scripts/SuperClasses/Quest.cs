using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questName;
    public float expGained;
    public GameObject itemGained;

    public bool isActive;
}
