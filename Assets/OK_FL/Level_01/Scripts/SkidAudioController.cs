using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidAudioController : MonoBehaviour
{

    public float currentFrictionValue = 0.2f;
    public float skidAt = 30f;
    public GameObject skidAudio;

    FMOD.Studio.EventInstance skid;
    Rigidbody rb;

    public bool isSkidding = false;




    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
       

        // parameters



    }

    // Start is called before the first frame update
    void Start()
    {
        skid = FMODUnity.RuntimeManager.CreateInstance("event:/skid");
        skidAudio.SetActive(false);
        //FMOD.Studio.EventInstance skid = FMODUnity.RuntimeManager.CreateInstance("event:/carSkid");


    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        
        PlayerSkidSound();
        if (isSkidding)
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(skid, GetComponent<Transform>(), rb);
            skid.start();
            skidAudio.SetActive(true);
        }

        else
        {
            skidAudio.SetActive(false);
            skid.release();
        }
        //WheelHit hit;
        //WheelCollider collider;
        //collider = GetComponent<WheelCollider>();
        //collider.GetGroundHit(out hit);
        //currentFrictionValue = Mathf.Abs(hit.sidewaysSlip) * 100;



    }

    void PlayerSkidSound()
    {

        WheelHit hit;
        WheelCollider collider;
        collider = GetComponent<WheelCollider>();
        collider.GetGroundHit(out hit);
        currentFrictionValue = Mathf.Abs(hit.sidewaysSlip) * 100;

        //skidval = currentFrictionValue;
        //carSkid.setParameterByName("SkidVal", skidval);

        //Debug.Log("SKID_VAL_SAC" + currentFrictionValue.ToString());
        //FMOD.Studio.PLAYBACK_STATE PbState;
        //skid.getPlaybackState(out PbState);
        if (currentFrictionValue >= skidAt && !isSkidding) //&& PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            
            
            isSkidding = true;
               Debug.Log("I AM SKIDDING");
            //if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            //{
            //    
            //    skid.start();
            //    Debug.Log("I AM SKIDDING");

            //    isSkidding = true;
            //}




        }
        if (currentFrictionValue <= skidAt) //&& PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            isSkidding = false;
            //Debug.Log("I AM SKIDDING NOT");
            //skid.release();
            //skid.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            //else
            //{

            //    
            //}


        }
    }
    
}
