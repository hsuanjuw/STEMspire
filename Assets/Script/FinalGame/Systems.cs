using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Systems : MonoBehaviour
{
    public MiniGameManager.GameStatus currentStatus = MiniGameManager.GameStatus.NotStarted;
    public GameObject[] systemSymbols;
    public GameObject[] systemInfoSymbols;
    [HideInInspector]public bool[] symbolsClicked;
    public float timeAfterGameStart = 20f; // every 20f, the symbol flashes

    public float completionTime = 10f;
    public float timeRemaining = 10f; // 10 second for clicking the symbols

    // Update is called once per frame
    void Update()
    {
        if (currentStatus == MiniGameManager.GameStatus.InProgress)
        {
            CountDown();
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartWait());
    }

    private IEnumerator StartWait()
    {
        yield return new WaitForSeconds(timeAfterGameStart); // 10 Sec after launch, the wheel game started
        timeRemaining = completionTime;
        currentStatus = MiniGameManager.GameStatus.InProgress;
        FindObjectOfType<Finale_SystemInfo>().NextPhase();
    }

    private void CountDown()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            Text text = GameObject.Find("SystemCountDownTxt").GetComponent<Text>();
            DisplayTime(timeRemaining, text);
        }
        else
        {
            Text text = GameObject.Find("SystemCountDownTxt").GetComponent<Text>();
            DisplayTime(0, text);
            timeRemaining = completionTime;

            if (!CheckPattern())
            {
                Fail();
            }
            else
            {
                FindObjectOfType<Finale_SystemInfo>().NextPhase();
                if(FindObjectOfType<Finale_SystemInfo>().currentPhase == Finale_SystemInfo.Phase.Complete)
                    Succeed();
            }
        }
        
    }
    private void DisplayTime(float timeToDisplay, Text timeText)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}", seconds);
    }

    private bool CheckPattern()
    {
        for (int symbolIndex = 0; symbolIndex < systemSymbols.Length; symbolIndex++)
        {
            if (systemSymbols[symbolIndex].transform.Find("Active").gameObject.activeSelf !=
                systemInfoSymbols[symbolIndex].transform.Find("Active").gameObject.activeSelf)
                return false;
        }

        return true;
    }

    private void ResetSymbolStatus()
    {
        for (int i = 0; i < systemSymbols.Length; i++)
        {
            systemSymbols[i].transform.Find("Active").gameObject.SetActive(false);
            systemSymbols[i].transform.Find("Inactive").gameObject.SetActive(true);
            
            systemInfoSymbols[i].transform.Find("Active").gameObject.SetActive(false);
            systemInfoSymbols[i].transform.Find("Inactive").gameObject.SetActive(true);
        }
    }

    private void ResetSystem()
    {
        currentStatus = MiniGameManager.GameStatus.NotStarted;
        Text text = GameObject.Find("SystemCountDownTxt").GetComponent<Text>();
        DisplayTime(timeRemaining, text);
        for (int i = 0; i < systemSymbols.Length; i++)
        {
            systemSymbols[i].GetComponent<Image>().color = Color.white;
        }
        ResetSymbolStatus();
    }

    public void Fail()
    {
        currentStatus = MiniGameManager.GameStatus.Failed;
        FindObjectOfType<MiniGameManager>().CallRestart();
    }

    public void Succeed()
    {
        currentStatus = MiniGameManager.GameStatus.Completed;
        GameObject.Find("SystemCountDownTxt").GetComponent<Text>().text = "";
        for (int i = 0; i < systemSymbols.Length; i++)
        {
            systemSymbols[i].GetComponent<Image>().color = Color.green;
        }
    }
    public void EndGame()
    {
        ResetSystem();
    }
}
