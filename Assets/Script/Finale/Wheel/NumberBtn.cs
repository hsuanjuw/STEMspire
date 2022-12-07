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
            if(wheel.buttonToClick == num)
                SetGreen();
            else SetRed();
            wheel.ButtonClicked(num);
        }
        else if (wheel.currentStatus == MiniGameManager.GameStatus.Completed) ;
        else SetYellow();
    }

    public void SetGreen()
    {
        GetComponent<Image>().color = Color.green;
    }
    public void SetRed()
    {
        GetComponent<Image>().color = Color.red;
    }

    public void SetYellow()
    {
        GetComponent<Image>().color = Color.yellow;
        StartCoroutine(DelayRed());
    }

    private IEnumerator DelayRed()
    {
        yield return new WaitForSeconds(1f);
        ResetColor();
    }
    public void ResetColor()
    {
        SetRed();
        StartCoroutine(ResetFull());
    }

    private IEnumerator ResetFull()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Image>().color = Color.white;
    }
}
