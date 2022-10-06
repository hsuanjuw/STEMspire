using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : MonoBehaviour
{
    private int lastBtnClickedNum;
    private bool[] clickedBtns;
    [HideInInspector]public bool gameStarted;
    private GameObject[] numberCards;

    public float timeAfterGameStart = 10f; // 10f after game start, number card flash

    private float time;


    void Start()
    {
        lastBtnClickedNum = 0;
        clickedBtns = new bool[5] { false, false, false, false, false };
        gameStarted = false;
        numberCards = new GameObject[5];
        for (int i = 0 ; i < 5 ; i++)
        {
            numberCards[i] = GameObject.Find((i+1).ToString());
        }
        //StartGame();
    }

    
    void Update()
    {
        if (gameStarted)
        {
            CountDown();
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartNumFlash());
    }

    public void ButtonClicked( int btnClickedNum)
    {
        clickedBtns[btnClickedNum - 1] = true;
        if (btnClickedNum != lastBtnClickedNum + 1 || !clickedBtns[0])
        {
            ResetBtnClicked();
        }
        if (btnClickedNum == 1)
        {
            clickedBtns[0] = true;
        }
        lastBtnClickedNum = btnClickedNum;

        if (CheckAllBtnClicked())
        {
            Debug.Log("Finshed");
            gameStarted = false;
        }
    }

    public void ResetBtnClicked()
    {
        for (int i = 0; i < 5; i++ )
        {
            clickedBtns[i] = false;
        }
    }

    public bool CheckAllBtnClicked()
    {
        int clickedBtnSum = 0;
        for (int i = 0; i < 5; i++)
        {
            if (clickedBtns[i])
            {
                clickedBtnSum++;
            }
        }

        if (clickedBtnSum == 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator StartNumFlash()
    {
        yield return new WaitForSeconds(timeAfterGameStart); // 10 Sec after launch, the wheel game started
        Debug.Log("Start Wheel game");
        time = 10f;
        gameStarted = true;
        while (gameStarted)
        {
            for (int i = 0; i < 5; i++)
            {
                numberCards[i].GetComponent<SpriteRenderer>().color = new Color(253f,255f,0f,255f);
            }
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 5; i++)
            {
                numberCards[i].GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void CountDown()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            gameStarted = false;
            Debug.Log("Wheel Failure");
        }
        Text text = GameObject.Find("WheelCountDownTxt").GetComponent<Text>();
        DisplayTime(time, text);
    }
    private void DisplayTime(float timeToDisplay, Text timeText)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}", seconds);
    }
}
