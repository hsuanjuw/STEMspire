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
    public bool hintTextActive = false;
    private void Start()
    {
        if (hintTextActive)
        {
            DisplayTime(leftSwitchTime, GameObject.Find("LCountDownTxt").GetComponent<Text>());
            DisplayTime(rightSwitchTime, GameObject.Find("RCountDownTxt").GetComponent<Text>());
        }
        else
        {
            GameObject.Find("LCountDownTxt").GetComponent<Text>().text = "";
            GameObject.Find("RCountDownTxt").GetComponent<Text>().text = "";
        }
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
                if(hintTextActive)
                    DisplayTime(leftSwitchTimeRemaining, GameObject.Find("LCountDownTxt").GetComponent<Text>());
                break;
            case "Right":
                rightSwitchTimeRemaining = rightSwitchTime;
                rightOnLight.SetActive(true);
                rightDangerLight.SetActive(false);
                if(hintTextActive)
                    DisplayTime(rightSwitchTimeRemaining, GameObject.Find("RCountDownTxt").GetComponent<Text>());
                break;
            case "Start":
                leftSwitchTimeRemaining = leftSwitchTime;
                leftDangerLight.SetActive(false);
                leftOnLight.SetActive(true);
                if(hintTextActive)
                    DisplayTime(leftSwitchTimeRemaining, GameObject.Find("LCountDownTxt").GetComponent<Text>());
                rightSwitchTimeRemaining = rightSwitchTime;
                rightOnLight.SetActive(true);
                rightDangerLight.SetActive(false);
                if(hintTextActive)
                    DisplayTime(rightSwitchTimeRemaining, GameObject.Find("RCountDownTxt").GetComponent<Text>());
                break;
            case "End":
                leftSwitchTimeRemaining = leftSwitchTime;
                leftDangerLight.SetActive(true);
                leftOnLight.SetActive(false);
                if(hintTextActive)
                    DisplayTime(leftSwitchTimeRemaining, GameObject.Find("LCountDownTxt").GetComponent<Text>());
                rightSwitchTimeRemaining = rightSwitchTime;
                rightOnLight.SetActive(false);
                rightDangerLight.SetActive(true);
                if(hintTextActive)
                    DisplayTime(rightSwitchTimeRemaining, GameObject.Find("RCountDownTxt").GetComponent<Text>());
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
                        StartCoroutine(LeftFlash(leftDangerLight.transform.parent.Find("powerCoilYellow").GetComponent<SpriteRenderer>()));
                    }
                    leftFill.FillNextImage(Time.deltaTime * fillSpeed);
                }
                if (hintTextActive)
                {
                    Text text = GameObject.Find("LCountDownTxt").GetComponent<Text>();
                    DisplayTime(rightSwitchTimeRemaining, text);
                }
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
                        StartCoroutine(RightFlash(rightDangerLight.transform.parent.Find("powerCoilBlue").GetComponent<SpriteRenderer>()));
                    }
                    rightFill.FillNextImage(Time.deltaTime * fillSpeed);
                }
                if (hintTextActive)
                {
                    Text text = GameObject.Find("RCountDownTxt").GetComponent<Text>();
                    DisplayTime(rightSwitchTimeRemaining, text);
                }
                break;
        }

        
    }

    private void DisplayTime(float timeToDisplay, Text timeText)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}", seconds);
    }
    
    public void Fail()
    {
        Debug.Log("Power Failed");
        DisplayTime(0, GameObject.Find("RCountDownTxt").GetComponent<Text>());
        DisplayTime(0, GameObject.Find("LCountDownTxt").GetComponent<Text>());

        currentStatus = MiniGameManager.GameStatus.Failed;
        FindObjectOfType<MiniGameManager>().CallRestart();
    }

    public void Succeed()
    {
        currentStatus = MiniGameManager.GameStatus.Completed;
        if (hintTextActive)
        {
            GameObject.Find("LCountDownTxt").GetComponent<Text>().text = "";
            GameObject.Find("RCountDownTxt").GetComponent<Text>().text = "";
        }
        FindObjectOfType<MiniGameManager>().CallSuccess();
    }

}
