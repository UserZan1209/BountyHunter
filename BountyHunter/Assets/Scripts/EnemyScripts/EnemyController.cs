using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : Enemy
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject player;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image staminaBar;

    protected enum AImode { def ,melee, ranged}
    [SerializeField] protected AImode AIMode = AImode.def;

    [SerializeField] protected GameObject weapon;

    [SerializeField] protected float timer2;
    [SerializeField] protected bool waitingForWakeUp;
    // Start is called before the first frame update
    void Start()
    {
        InitMe();
        waitingForWakeUp = false;

        if(weapon == null || weapon.gameObject.name == "weaponMelee")
        {
            AIMode = AImode.melee;
        }
        else
        {
            AIMode = AImode.ranged;
        }

        agent = GetComponent<NavMeshAgent>();
        checkForPlayer();
        
    }

    // Update is called once per frame
    void Update()
    {

        checkForRagdoll();
        enableDisableRagdoll();
        moveToPlayer();

        if (Input.GetKeyDown(KeyCode.H))
        {
            useStamina(10.0f);
        }


    }

    protected void checkForRagdoll()
    {
        if (health <= 0 && my_life_state != LifeState.isdead)
        {
            // the object is currently being destroyed at 0 health in "Enemy.cs"
            toggleRagdoll();
            my_life_state = LifeState.isdead;
            player.GetComponent<playerProgressionn>().increaseExpAmount(baseExp);
        }

        if(stamina < maxstamina)
        {
            stamina += Time.deltaTime * 10.0f;
            staminaBar.GetComponent<Image>().fillAmount = stamina / 100;
        }
        if(stamina <= 0)
        {
            toggleRagdoll();
            timer2 += 5.0f;
            
            stamina = 30.0f;
            waitingForWakeUp = true;
        }

        if (waitingForWakeUp)
        {
            agent.SetDestination(transform.position);
            timer2 -= Time.deltaTime;
            if(timer2 <= 0)
            {
                toggleRagdoll();
                waitingForWakeUp = false;
            }
        }
    }

    protected void enableDisableRagdoll()
    {
        if (myAnim.enabled == false)
        {
            my_ragdoll_state = RagdollState.isRagdoll;
            my_movement_state = MovementState.isPhysics;
        }
        else if (myAnim.enabled == true)
        {
            my_ragdoll_state = RagdollState.isNotRagdoll;
            my_movement_state = MovementState.isNotPhysics;
        }
    }

    protected void checkForPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected void moveToPlayer()
    {
        if (player != null && my_life_state == LifeState.isalive)
        {

            // add a condition for weapons and use this as the default when ammo is depleted or no 
            // weapon is equiped.
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            switch (AIMode)
            {
                case AImode.melee:
                    if (distanceToPlayer < 10.0f && distanceToPlayer > 1.0f)
                    {
                        if (!waitingForWakeUp)
                        {
                            agent.SetDestination(player.transform.position);
                            meleeCombat();
                        }
                    }
                    break;
                case AImode.ranged:
                    if (distanceToPlayer < 1.5f)
                    {
                        AIMode = AImode.melee;
                    }
                    else
                    {
                        rangedCombat();  
                    }
                    break;
            }
        }
    }

    override public void takeDamage(float d)
    {
        health -= d;
        healthBar.GetComponent<Image>().fillAmount = health/10;
    }

    public void useStamina(float s)
    {
        stamina -= s;
        staminaBar.GetComponent<Image>().fillAmount = stamina / 100;
    }

    protected void meleeCombat()
    {
        float timer = 0;
        if(timer < 0)
        {
            print("attacking player");
            timer += 4.5f;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    protected void rangedCombat()
    {
        Debug.Log("ranged");
    }
}

 