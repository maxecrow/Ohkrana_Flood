﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMonitor : MonoBehaviour
{
    public GameObject[] countDownItems;
    public static bool racing = false;
    
    // Start is called before the first frame update
    //void Start()
    //{
    //    foreach(GameObject g in countDownItems)
    //    {
    //        g.SetActive(false);
    //        StartCoroutine(PlayCountDown());
    //    }
    //}

    //IEnumerator PlayCountDown()
    //{
    //    yield return new WaitForSeconds(2);
    //    foreach(GameObject g in countDownItems)
    //    {
    //        g.SetActive(true);
    //        yield return new WaitForSeconds(1.0f);
    //        g.SetActive(false);
    //    }
    //    racing = true;

    //}
    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void driveStart()
    {
        racing = true;
    }

    public void driveStop()
    {
        racing = false;
    }
}
