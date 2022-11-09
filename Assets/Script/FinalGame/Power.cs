using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    public MiniGameManager.GameStatus currentStatus = MiniGameManager.GameStatus.NotStarted;
    public PowerFill leftFill;
    public PowerFill rightFill;

    public GameObject leftOnLight;
    public GameObject leftDangerLight;
    public GameObject rightOnLight;
    public GameObject rightDangerLight;
    
    public float leftSwitchTime = 10f;
    public float rightSwitchTime = 15f;

    public float fillSpeed = 0.05f;
    private float leftSwitchTimeRemaining;
    private float rightSwitchTimeRemaining;

    public float timeAfterGameStart = 3f; // 3f after the game start, the power start to count down;
    public bool hintActive = false;
    private void Start()
    {
        if (hintActive)
        {
            DisplayTime(leftSwitchTime, "LCountDownTxt",leftSwitchTime);
            DisplayTime(rightSwitchTime, "RCountDownTxt",rightSwitchTime);
        }
        /*
         else
        {
            GameObject.Find("LCountDownTxt").GetComponent<Text>().text = "";
            GameObject.Find("RCountDownTxt").GetComponent<Text>().text = "";
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStatus == MiniGameManager.GameStatus.InProgress)
        {
            if(leftFill.fillStatus == PowerFill.FillStatus.NotFilled)
                Countdown("Left");
            if(rightFill.fillStatus == PowerFill.FillStatus.NotFilled)
                Countdown("Right");
            if(rightFill.fillStatus == PowerFill.FillStatus.Filled && leftFill.fillStatus == PowerFill.FillStatus.Filled)
                Succeed();
        }
    }

    public void LeftSwitchOnOff()
    {
        if(currentStatus == MiniGameManager.GameStatus.InProgress)
            TimeReStart("Left");
    }

    public void RightSwitchOnOff()
    {
        if(currentStatus == MiniGameManager.GameStatus.InProgress)
            TimeReStart("Right");
    }

    public void StartGame()
    {
        StartCoroutine(StartWait());
    }

    private IEnumerator StartWait()
    {
        yield return new WaitForSeconds(timeAfterGameStart); // 10 Sec after launch, the wheel game started
        ResetPower("Start");
    }

    public void RestartGame()
    {
        ResetPower("End");
    }

    public void ResetPower(string mode)
    {
        TimeReStart(mode);
        leftFill.ResetFill();
        rightFill.ResetFill();
        if(mode == "End")
            currentStatus = MiniGameManager.GameStatus.NotStarted;
        else if (mode == "Start")
            currentStatus = MiniGameManager.GameStatus.InProgress;
    }

    private void TimeReStart(string type)
    {
        switch (type)
        {
            case "Left":
                leftSwitchTimeRemaining = leftSwitchTime;
                leftOnLight.SetActive(true);
                leftDangerLight.SetActive(false);
                DisplayTime(leftSwitchTimeRemaining, "LCountDownTxt",leftSwitchTime);
                break;
            case "Right":
                rightSwitchTimeRemaining = rightSwitchTime;
                rightOnLight.SetActive(true);
                rightDangerLight.SetActive(false);
                DisplayTime(rightSwitchTimeRemaining, "RCountDownTxt",rightSwitchTime);
                break;
            case "Start":
                leftSwitchTimeRemaining = leftSwitchTime;
                leftDangerLight.SetActive(false);
                leftOnLight.SetActive(true);
                DisplayTime(leftSwitchTimeRemaining, "LCountDownTxt",leftSwitchTime);
                rightSwitchTimeRemaining = rightSwitchTime;
                rightOnLight.SetActive(true);
                rightDangerLight.SetActive(false);
                DisplayTime(rightSwitchTimeRemaining, "RCountDownTxt",rightSwitchTime);
                break;
            case "End":
                leftSwitchTimeRemaining = leftSwitchTime;
                leftDangerLight.SetActive(true);
                leftOnLight.SetActive(false);
                DisplayTime(leftSwitchTime, "LCountDownTxt",leftSwitchTime);
                rightSwitchTimeRemaining = rightSwitchTime;
                rightOnLight.SetActive(false);
                rightDangerLight.SetActive(true);
                DisplayTime(rightSwitchTime, "RCountDownTxt",rightSwitchTime);
                break;
            default:
                Debug.Log("Time restart Error");
                break;
        }
    }
    private IEnumerator LeftFlash(SpriteRenderer flashImg)
    {
        if (flashImg != null)
        {
            while (leftDangerLight.activeSelf && currentStatus == MiniGameManager.GameStatus.InProgress)
            {
                flashImg.color = Color.red;
                yield return new WaitForSeconds(1f);
                flashImg.GetComponent<SpriteRenderer>().color = Color.white;
                yield return new WaitForSeconds(1f);
            } 
        }
    }
    private IEnumerator RightFlash(SpriteRenderer flashImg)
    {
        if (flashImg != null)
        {
            while (rightDangerLight.activeSelf && currentStatus == MiniGameManager.GameStatus.InProgress)
            {
                flashImg.color = Color.red;
                yield return new WaitForSeconds(1f);
                flashImg.GetComponent<SpriteRenderer>().color = Color.white;
                yield return new WaitForSeconds(1f);
            } 
        }
    }

    private void Countdown(string _side)
    {
        switch (_side)
        {
            case "Left":
                leftSwitchTimeRemaining -= Time.deltaTime;
                if (leftSwitchTimeRemaining <= 0)
                {
                    leftSwitchTimeRemaining = 0;
                    Fail();
                }
                else
                {
                    if(leftSwitchTimeRemaining < 10 && leftDangerLight.activeSelf == false)
                    {
                        leftDangerLight.SetActive(true);
                        leftOnLight.SetActive(false);
                        StartCoroutine(LeftFlash(leftDangerLight.transform.parent.Find("LpowerCoil").GetComponent<SpriteRenderer>()));
                    }
                    leftFill.FillNextImage(Time.deltaTime * fillSpeed);
                }
                DisplayTime(leftSwitchTimeRemaining, "LCountDownTxt",leftSwitchTime);
                break;
            case "Right":
                rightSwitchTimeRemaining -= Time.deltaTime;
                if (rightSwitchTimeRemaining <= 0)
                {
                    rightSwitchTimeRemaining = 0;
                    Fail();
                }
                else
                {
                    if(rightSwitchTimeRemaining < 10 && rightDangerLight.activeSelf == false)
                    {
                        rightDangerLight.SetActive(true);
                        rightOnLight.SetActive(false);
                        StartCoroutine(RightFlash(rightDangerLight.transform.parent.Find("RpowerCoil").GetComponent<SpriteRenderer>()));
                    }
                    rightFill.FillNextImage(Time.deltaTime * fillSpeed);
                }
                DisplayTime(rightSwitchTimeRemaining, "RCountDownTxt",rightSwitchTime);
                break;
        }

        
    }

    private void DisplayTime(float timeToDisplay, string countdownSide, float totalTime)
    {
        if(hintActive)
            GameObject.Find(countdownSide).transform.Find("Circle").GetComponent<Image>().fillAmount =
                timeToDisplay / totalTime;
    }
    
    public void Fail()
    {
        Debug.Log("Power Failed");
        DisplayTime(0, "LCountDownTxt",leftSwitchTime);
        DisplayTime(0, "RCountDownTxt",rightSwitchTime);

        currentStatus = MiniGameManager.GameStatus.Failed;
        FindObjectOfType<MiniGameManager>().CallRestart();
    }

    public void Succeed()
    {
        currentStatus = MiniGameManager.GameStatus.Completed;
        if (hintActive)
        {
            DisplayTime(leftSwitchTime, "LCountDownTxt",leftSwitchTime);
            DisplayTime(rightSwitchTime, "RCountDownTxt",rightSwitchTime);
        }
        FindObjectOfType<MiniGameManager>().CallSuccess();
    }

}
