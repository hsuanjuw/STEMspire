using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Systems : MonoBehaviour
{
    public GameObject[] symbols;
    [HideInInspector]public bool[] symbolsClicked;
    public float timeGap = 20f; // every 20f, the symbol flashes

    public float time = 10f; // 10 second for clicking the symbols
    //public List<bool[]> paterns = new List<bool[]>();
    private bool[] patern = new bool[4] { true, false, true, false };
    private bool timeStart = false;
    private bool gameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        symbolsClicked = new bool[4] { false, false, false, false };
        //StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStart)
        {
            CountDown();
        }
    }

    public void StartGame()
    {
        gameStart = true;
        StartCoroutine(ShowSymbols());
    }

    private IEnumerator ShowSymbols()
    {
        while (gameStart)
        {
            yield return new WaitForSeconds(timeGap);
            timeStart = true;
            foreach(GameObject symbol in symbols)
            {
                symbol.SetActive(true);
            }
            
        }
    }

    private void CloseSymbols()
    {
        foreach (GameObject symbol in symbols)
        {
            symbol.SetActive(false);
        }
    }

    private void CountDown()
    {
        if (time > 0)
        {
            Text text = GameObject.Find("SystemCountDownTxt").GetComponent<Text>();
            DisplayTime(time, text);
            time -= Time.deltaTime;
            
        }
        else
        {
            timeStart = false;
            time = 10f;

            bool isFail = CheckPatern();
            if (isFail)
            {
                gameStart = false;
                Debug.Log("Systems failure");
            }
            else
            {
                CloseSymbols();
                ResetSymbolStatus();
            }
        }
        
    }
    private void DisplayTime(float timeToDisplay, Text timeText)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}", seconds);
    }

    private bool CheckPatern()
    {
        bool isFail = false;
        for (int i = 0; i < 4; i++)
        {
            if (symbolsClicked[i] != patern[i])
            {
                isFail = true;
            }
        }
        return isFail;
    }

    private void ResetSymbolStatus()
    {

        for (int i = 0; i < 4; i++)
        {
            symbolsClicked[i] = false;
            symbols[i].GetComponent<Image>().color = Color.white;

        }
    }

}
