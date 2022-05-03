using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileInstance : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject currantWeapon;
    [SerializeField] protected GameObject bulletStartPoint;

    [SerializeField] protected Rigidbody myRb;

    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        //transform.position = bulletStartPoint.transform.position;
        //transform.rotation = Camera.main.GetComponent<Transform>().rotation;
        //myRb.AddForce(Vector3.forward * 50.0f);
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

        player = GameObject.FindGameObjectWithTag("Player");
        currantWeapon = player.GetComponent<playerWeaponManager>().myCurrantWeapon;
        bulletStartPoint = currantWeapon.transform.GetChild(0).gameObject;

        //transform.parent = null;
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
