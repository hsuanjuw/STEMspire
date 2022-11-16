using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    public enum ChargeMode
    {
        Broken,
        Fixed
    };

    public ChargeMode currentCharge = ChargeMode.Broken;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStatus == MiniGameManager.GameStatus.InProgress)
        {
            if(leftFill.fillStatus == PowerFill.FillStatus.NotFilled)
                Countdown("Left");
            else
            {
                TimeReStart("Left");
                GameObject.Find("LCountDownTxt").transform.Find("Circle").GetComponent<Image>().color = Color.green;
                GameObject.Find("LCountDownTxt").transform.Find("Circle").GetComponent<Image>().fillAmount = 1f;
            }
            if(rightFill.fillStatus == PowerFill.FillStatus.NotFilled)
                Countdown("Right");
            else
            {
                TimeReStart("Right");
                GameObject.Find("RCountDownTxt").transform.Find("Circle").GetComponent<Image>().color = Color.green;
                GameObject.Find("RCountDownTxt").transform.Find("Circle").GetComponent<Image>().fillAmount = 1f;
            }
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
        GameObject.Find("PowerPanel").transform.Find("Spotlight").GetComponent<Spotlight>().StartFlashing();
        
        ResetPower("Start");
    }

    public void RestartGame()
    {
        ResetPower("End");
    }

    public void ResetPower(string mode)
    {
        TimeReStart(mode);
        /*
        if(currentCharge == ChargeMode.Broken)
        TimeReStart(mode);
        else TimeReStart("Fixed");
        */
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
            case "Fixed":
                leftSwitchTimeRemaining = 0;
                leftDangerLight.SetActive(true);
                leftOnLight.SetActive(false);
                DisplayTime(leftSwitchTimeRemaining, "LCountDownTxt",leftSwitchTime);
                rightSwitchTimeRemaining = 0;
                rightOnLight.SetActive(false);
                rightDangerLight.SetActive(true);
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
                    if(currentCharge == ChargeMode.Broken)
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
                    if(currentCharge == ChargeMode.Broken)
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
        if (hintActive)
        {
            GameObject.Find(countdownSide).transform.Find("Circle").GetComponent<Image>().fillAmount = timeToDisplay / totalTime;
            if(timeToDisplay / totalTime < 0.5f)
            GameObject.Find(countdownSide).transform.Find("Circle").GetComponent<Image>().color = Color.red;
            else GameObject.Find(countdownSide).transform.Find("Circle").GetComponent<Image>().color = Color.white;
        }
            
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
        TimeReStart("Left");
        TimeReStart("Right");
        GameObject.Find("LCountDownTxt").transform.Find("Circle").GetComponent<Image>().color = Color.green;
        GameObject.Find("LCountDownTxt").transform.Find("Circle").GetComponent<Image>().fillAmount = 1f;
        GameObject.Find("RCountDownTxt").transform.Find("Circle").GetComponent<Image>().color = Color.green;
        GameObject.Find("RCountDownTxt").transform.Find("Circle").GetComponent<Image>().fillAmount = 1f;
        
        currentStatus = MiniGameManager.GameStatus.Completed;
        //FindObjectOfType<MiniGameManager>().CallSuccess();
    }

}
