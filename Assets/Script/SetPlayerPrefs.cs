using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Game1_dialogueStarted", 0);
        PlayerPrefs.SetInt("Game2_dialogueStarted", 0);
        PlayerPrefs.SetInt("Game3_dialogueStarted", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
