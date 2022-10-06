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
        
        num = (int)Char.GetNumericValue(this.name[7]);
        wheel = GameObject.FindObjectOfType<Wheel>();
    }

    public void btnClicked()
    {
        //Debug.Log(num.ToString() + "Button Clicked");
        if (wheel.gameStarted)
        {
            wheel.ButtonClicked(num);
        }
        
    }
}
