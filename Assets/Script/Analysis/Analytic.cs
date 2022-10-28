using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
//using UnityEngine.Analytics;

public class Analytic : MonoBehaviour
{
    void Awake()
    {

    }
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
    }

    public void SaveData(string eventName, string sceneName, float time, string question, string choice)
    {
        string formatTime = FormatTime(time);
        AnalyticsService.Instance.CustomData(
            eventName,
            new Dictionary<string, object> {
                {"scene", sceneName},
                {"time", formatTime},
                {"dialogue", question},
                {"choice", choice}
            }
        );
    }

    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
