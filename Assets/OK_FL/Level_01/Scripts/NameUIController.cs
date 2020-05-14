﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NameUIController : MonoBehaviour
{
    public Text playerName;
    public Text LapDisplay;
    public Transform target;
    CanvasGroup canvasGroup;
    public Renderer carRend;
    CheckPointManager cpManager;

    

    int carRego; //for the leaderboard

    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        playerName = this.GetComponent<Text>();
        canvasGroup = this.GetComponent<CanvasGroup>();
        carRego = Leaderboard.RegisterCar(playerName.text);
    }

    void LateUpdate()
    {
        if (!RaceMonitor.racing) { canvasGroup.alpha = 0; return; }
        //if (carRend == null) return;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        bool carInView = GeometryUtility.TestPlanesAABB(planes, carRend.bounds);
        canvasGroup.alpha = carInView ? 1 : 0;
        this.transform.position = Camera.main.WorldToScreenPoint(target.position + Vector3.up * 1.2f);

        if (cpManager == null)
        {
            cpManager = target.GetComponent<CheckPointManager>();
        }

        Leaderboard.SetPosition(carRego, cpManager.lap, cpManager.checkpoint, cpManager.timeEntered);
        string position = Leaderboard.GetPosition(carRego);


        //RacePositionText.text = position;
    }
}
