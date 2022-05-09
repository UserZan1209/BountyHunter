using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : Enemy
{
    //references
    [HideInInspector] private NavMeshAgent agent;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject myHead;

    //UI components
    [SerializeField] private Canvas myStatCanvas;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image staminaBar;

    //AI variables
    protected enum AImode { def ,melee, ranged, target}
    [SerializeField] protected AImode AIMode;

    //Combat variables
    [SerializeField] protected GameObject weaponMelee;
    [SerializeField] protected GameObject weaponRanged;
    [HideInInspector] protected float attackTimer;
    [HideInInspector] protected float timer2;
    [HideInInspector] protected bool waitingForWakeUp;

    // Start is called before the first frame update
    void Start()
    {
        InitMe();
        waitingForWakeUp = false;
        attackTimer = 0;
        getPlayer();
        selectAI(weaponRanged);
        agent = GetComponent<NavMeshAgent>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        moveToPlayer();
    }

    private void FixedUpdate()
    {
        checkForRagdoll();
        enableDisableRagdoll();
        statUIController();
    }

    protected void selectAI(GameObject w)
    {
        //using the ranged weapon slot decided AI mode
        if(w != null)
        {
            AIMode = AImode.ranged;
        }
        else
        {
            AIMode = AImode.melee;
        }
    }

    protected void checkForRagdoll()
    {
        if (health <= 0 && my_life_state != LifeState.isdead)
        {
            // the object is currently being destroyed at 0 health in "Enemy.cs"
            toggleRagdoll();
            player.GetComponent<playerProgressionn>().increaseExpAmount(baseExp);
            my_life_state = LifeState.isdead;
            myStatCanvas.enabled = false;   
        }

        if(stamina < maxstamina)
        {
            stamina += Time.deltaTime * 10.0f;
            if(myStatCanvas != null && staminaBar != null)
            {
                staminaBar.GetComponent<Image>().fillAmount = stamina / 100;
            }
            
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
        if(myAnim != null)
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
        else
        {
            myAnim = gameObject.GetComponent<Animator>();
            //Debug.Log("Animator added to references");
        }

    }

    protected void getPlayer()
    {
        player = gameEvents.current.playerObject;
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
                            print("ready to attack");
                            myAnim.SetBool("isAttacking", false);
                            agent.SetDestination(player.transform.position);
                        }
                    }
                    else if(distanceToPlayer <= 1)
                    {
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
                        //add a check if there is no ammo drop weapon and switch to melee
                        rangedCombat();  
                    }
                    break;
            }
        }
    }

    override public void takeDamage(float d)
    {
        health -= d;
        if(myStatCanvas != null && healthBar != null)
        {
            healthBar.GetComponent<Image>().fillAmount = health / 10;
        }
    }
    public void useStamina(float s)
    {
        stamina -= s;
        if(myStatCanvas != null && staminaBar != null)
        {
            staminaBar.GetComponent<Image>().fillAmount = stamina / 100;
        }
    }

    protected void meleeCombat()
    {

        if (attackTimer < 0)
        {
            stamina -= 35.0f;
            print("attacking player");
            myAnim.SetBool("isAttacking",true);
            player.GetComponent<playerMovement>().healPlayer(-5.0f);
            attackTimer += 3.0f;
        }
        else
        {
            if (attackTimer < 0.1f)
            {
                myAnim.SetBool("isAttacking", false);
            }
            attackTimer -= Time.deltaTime;
        }
    }

    protected void rangedCombat()
    {
        //Debug.Log("ranged");
    }

    protected void toggleAnimIsAttacking()
    {
        if(attackTimer != 0)
        {
            print("cant attack");
            
        }
        else
        {
            myAnim.SetBool("isAttacking", !myAnim.GetBool("isAttacking"));
        }
        
    }

    protected void statUIController() 
    {
        if(myStatCanvas != null && player != null)
        {
            //myStatCanvas.transform.LookAt(player.transform.position);
            myStatCanvas.transform.position = new Vector3( myHead.transform.position.x, myHead.transform.position.y + 0.5f, myHead.transform.position.z);    
            
        }
    }
}

 