using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : Enemy
{
    [SerializeField] GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager");
        if(gameManager == null)
        {
            Debug.Log(gameObject.name + "cannot find the game manager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            gameManager.GetComponent<GameManager>().killTarget();
            Destroy(gameObject);
        }
    }

    override public void takeDamage(float d)
    {
        health -= d;

    }
}
