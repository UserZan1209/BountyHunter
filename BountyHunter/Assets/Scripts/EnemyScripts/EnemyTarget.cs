using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTarget : EnemyController
{
    [SerializeField] GameObject gameManager;
    [SerializeField] public string nextSceneToLoad;

    float timer;
    bool isTargetAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager");
        AIMode = AImode.ranged;

        if(gameManager == null)
        {
            Debug.Log(gameObject.name + "cannot find the game manager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && isTargetAlive)
        {
            gameManager.GetComponent<GameManager>().killTarget();
            this.GetComponent<Animator>().enabled = false;
            timer += 10.0f;

            isTargetAlive = false;
        }

        if(timer < 0.5f && !isTargetAlive)
        {
            SceneManager.LoadScene(nextSceneToLoad);
        }
        else if(timer >= 0.5f && !isTargetAlive)
        {
            timer -= Time.deltaTime;
            //Debug.Log(timer);
        }
        checkForRagdoll();
        moveToPlayer();
    }

    override public void takeDamage(float d)
    {
        health -= d;
    }
}
