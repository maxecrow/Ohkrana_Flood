using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public int lap = 0;
    public int checkpoint = -1;
    int checkPointCount;
    int nextCheckPoint;

    // Start is called before the first frame update
    void Start()
    {
        checkPointCount = GameObject.FindGameObjectsWithTag("checkpoint").Length;
    }

     void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "checkpoint")
        {
            int thisCPNumber = int.Parse(col.gameObject.name);
            if(thisCPNumber == nextCheckPoint)
            {
                checkpoint = thisCPNumber;
                if (checkpoint == 0) lap++;

                nextCheckPoint++;
                if(nextCheckPoint >= checkPointCount)
                {
                    nextCheckPoint = 0;
                }
            }
        }
    }

    
}
