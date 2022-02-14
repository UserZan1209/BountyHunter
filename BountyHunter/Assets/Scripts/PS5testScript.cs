using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PS5testScript : MonoBehaviour
{
    //Left Joystick Values
    [Header ("Left Joystick values")]
    [SerializeField] private float aH;
    [SerializeField] private float aV;

    private void Update()
    {
        //Movement
        aH = Input.GetAxis("Horizontal");
        aV = Input.GetAxis("Vertical");
        transform.Translate(aH * 3 * Time.deltaTime, 0, aV * 3.5f * Time.deltaTime);
    }
}
