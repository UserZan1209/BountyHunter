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
    private void Start()
    {
        gameManager = gameEvents.current.gameObject;
        gameEvents.onTargetDeath += onTargetDeath;

        AIMode = AImode.target;

        if(gameManager == null)
        {
            Debug.Log(gameObject.name + "cannot find the game manager");
        }

        getPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && isTargetAlive)
        {
            gameEvents.current.mainMissionEnd();
            this.GetComponent<Animator>().enabled = false;
            timer += 10.0f;
            isTargetAlive = false;
        }

        if(timer < 0.5f && !isTargetAlive)
        {
            //Loads next scene
            gameEvents.current.targetDeath();
        }
        else if(timer >= 0.5f && !isTargetAlive)
        {
            timer -= Time.deltaTime;
        }
        checkForRagdoll();
        moveToPlayer();
    }

    override public void takeDamage(float d)
    {
        health -= d;
    }

    protected void onTargetDeath()
    {
        SceneManager.LoadScene(nextSceneToLoad);
    }
}
