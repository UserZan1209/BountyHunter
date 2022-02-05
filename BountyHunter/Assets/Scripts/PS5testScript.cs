using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PS5testScript : MonoBehaviour
{
    [SerializeField]float ah;
    [SerializeField]float av;
    private void Update()
    {
        ah = Input.GetAxis("Horizontal");
        av = Input.GetAxis("Vertical");
        transform.Translate(ah * 3 * Time.deltaTime, 0, av * 3 * Time.deltaTime);
        if(Input.GetAxis("A/Cross") == 1)
        {
            print("a");
        }
    }
    
}
