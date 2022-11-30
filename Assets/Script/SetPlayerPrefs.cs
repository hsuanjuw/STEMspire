using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerPrefs : MonoBehaviour
{
    /// <summary>
    /// Reset all the PlayerPrefs to 0
    /// </summary>
    /// 

    public Toggle analyticCheckbox;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Game1_DialogueStarted", 0);
        PlayerPrefs.SetInt("Game2_DialogueStarted", 0);
        PlayerPrefs.SetInt("Game3_DialogueStarted", 0);

        // Space Station 1
        PlayerPrefs.SetInt("Zun_Chat", 0);
        //PlayerPrefs.SetInt("Engineer1_Chat", 0);
        PlayerPrefs.SetInt("Bot_Enthusiast_Chat", 0);
        PlayerPrefs.SetInt("Parents_Chat", 0);
        PlayerPrefs.SetInt("Service_Guide_Chat", 0);
        PlayerPrefs.SetInt("Family_Chat", 0);

        // Game 2 Robot status 
        PlayerPrefs.SetInt("Robot_Stay", 0);

        // Analytic 
        PlayerPrefs.SetInt("Analytic", 1);
    }

    public void setAnalyticPrfabs()
    {
        if (analyticCheckbox.isOn)
        {
            PlayerPrefs.SetInt("Analytic", 1);
            //Debug.Log("Checked");
        }
        else
        {
            PlayerPrefs.SetInt("Analytic", 0);
            //Debug.Log("NotChecked");
        }
    }


}
