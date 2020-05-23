using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODPLayer : MonoBehaviour
{
    FMOD.Studio.EventInstance car;
    FMOD.Studio.EventInstance carhit;
    //FMOD.Studio.EventInstance carSkid;

    
    private float revs;
    private float vol = 1f;
    private float skidval;

    public float maxSpeed = 40f;
    public Drive vehicleMovement;

    Rigidbody rb;
    public CountDownManager cdm;
    public SkidAudioController sac;


    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        car = FMODUnity.RuntimeManager.CreateInstance("event:/car");
        carhit = FMODUnity.RuntimeManager.CreateInstance("event:/test");
        //carSkid = FMODUnity.RuntimeManager.CreateInstance("event:/carSkid");


        // parameters
        car.getParameterByName("RPM", out revs);
        car.getParameterByName("VOL", out vol);
        //carSkid.getParameterByName("SKID", out skidval);
    }

    void Start()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(car, GetComponent<Transform>(), rb);

        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(carSkid, GetComponent<Transform>(), rb);



        car.start();
        //carSkid.start();

    }

    // Update is called once per frame
    void Update()
    {

        revs = vehicleMovement.currentSpeed / maxSpeed;
        car.setParameterByName("RPM", revs);

        
    }

    void FixedUpdate()
    {
        SkidValue();
    }

    private void OnDestroy()
    {
        car.release();
        //carSkid.release();
    }

    public void OnCollisionEnter(Collision collision)
    {


        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "car")
        {
            //carhit.start();

            FMOD.Studio.EventInstance carhit = FMODUnity.RuntimeManager.CreateInstance("event:/hits");
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(carhit, GetComponent<Transform>(), rb);
            carhit.start();
            carhit.release();
        }
          

        if (cdm.timer <= 0.0f)
        {
            //Debug.Log("OVERRRR!!");
            car.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            car.release();

        }

        



    }
    void SkidValue()
    {
        skidval = sac.currentFrictionValue;
        //carSkid.setParameterByName("SKID", skidval);
        Debug.Log("SKID_VAL_" + sac.currentFrictionValue.ToString());
    }

}


    
        


