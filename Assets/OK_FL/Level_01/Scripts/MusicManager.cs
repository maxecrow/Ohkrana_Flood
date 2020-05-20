using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public GameObject music;
    public CountDownManager CDM;
    // Start is called before the first frame update
    void Start()
    {
        music.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(CDM.thislevelisover == true)
        {
            music.SetActive(false);
            Debug.Log("MAKE MUSIC");
        }
        
        
    }

    public void isMusic()
    {
        music.SetActive(false);
    }
}
