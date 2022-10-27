using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NumberBtn : MonoBehaviour
{
    public int num;
    private Wheel wheel;

    void Start()
    {
        wheel = GameObject.FindObjectOfType<Wheel>();
    }
   

    public void btnClicked()
    {
        //Debug.Log(num.ToString() + "Button Clicked");
        if (wheel.currentStatus == MiniGameManager.GameStatus.InProgress)
        {
            wheel.ButtonClicked(num);
        }
        GetComponent<Animator>().SetTrigger("SpinWheel");
    }
}
