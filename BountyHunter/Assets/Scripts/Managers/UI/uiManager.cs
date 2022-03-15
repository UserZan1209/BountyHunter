using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] private Text uiHealthText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        uiHealthText.text = player.gameObject.GetComponent<playerMovement>().health.ToString();
    }
}
