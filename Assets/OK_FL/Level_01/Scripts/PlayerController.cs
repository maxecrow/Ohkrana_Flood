using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    Drive ds;
    float lastTimeMoving = 0;
    Vector3 lastPosition;
    Quaternion lastRotation;

    CheckPointManager cpm;



    // Start is called before the first frame update
    void Start()
    {
        ds = this.GetComponent<Drive>();
        //this.GetComponent<Ghost>().enabled = false;
        lastPosition = ds.rb.gameObject.transform.position;
        lastRotation = ds.rb.gameObject.transform.rotation;



    }

    void Update()
    {
        

            float a = Input.GetAxis("Vertical");
        float s = Input.GetAxis("Horizontal");
        float b = Input.GetAxis("Jump");


        //if (Time.time > lastTimeMoving + 4)
        //{
        //    if (cpm == null)
        //    {
        //        cpm = ds.rb.GetComponent<CheckPointManager>();
        //    }

        //    ds.rb.gameObject.transform.position = cpm.lastCP.transform.position + Vector3.up * 2;
        //    ds.rb.gameObject.transform.rotation = cpm.lastCP.transform.rotation;
        //    ds.rb.gameObject.layer = 9;

        //    Invoke("ResetLayer", 3);
        //}


        if (!RaceMonitor.racing)
        {
            a = 0;

        }


        ds.Go(a, s, b);

        //ds.CheckForSkid();

        //ds.CalculateEngineSound();

    }
}
