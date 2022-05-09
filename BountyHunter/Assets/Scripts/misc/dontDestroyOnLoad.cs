using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroyOnLoad : MonoBehaviour
{
    void Start()
    {
        for(int i = 0; i < Object.FindObjectsOfType<dontDestroyOnLoad>().Length; i++)
        {
            if(Object.FindObjectsOfType<dontDestroyOnLoad>()[i] != this)
            {
                if(Object.FindObjectsOfType<dontDestroyOnLoad>()[i].name == this.gameObject.name)
                {
                    Destroy(Object.FindObjectsOfType<dontDestroyOnLoad>()[i].gameObject);
                }
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}
