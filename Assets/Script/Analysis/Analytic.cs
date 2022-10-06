using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Analytic : MonoBehaviour
{
    [Serializable]
    public class Data 
    {
        public string eventName;
        public string time;
    }
    [Serializable]
    public class DataCollection
    {
        public List<Data> allDatas = new List<Data>();
    }

    DataCollection dataCollection;
    void Start()
    {
        dataCollection = new DataCollection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData(string name)
    {
        Data newData = new Data();
        newData.eventName = name;
        newData.time = FormatTime(Time.time);

        dataCollection.allDatas.Add(newData);
    }

    public void SaveIntoJson()
    {
        string data = JsonUtility.ToJson(dataCollection);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Data.json", data);
        Debug.Log(Application.persistentDataPath);
    }

    public string FormatTime(float time)
    {
        Debug.Log(time);
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        int milliseconds = (int)time * 1000 % 1000;
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }



}
