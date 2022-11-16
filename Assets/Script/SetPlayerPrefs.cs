using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Game1_DialogueStarted", 0);
        PlayerPrefs.SetInt("Game2_DialogueStarted", 0);
        PlayerPrefs.SetInt("Game3_DialogueStarted", 0);

        // Space Station 1
        PlayerPrefs.SetInt("Zun_Chat", 0);
        PlayerPrefs.SetInt("Bot_Enthusiast_Chat", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
