using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODPLayer : MonoBehaviour
{
    FMOD.Studio.EventInstance car;
    FMOD.Studio.EventInstance carhit;

    private float revs;
    private float vol = 1f;
    public float maxSpeed = 40f;
    public Drive vehicleMovement;

    Rigidbody rb;
    CountDownManager CDM;
    

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        car = FMODUnity.RuntimeManager.CreateInstance("event:/car");
        //carhit = FMODUnity.RuntimeManager.CreateInstance("event:/test");

        // parameters
        car.getParameterByName("RPM", out revs);
        car.getParameterByName("VOL", out vol);
        
    }

    void Start()
    {

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(car, GetComponent<Transform>(), rb);
        
        car.start();


    }

    // Update is called once per frame
    void Update()
    {
        revs = vehicleMovement.currentSpeed / maxSpeed;
        car.setParameterByName("RPM", revs);
    }


    private void OnDestroy()
    {
        car.release();
    }

    public void OnCollisionEnter(Collision collision)
    {


        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "car")
        {
            //carhit.start();
            Debug.Log("IS HITT");
            FMOD.Studio.EventInstance carhit = FMODUnity.RuntimeManager.CreateInstance("event:/test");
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(carhit, GetComponent<Transform>(), rb);
            carhit.start();
            //carhit.release();
        }
    }





}
