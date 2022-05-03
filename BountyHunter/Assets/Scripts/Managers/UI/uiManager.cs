using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] private Text uiHealthText;

    //UI bars
    [SerializeField] protected Image expBar;
    [SerializeField] protected Image healthBar;
    [SerializeField] protected Image staminaBar;
    [SerializeField] public Image ammoBar;

    [SerializeField] protected Image icon;

    //Ui level display
    [SerializeField] protected Text levelText;
    [SerializeField] protected Text magazinesText;

    [SerializeField] protected GameObject bottomLeftUI;


    //NPC interface
    [SerializeField] protected GameObject NPCUI;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bottomLeftUI.SetActive(false);
        //NPCUI.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        uiHealthText.text = player.gameObject.GetComponent<playerMovement>().health.ToString();

        //change these functions to be triggered by other objects
        calculateExpBar();
        calculateHealthBar();
        calculateStaminaBar();
        displayLevelToUI();
    }

    void displayLevelToUI()
    {
        levelText.text = player.GetComponent<playerProgressionn>().level.ToString();
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

    //criteria
    //npc ui
    //trigger on and off

    void enableDisableNPCUI()
    {

    }
}
