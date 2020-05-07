using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_controler : MonoBehaviour
{
    CanvasGroup canvasGroup;
    float HUDsetting = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        if (PlayerPrefs.HasKey("HUD"))
        {
            HUDsetting = PlayerPrefs.GetFloat("HUD");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (RaceMonitor.racing)
        {
            canvasGroup.alpha = HUDsetting;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            canvasGroup.alpha = canvasGroup.alpha == 1 ? 0 : 1;
            HUDsetting = canvasGroup.alpha;
            PlayerPrefs.SetFloat("HUD", canvasGroup.alpha);
        }
    }
}
