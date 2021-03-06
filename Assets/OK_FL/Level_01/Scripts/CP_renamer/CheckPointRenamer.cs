﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class CheckPointRenamer : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Transform[] checkPoints = this.GetComponentsInChildren<Transform>();
        int number = 0;
        foreach (Transform cp in checkPoints)
        {
            if(cp.gameObject != this.gameObject)
            {
                cp.gameObject.name = "" + number;
                number++;
            }
        }
    }
    
}
