using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetScale : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = transform.lossyScale;
    }
}
