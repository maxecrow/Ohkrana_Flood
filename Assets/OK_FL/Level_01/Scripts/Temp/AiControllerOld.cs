using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiControllerOld : MonoBehaviour
{
    public GameObject brakeLight;
    public Circuit circuit;
    public float brakingSensitivity = 1.1f;
    public float steeringSensitivity = 0.01f;
    public float accelSensitivity = 0.1f;
    public float treshhold = 2f;

    Drive ds;
    
    Vector3 target;
    Vector3 nextTarget;

    int currentWP = 0;
    float totalDistanceToTarget;

    
    

    // Start is called before the first frame update
    void Start()
    {
        ds = this.GetComponent<Drive>();
        target = circuit.waypoints[currentWP].transform.position;
        nextTarget = circuit.waypoints[currentWP + 1].transform.position;

        totalDistanceToTarget = Vector3.Distance(target, ds.rb.gameObject.transform.position);
        brakeLight.SetActive(false);
        
    }

    // Update is called once per frame
    bool isJump = false;


    void Update()
    {
        Vector3 localTarget = ds.rb.gameObject.transform.InverseTransformPoint(target);
        Vector3 nextLocalTarget = ds.rb.gameObject.transform.InverseTransformPoint(nextTarget);



        float distanceToTarget = Vector3.Distance(target, ds.rb.gameObject.transform.position);

        float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        float nextTargetAngle = Mathf.Atan2(nextLocalTarget.x, nextLocalTarget.z) * Mathf.Rad2Deg;

        float steer = Mathf.Clamp(targetAngle * steeringSensitivity, -1, 1) * Mathf.Sign(ds.currentSpeed);

        float distanceFactor = distanceToTarget / totalDistanceToTarget;

        float speedFactor = ds.currentSpeed / ds.maxSpeed;


        //float accel = 0.1f;
        float accel = Mathf.Lerp(accelSensitivity, 0.1f, distanceFactor);
        float brake = Mathf.Lerp((-1 - Mathf.Abs(nextTargetAngle)) * brakingSensitivity , 1 + speedFactor, 1 - distanceFactor);


        //if (Mathf.Abs(nextTargetAngle) > 35)
        //{
        //    brake += 0.2f;
        //    accel -= 0.2f;
        //}

        //Debug.Log("Brake:" + brake + "Accel:" + accel + "Speed:" + ds.rb.velocity.magnitude);

        if (isJump == true)
        {
            accel = 20;
            brake = 0;
            Debug.Log("Is Jumping");
        }



        ds.Go(accel, steer, brake);

        if(brake > 0.2)
        {
            brakeLight.SetActive(true);
        }
        else
        {
            brakeLight.SetActive(false);
        }

        if(distanceToTarget < treshhold)
        {
            currentWP++;
            if (currentWP >= circuit.waypoints.Length)
                currentWP = 0;
            target = circuit.waypoints[currentWP].transform.position;
            if(currentWP == circuit.waypoints.Length - 1)
            {
                nextTarget = circuit.waypoints[0].transform.position;

            }
            else
                nextTarget = circuit.waypoints[currentWP + 1].transform.position;

            totalDistanceToTarget = Vector3.Distance(target, ds.rb.gameObject.transform.position);


            if (ds.rb.gameObject.transform.InverseTransformPoint(target).y > 5)
            {
                isJump = true;
            }
            else
            {
                isJump = false;
            }
        }

        ds.CheckForSkid();

        ds.CalculateEngineSound();

        
    }
}
