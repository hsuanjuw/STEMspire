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

    private void Start()
    {
        DisplayTime(leftSwitchTime, GameObject.Find("LCountDownTxt").GetComponent<Text>());
        DisplayTime(rightSwitchTime, GameObject.Find("RCountDownTxt").GetComponent<Text>());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStatus == MiniGameManager.GameStatus.InProgress)
        {
            if(leftFill.fillStatus == PowerFill.FillStatus.NotFilled)
                LCountDown();
            if(rightFill.fillStatus == PowerFill.FillStatus.NotFilled)
                RCountDown();
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
        ResetPower();
        currentStatus = MiniGameManager.GameStatus.InProgress;
    }

    public void RestartGame()
    {
        ResetPower();
    }

    public void ResetPower()
    {
        TimeReStart("Left");
        TimeReStart("Right");
        leftFill.ResetFill();
        rightFill.ResetFill();
        currentStatus = MiniGameManager.GameStatus.NotStarted;
    }

    private void TimeReStart(string type)
    {
        switch (type)
        {
            case "Left":
                leftSwitchTimeRemaining = leftSwitchTime;
                leftOnLight.SetActive(true);
                leftDangerLight.SetActive(false);
                DisplayTime(leftSwitchTimeRemaining, GameObject.Find("LCountDownTxt").GetComponent<Text>());
                break;
            case "Right":
                rightSwitchTimeRemaining = rightSwitchTime;
                rightOnLight.SetActive(true);
                rightDangerLight.SetActive(false);
                DisplayTime(rightSwitchTimeRemaining, GameObject.Find("RCountDownTxt").GetComponent<Text>());
                break;
            default:
                Debug.Log("Time restart Error");
                break;
        }
    }

    private void LCountDown()
    {
        if (leftSwitchTimeRemaining > 0)
        {
            leftSwitchTimeRemaining -= Time.deltaTime;
            if (leftSwitchTimeRemaining < 5)
            {
                if (leftDangerLight.activeSelf == false)
                {
                    leftDangerLight.SetActive(true);
                    leftOnLight.SetActive(false);
                }
            }
            leftFill.FillNextImage(Time.deltaTime * fillSpeed);
        }
        else
        {
            Fail();
        }
        Text text = GameObject.Find("LCountDownTxt").GetComponent<Text>();
        DisplayTime(leftSwitchTimeRemaining, text);
    }
    
    private void RCountDown()
    {
        if (rightSwitchTimeRemaining > 0)
        {
            rightSwitchTimeRemaining -= Time.deltaTime;
            if (rightSwitchTimeRemaining < 5)
            {
                if (rightDangerLight.activeSelf == false)
                {
                    rightDangerLight.SetActive(true);
                    rightOnLight.SetActive(false);
                }
            }
            rightFill.FillNextImage(Time.deltaTime * fillSpeed);
        }
        else
        {
            Fail();
        }
        Text text = GameObject.Find("RCountDownTxt").GetComponent<Text>();
        DisplayTime(rightSwitchTimeRemaining, text);
    }

    private void DisplayTime(float timeToDisplay, Text timeText)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}", seconds);
    }
    
    public void Fail()
    {
        Debug.Log("power fail");
        currentStatus = MiniGameManager.GameStatus.Failed;
        FindObjectOfType<MiniGameManager>().CallRestart();
    }

    public void Succeed()
    {
        currentStatus = MiniGameManager.GameStatus.Completed;
        GameObject.Find("LCountDownTxt").GetComponent<Text>().text = "";
        GameObject.Find("RCountDownTxt").GetComponent<Text>().text = "";
        FindObjectOfType<MiniGameManager>().CallSuccess();
    }

}
