using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileInstance : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject currantWeapon;
    [SerializeField] static public GameObject bulletStartPoint;

    [SerializeField] protected Rigidbody myRb;

    [SerializeField] protected float damage;

    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        myRb.AddForce(projectileWeapons.spawnPoint.transform.right * 2000.0f);
        timer += 10;
    }

    // Update is called once per frame
    void Update()
    {
        projectileDestruction();
    }
    protected void Init()
    {
        myRb = GetComponent<Rigidbody>();
        damage = 10.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        currantWeapon = player.GetComponent<playerWeaponManager>().myCurrantWeapon;
        

        transform.parent = null;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().takeDamage(damage);
            Destroy(gameObject);
        }
    }

    void projectileDestruction()
    {
        if(timer < 0.5f)
        {
            Debug.Log("destroying projectile");
            Destroy(gameObject);
        }
        else if(timer < 8.0f)
        {
            transform.parent = null;
        }
        else
        {
            timer -= Time.deltaTime;

        }
    }
}
