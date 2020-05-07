using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCar : MonoBehaviour
{
    Rigidbody rb;
    float LastTimeChecked;

    void rightCar()
    {
        this.transform.position += Vector3.up;
        this.transform.rotation = Quaternion.LookRotation(this.transform.forward);
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!RaceMonitor.racing)
        {
            
            return;
        }
        if (transform.up.y > 0.5f || rb.velocity.magnitude >1)
        {
            LastTimeChecked = Time.time;
            
        }

        if (Time.time > LastTimeChecked + 3)
        {
           rightCar();
        }

    }
}
