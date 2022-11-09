using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : MonoBehaviour
{
    public MiniGameManager.GameStatus currentStatus = MiniGameManager.GameStatus.NotStarted;
    
    [HideInInspector] public int buttonToClick = 1;
    private int restartButtonValue = 1;
    [HideInInspector] public bool startGame = false;
    public GameObject[] wheelIDs;
    public int numWheels = 5;
    public float timeAfterGameStart = 10f; // 10f after game start, number card flash

    public float completionTime = 20f;
    private float timeRemaining;
    
    Coroutine gameCoroutine;
    public bool hintActive = false;
    private void Start()
    {
        if (hintActive)
            DisplayTime(completionTime);
        else GameObject.Find("WheelCountDownTxt").GetComponent<Text>().text = "";
    }

    void Update()
    {
        if (currentStatus == MiniGameManager.GameStatus.InProgress)
        {
            CountDown();
        }
    }

    public void StartGame()
    {
        gameCoroutine = StartCoroutine(StartNumFlash());
    }

    public void ButtonClicked(int btnClickedNum)
    {
        if (btnClickedNum != buttonToClick)
        {
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("SequenceButton");
            if (btnClickedNum == 1)
            {
                buttonToClick = 2;
            }
            else buttonToClick = 1;
            foreach (var button in buttons)
            {
                if(button.GetComponent<NumberBtn>().num == btnClickedNum && btnClickedNum == 1)
                    button.GetComponent<NumberBtn>().SetGreen();
                else button.GetComponent<NumberBtn>().ResetColor();
            }
        }
        else buttonToClick++;

        if (CheckAllBtnClicked())
        {
            Succeed();
        }
    }

    public bool CheckAllBtnClicked()
    {
        return (buttonToClick > numWheels);
    }

    private IEnumerator StartNumFlash()
    {
        yield return new WaitForSeconds(timeAfterGameStart); // 10 Sec after launch, the wheel game started
        timeRemaining = completionTime;
        currentStatus = MiniGameManager.GameStatus.InProgress;
        while (currentStatus == MiniGameManager.GameStatus.InProgress)
        {
            for (int i = 0; i < wheelIDs.Length; i++)
            {
                wheelIDs[i].GetComponent<SpriteRenderer>().color = new Color(253f,255f,0f,255f);
            }
            yield return new WaitForSeconds(1f);
            if (currentStatus == MiniGameManager.GameStatus.InProgress)
            {
                for (int i = 0; i < wheelIDs.Length; i++)
                {
                    wheelIDs[i].GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private void CountDown()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            Debug.Log("Wheel Failure");
            Fail();
            DisplayTime(0);
        }
        
    }
    private void DisplayTime(float timeToDisplay)
    {
        if(hintActive)
        GameObject.Find("WheelCountDownTxt").transform.Find("Circle").GetComponent<Image>().fillAmount =
            timeToDisplay / completionTime;
    }

    public void RestartGame()
    {
        StopCoroutine(gameCoroutine);
        currentStatus = MiniGameManager.GameStatus.NotStarted;
        buttonToClick = restartButtonValue;
        for (int i = 0; i < wheelIDs.Length; i++)
        {
            wheelIDs[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
        DisplayTime(completionTime);
    }

    public void Succeed()
    {
        currentStatus = MiniGameManager.GameStatus.Completed;
        DisplayTime(completionTime);
        for (int i = 0; i < wheelIDs.Length; i++)
        {
            wheelIDs[i].GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
    public void Fail()
    {
        Debug.Log("Wheel Failed");
        currentStatus = MiniGameManager.GameStatus.Failed;
        FindObjectOfType<MiniGameManager>().CallRestart();
    }
}
