using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public int lap = 0;
    public int checkpoint = -1;

    int checkPointCount;
    int nextCheckPoint;
    public GameObject lastCP;

    public float timeEntered = 0;

    List<GameObject> children = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] cps = GameObject.FindGameObjectsWithTag("CheckPoint");

        checkPointCount = cps.Length;

        foreach (GameObject c in cps)
        {
            if (c.name == "0")
            {
                lastCP = c;
                break;
            }
        }


    }

     void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "CheckPoint")
        {
            int thisCPNumber = int.Parse(col.gameObject.name);
            if(thisCPNumber == nextCheckPoint)
            {
                lastCP = col.gameObject;
                checkpoint = thisCPNumber;
                timeEntered = Time.time;

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
