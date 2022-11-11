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
    public bool hintActive = false;

    Coroutine coroutine;
    void Start()
    {
        if (hintActive)
            DisplayTime(completionTime);
        else GameObject.Find("SystemCountDownTxt").GetComponent<Text>().text = "";
    }
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
        coroutine = StartCoroutine(StartWait());
    }

    private IEnumerator StartWait()
    {
        yield return new WaitForSeconds(timeAfterGameStart); // 10 Sec after launch, the wheel game started
        if (currentStatus == MiniGameManager.GameStatus.NotStarted)
        {
            currentStatus = MiniGameManager.GameStatus.InProgress;
            timeRemaining = completionTime;
            FindObjectOfType<Finale_SystemInfo>().NextPhase();
            StartCoroutine(StartFlash());
        }
    }

    private void CountDown()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
            if (CheckPattern())
            {
                timeRemaining = completionTime;
                FindObjectOfType<Finale_SystemInfo>().NextPhase();
                if(FindObjectOfType<Finale_SystemInfo>().currentPhase == Finale_SystemInfo.Phase.Complete)
                    Succeed();    
            }
        }
        else
        {
            
            DisplayTime(0);

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
    private IEnumerator StartFlash()
    {
        GameObject.Find("SystemCountDownTxt").transform.Find("Spotlight").GetComponent<Spotlight>().StartFlashing();
        SpriteRenderer sisBG = FindObjectOfType<Finale_SystemInfo>().transform.Find("systemsInfo")
            .GetComponent<SpriteRenderer>();
        if (sisBG != null)
        {
            while (currentStatus == MiniGameManager.GameStatus.InProgress)
            {
                sisBG.color = Color.yellow;
                yield return new WaitForSeconds(1f);
                sisBG.GetComponent<SpriteRenderer>().color = Color.white;
                yield return new WaitForSeconds(1f);
            } 
        }
    }
    private void DisplayTime(float timeToDisplay)
    {
        if (hintActive)
        {
            GameObject.Find("SystemCountDownTxt").transform.Find("Circle").GetComponent<Image>().fillAmount =
                timeToDisplay / completionTime;
            if(timeToDisplay / completionTime < 0.5f)
                GameObject.Find("SystemCountDownTxt").transform.Find("Circle").GetComponent<Image>().color = Color.red;
            else GameObject.Find("SystemCountDownTxt").transform.Find("Circle").GetComponent<Image>().color = Color.white;
        }
            
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
            systemSymbols[i].GetComponent<Symbol>().ResetSymbol();
            
            systemInfoSymbols[i].transform.Find("Active").gameObject.SetActive(false);
            //systemInfoSymbols[i].transform.Find("Inactive").gameObject.SetActive(true);
        }
    }

    private void ResetSystem()
    {
        currentStatus = MiniGameManager.GameStatus.NotStarted;
        StopCoroutine(coroutine);
        DisplayTime(completionTime);
        for (int i = 0; i < systemSymbols.Length; i++)
        {
            systemSymbols[i].GetComponent<Image>().color = Color.white;
        }
        ResetSymbolStatus();
        FindObjectOfType<Finale_SystemInfo>().ResetSystemInfo();
    }

    public void Fail()
    {
        Debug.Log("System Failed");
        currentStatus = MiniGameManager.GameStatus.Failed;
        FindObjectOfType<MiniGameManager>().CallRestart();
    }

    public void Succeed()
    {
        currentStatus = MiniGameManager.GameStatus.Completed;
        DisplayTime(completionTime);
        GameObject.Find("SystemCountDownTxt").transform.Find("Circle").GetComponent<Image>().color = Color.green;
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
