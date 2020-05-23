using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownManager : MonoBehaviour
{
    public TextMeshProUGUI DeltaTimeDisplay;    //The time
    public TextMeshProUGUI currentLapText;      //The text element for the current lap 
    //public TextMeshProUGUI txt_YouWIN;
    //public TextMeshProUGUI txt_YouLoose;
    public GameObject gameWinUI;           //A reference to the UI objects that appears when the game is complete
    public GameObject gameLooseUI;           //A reference to the UI objects that appears when the game is complete
    public GameManager gm;
    public GameObject player;

    //public static int playerScore;  //  Static keyword makes this variable a Member of the class, not of any particular instance.
    public bool thislevelisover;

    [SerializeField] private float timeLimit = 4f;
    
    //[SerializeField] private GameObject title;


    // temp 0.03149
    // on start number of Setlaps eg. 3
    // state bool islap = false
    // number of completed laps (update)
    // if number of completed laps == Setlaps islap = true
    // if time counter = 0 (canCount = false) && islap = true then menu is WIN!
    // else if time counter = 0 (canCount = false) && islap = false then menu is LOOSE!

    public float timer;
    //private bool canCount = true;
    //private bool doOnce = false;
    //private bool islap = false;
    //private bool isWon = false;
    




    void Start()
    {
        timer = timeLimit;
        gameLooseUI.SetActive(false);
        gameWinUI.SetActive(false);
        thislevelisover = false;
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<AiController>().enabled = false;
        //txt_YouLoose.text = "";

    }


    // Update is called once per frame
    public void Update()
    {
        timer -= Time.deltaTime;
        DeltaTimeDisplay.text = timer.ToString("F");

        if (timer > 0.0f && gm.isGameOver)
        {
                        
            //...and show the Game Over UI
            gameWinUI.SetActive(true);
            thislevelisover = true;
            DisablePlayer();

        }


        else if (timer <= 0.0f && !gm.isGameOver)
        {
            //canCount = false;
            //doOnce = true;

            timer = 0.0f;
            //...and show the Game Over UI
            gameLooseUI.SetActive(true);
            DisablePlayer();
            
            
        }



    }

    public void SetLapDisplay(int currentLap, int numberOfLaps)
    {
        //If we are trying to set a lap greater than the total number of laps, exit
        if (currentLap > numberOfLaps)

            return;

        //Update the current lap text
        currentLapText.text = currentLap + "/" + numberOfLaps;
    }

    
    void DisablePlayer()
    {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<AiController>().enabled = true;
        thislevelisover = true;
    }
    
       
}
