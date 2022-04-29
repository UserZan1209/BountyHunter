using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProgressionn : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] public float totalXP;
    [SerializeField] public float requiredXP;
    [SerializeField] public float XPthisLevel;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        calculateReqExp();
    }

    // Update is called once per frame
    void Update()
    {
        if (totalXP >= requiredXP)
        {
            level++;
            XPthisLevel = 0;
            calculateReqExp();
        }
    }

    public void increaseExpAmount(int e)
    {
        XPthisLevel += e;
        totalXP += e;
    }

    private void calculateReqExp()
    {
        requiredXP = level * 50;
    }
}
