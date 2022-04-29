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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        
    }

    // Update is called once per frame
    void Update()
    {
        uiHealthText.text = player.gameObject.GetComponent<playerMovement>().health.ToString();

        //change these functions to be triggered by other objects
        calculateExpBar();
        calculateHealthBar();
        calculateStaminaBar();
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
}
