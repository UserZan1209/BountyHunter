using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amputator : MonoBehaviour
{
    [SerializeField] protected GameObject enemyTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            enemyTarget = other.gameObject;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.transform.gameObject);

        if(collision.gameObject.transform.gameObject == enemyTarget)
        {
            collision.gameObject.transform.parent = null;
            Debug.Log("detatched");
        }
    }
}
