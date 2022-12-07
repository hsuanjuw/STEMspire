using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
//using UnityEngine.Analytics;

public class Analytic : MonoBehaviour
{
    /// <summary>
    /// Handle uploading data to Unity Analytics
    /// There are three type of data:
    /// Type 1. Includes eventname, time
    /// Type 2. Includes eventname, time, number of the button being pressed
    /// Type 3. Includes eventname, sceneName, time, dialogue, choice
    /// Type 4. Includes eventname, timePassed, objName
    /// Type 5. Includes eventname, time, npcName
    /// 
    /// Events included:
    /// Type 1: GameStart, EnterSpaceStation, EnterSpaceStation2, minigamePassed
    /// Type 2: LaunchBtnPressedCount
    /// Type 3: DialogueChoices
    /// Type 4: EnvirObjClicked
    /// Type 5: NPCClicked
    /// </summary>

    void Start()
    {
        if (PlayerPrefs.GetInt("Analytic") == 1)
        {
            SaveData("GameStart", Time.time);
        }
            
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveData(string eventName, float time)
    {
        if (PlayerPrefs.GetInt("Analytic") == 1)
        {
            string formatTime = FormatTime(time);
            AnalyticsService.Instance.CustomData(
                eventName,
                new Dictionary<string, object> {
                    {"time", formatTime}
                }
            );
            AnalyticsService.Instance.Flush();
        }
    }
    public void SaveData(string eventName, float time, int count)
    {
        if (PlayerPrefs.GetInt("Analytic") == 1)
        {
            string formatTime = FormatTime(time);
            AnalyticsService.Instance.CustomData(
                eventName,
                new Dictionary<string, object> {
                    {"time", formatTime},
                    {"count", count}
                }
            );
            AnalyticsService.Instance.Flush();
        }
    }

    public void SaveData(string eventName, string sceneName, float timePassed, string dialogue, string choice)
    {
        //Debug.Log(timePassed);
        if (PlayerPrefs.GetInt("Analytic") == 1)
        {
            AnalyticsService.Instance.CustomData(
                eventName,
                new Dictionary<string, object> {
                    {"scene", sceneName},
                    {"timePassed", timePassed},
                    {"dialogue", dialogue},
                    {"choice", choice}
                }
            );
            AnalyticsService.Instance.Flush();
        }

    }

    public void SaveData(string eventName, float timePassed, string objName)
    {
        //Debug.Log(timePassed);
        if (PlayerPrefs.GetInt("Analytic") == 1)
        {
            AnalyticsService.Instance.CustomData(
                eventName,
                new Dictionary<string, object> {
                    {"timePassed", timePassed},
                    {"objName", objName}
                }
            );
            AnalyticsService.Instance.Flush();
        }

    }

    public void SaveNPCData(float time, string npcName)
    {
        if (PlayerPrefs.GetInt("Analytic") == 1)
        {
            string formatTime = FormatTime(time);
            AnalyticsService.Instance.CustomData(
                "NPCClicked",
                new Dictionary<string, object> {
                    {"time", formatTime},
                    {"npcName", npcName}
                }
            );
            AnalyticsService.Instance.Flush();
        }
    }

    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
