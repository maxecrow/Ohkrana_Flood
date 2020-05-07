using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuAwake : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject optionMenu;


    void Awake()
    {
        startMenu.SetActive(true);
        optionMenu.SetActive(false);

    }
}
