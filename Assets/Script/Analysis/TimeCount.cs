using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;
using UnityEngine;
using Unity.Services.Analytics;

public class TimeCount : MonoBehaviour
{
    public float btnClickedTime;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
       // Debug.Log("Start Time:" + startTime);
    }

    // Update is called once per frame
    void Update()
    {
        btnClickedTime = (float)System.Math.Round(Time.time - startTime, 2); 
        //Debug.Log("Passed Time:" + btnClickedTime);
    }

    public void triggerBtnAnalytic()
    {
        // Send custom event
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "timePassed", btnClickedTime },
        };
        // The ‘myEvent’ event will get queued up and sent every minute
        AnalyticsService.Instance.CustomData("BtnClicked", parameters);

        // Optional - You can call Events.Flush() to send the event immediately
        AnalyticsService.Instance.Flush();
    }
}
