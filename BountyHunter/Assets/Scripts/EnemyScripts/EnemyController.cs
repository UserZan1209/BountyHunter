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

    protected enum AImode { def ,melee, ranged}
    [SerializeField] protected AImode AIMode = AImode.def;

    [SerializeField] protected GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        InitMe();

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
            //print(distanceToPlayer);

            switch (AIMode)
            {
                case AImode.melee:
                    if (distanceToPlayer < 10.0f && distanceToPlayer > 1.0f)
                    {
                        agent.SetDestination(player.transform.position);
                        meleeCombat();
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

    protected void meleeCombat()
    {

    }

    protected void rangedCombat()
    {
        Debug.Log("ranged");
    }
}

 