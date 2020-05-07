using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    public GameObject brakeLight;

    public Circuit circuit;
    public float brakingSensitivity = 1.1f;
    public float steeringSensitivity = 0.01f;
    public float accelSensitivity = 0.1f;
    public float treshhold = 2f;

    GameObject tracker;
    int currentTrackerWP = 0;
    public float lookAhead = 15;

    Drive ds;
    
    Vector3 target;
    Vector3 nextTarget;
    int currentWP = 0;
    float totalDistanceToTarget;

    float lastTimeMoving = 0;
    



    // Start is called before the first frame update
    void Start()
    {
        ds = this.GetComponent<Drive>();
        target = circuit.waypoints[currentWP].transform.position;
        nextTarget = circuit.waypoints[currentWP + 1].transform.position;

        totalDistanceToTarget = Vector3.Distance(target, ds.rb.gameObject.transform.position);

        //To follow the tracking object
        tracker = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.position = ds.rb.gameObject.transform.position;
        tracker.transform.rotation = ds.rb.gameObject.transform.rotation;

        brakeLight.SetActive(false);
        //this.GetComponent<Ghost>().enabled = false;


        
    }


    void ProgressTracker()
    {
        //DEBUG
        //Debug.DrawLine(ds.rb.gameObject.transform.position, tracker.transform.position);

        if (Vector3.Distance(ds.rb.gameObject.transform.position, tracker.transform.position) > lookAhead) return;

        tracker.transform.LookAt(circuit.waypoints[currentTrackerWP].transform.position);
        //speed of tracker
        tracker.transform.Translate(0, 0, 1.0f);

        if(Vector3.Distance(tracker.transform.position, circuit.waypoints[currentTrackerWP].transform.position) < 1)
        {
            currentTrackerWP++;
            if (currentTrackerWP >= circuit.waypoints.Length)
            {
                currentTrackerWP = 0;
            }
            
                
        }

    }

    void ResetLayer()
    {
        ds.gameObject.layer = 0;
        //this.GetComponent<Ghost>().enabled = false;
    }

    // Update is called once per frame
    //bool isJump = false;


    void Update()
    {
        if (!RaceMonitor.racing)
        {

            lastTimeMoving = Time.time;
            ds.Go(0, 0, 0);

            return;
        }

        ProgressTracker();
        Vector3 localTarget;
        float targetAngle;

        //Vector3 nextLocalTarget = ds.rb.gameObject.transform.InverseTransformPoint(nextTarget);

        if(ds.rb.velocity.magnitude > 1 )
        {
            lastTimeMoving = Time.time;
        }

        if(Time.time > lastTimeMoving + 4 )
        {
            ds.rb.gameObject.transform.position = circuit.waypoints[currentTrackerWP-1].transform.position + Vector3.up *2 + new Vector3(Random.Range(-3,3),0 , Random.Range(-1,1));
            tracker.transform.position = ds.rb.gameObject.transform.position;
            ds.rb.gameObject.layer = 9;
            //this.GetComponent<Ghost>().enabled = true;
            Invoke("ResetLayer", 3);
        }

        //float distanceToTarget = Vector3.Distance(target, ds.rb.gameObject.transform.position);
        if(Time.time < ds.rb.GetComponent<AvoidDetector>().avoidTime)
        {
            localTarget = tracker.transform.right * ds.rb.GetComponent<AvoidDetector>().avoidPath;
            
        }
        else
        {
            localTarget = ds.rb.gameObject.transform.InverseTransformPoint(tracker.transform.position);
            
        }

        targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;


        //float nextTargetAngle = Mathf.Atan2(nextLocalTarget.x, nextLocalTarget.z) * Mathf.Rad2Deg;

        float steer = Mathf.Clamp(targetAngle * steeringSensitivity, -1, 1) * Mathf.Sign(ds.currentSpeed);

        float speedFactor = ds.currentSpeed / ds.maxSpeed;

        float corner = Mathf.Clamp(Mathf.Abs(targetAngle), 0, 90);
        float cornerFactor = corner / 90.0f;

        float brake = 0;
        if(corner > 10 && speedFactor > 0.05f)
        {
            brake = Mathf.Lerp(0, 1 + speedFactor * brakingSensitivity, cornerFactor);
        }


        float accel = 0.1f;
        if (corner > 20 && speedFactor > 0.2f)
        {
            accel = Mathf.Lerp(0, 1 * accelSensitivity, 1 - cornerFactor);
        }


        //float distanceFactor = distanceToTarget / totalDistanceToTarget;

        //float speedFactor = ds.currentSpeed / ds.maxSpeed;


        //float accel = 0.1f;
        //float accel = Mathf.Lerp(accelSensitivity, 0.1f, distanceFactor);
        //float brake = Mathf.Lerp((-1 - Mathf.Abs(nextTargetAngle)) * brakingSensitivity , 1 + speedFactor, 1 - distanceFactor);






        ds.Go(accel, steer, brake);

        if(brake > 0.2)
        {
            brakeLight.SetActive(true);
        }
        else
        {
            brakeLight.SetActive(false);
        }

        

        ds.CheckForSkid();

        ds.CalculateEngineSound();

        
    }
}
