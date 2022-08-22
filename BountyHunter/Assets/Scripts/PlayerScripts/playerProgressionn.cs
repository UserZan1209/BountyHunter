using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerProgressionn : MonoBehaviour
{
    [SerializeField] public int level;
    [SerializeField] public float totalXP;
    [SerializeField] public float requiredXP;
    [SerializeField] public float XPthisLevel;

    // Start is called before the first frame update
    void Start()
    {
        //load
        if(SceneManager.GetActiveScene().name != "DebugScene")
        {
            if (gameEvents.current.recentSaveData != null)
            {
                SaveData sD = gameEvents.current.recentSaveData;
                level = sD.level;
                XPthisLevel = sD.xpThisLevel;

                print("level = "+level + "xp = "+XPthisLevel);

                calculateReqExp();

                gameEvents.current.changeLevelUI();
            }
            else
            {
                level = 1;
            }
        }
        else
        {
            level = 1;
        }

       
        calculateReqExp();
    }

    // Update is called once per frame
    void Update()
    {
        if (totalXP >= requiredXP)
        {
            //calculate if the the remainder to check if its not a clean level
            float remainder = totalXP -= requiredXP;
            XPthisLevel = 0;

            //if there is a remainder add it back to xp this level
            if (remainder > 0)
            {
                XPthisLevel += remainder;
            }
            level++;
            //checks how much exp will be needed for the next level
            calculateReqExp();
            gameEvents.current.changeLevelUI();
        }

        if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            increaseExpAmount(10);
        }
    }

    public void increaseExpAmount(int e)
    {
        XPthisLevel += e;
        totalXP += e;
    }

    private void calculateReqExp()
    {
        requiredXP = level * 50;
    }
    
    public void resetStat()
    {
        level = 1;

    }
}
