using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    public GameObject leftUpButton;
    public GameObject leftDownButton;
    public GameObject rightUpButton;
    public GameObject rightDownButton;
    private bool[] electicTopStatus = new bool[3] { true, true, true};
    private bool[] electicBottomStatus = new bool[2] { true, true};

    private int failCountTop = 0;
    private int failCountBottom = 0;

    private float leftSwitchTime;
    private float rightSwitchTime;

    private bool switchTimeStart = false;
    private bool gameStart = false;

    public float timeAfterGameStart = 3f; // 3f after the game start, the power start to count down;


    void Start()
    {
        //GameStart();
        //electicTop = GameObject.FindGameObjectsWithTag("ElectricTop");
        //electicBottom = GameObject.FindGameObjectsWithTag("ElectricBottom");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            if (failCountTop == 3 || failCountBottom == 2)
            {
                Debug.Log("Power Failure");
                EndGame();
            }
            if (switchTimeStart)
            {
                LCountDown();
                RCountDown();
            }
        }
    }

    public void LeftSwitchOnOff()
    {
        StartCoroutine(SwitchOnOff(leftUpButton, leftDownButton));
        TimeReStart("Left");

    }

    public void RightSwitchOnOff()
    {
        StartCoroutine(SwitchOnOff(rightUpButton, rightDownButton));
        TimeReStart("Right");
    }

    private IEnumerator SwitchOnOff(GameObject switchUpClicked, GameObject switchDownClicked)
    {
        switchUpClicked.SetActive(false);
        switchDownClicked.SetActive(true);
        yield return new WaitForSeconds(.3f);
        switchDownClicked.SetActive(false);
        switchUpClicked.SetActive(true);
    }

    public void StartGame()
    {
        gameStart = true;
        leftSwitchTime = 10f;
        rightSwitchTime = 15f;
        switchTimeStart = true;
    }

    public void EndGame()
    {
        gameStart = false;
        switchTimeStart = false;
    }

    private void TimeReStart(string type)
    {
        switch (type)
        {
            case "Left":
                leftSwitchTime = 10f;
                break;
            case "Right":
                rightSwitchTime = 15f;
                break;
            default:
                Debug.Log("Time restart Error");
                break;
        }
    }

    private void LCountDown()
    {
        if (leftSwitchTime > 0)
        {
            leftSwitchTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Left Switch Restart");
            TimeReStart("Left");
            GameObject eletricImg = GameObject.FindGameObjectsWithTag("ElectricTop")[failCountTop];
            Debug.Log(eletricImg.name);
            eletricImg.GetComponent<SpriteRenderer>().color = new Color(120f / 255f, 170f / 255f, 180f / 255f, 1f);
            failCountTop++;
        }
        Text text = GameObject.Find("LCountDownTxt").GetComponent<Text>();
        DisplayTime(leftSwitchTime, text);
    }

    private void RCountDown()
    {
        if (rightSwitchTime > 0)
        {
            rightSwitchTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Right Switch Restart");
            TimeReStart("Right");
            GameObject eletricImg = GameObject.FindGameObjectsWithTag("ElectricBottom")[failCountBottom];
            eletricImg.GetComponent<SpriteRenderer>().color = new Color(120f / 255f, 170f / 255f, 180f / 255f, 1f);
            failCountBottom++;
        }
        Text text = GameObject.Find("RCountDownTxt").GetComponent<Text>();
        DisplayTime(rightSwitchTime, text);
    }

    private void DisplayTime(float timeToDisplay, Text timeText)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}", seconds);
    }

}
