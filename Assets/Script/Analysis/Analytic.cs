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
    /// Type 3. Includes eventname, time, dialogue, choice
    /// 
    /// Events included:
    /// Type 1: GameStart, Engineer1Clicked, Engineer2Clicked, EnterSpaceStation, EnterSpaceStation2
    /// Type 2: LaunchBtnPressedCount
    /// Type 3: DialogueChoices
    /// </summary>

    void Start()
    {
        SaveData("GameStart", Time.time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveData(string eventName, float time)
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
    public void SaveData(string eventName, float time, int count)
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

    public void SaveData(string eventName, string sceneName, float time, string dialogue, string choice)
    {
        string formatTime = FormatTime(time);
        AnalyticsService.Instance.CustomData(
            eventName,
            new Dictionary<string, object> {
                {"scene", sceneName},
                {"time", formatTime},
                {"dialogue", dialogue},
                {"choice", choice}
            }
        );
        AnalyticsService.Instance.Flush();
    }

    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
