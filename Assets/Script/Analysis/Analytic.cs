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
        public string eventType;
        public string choice;
        public string time;
    }
    [Serializable]
    public class DataCollection
    {
        public List<Data> allDatas = new List<Data>();
    }

    DataCollection dataCollection;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        dataCollection = new DataCollection();
        SaveData("Game Start", "event");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveData("Quit Game", "event");
            SaveIntoJson();
        }
    }

    public void SaveData(string name, string type)
    {
        Data newData = new Data();
        newData.eventName = name;
        newData.eventName = type;
        newData.time = FormatTime(Time.time);

        dataCollection.allDatas.Add(newData);
    }
    public void SaveData(string name, string type, string choice)
    {
        Data newData = new Data();
        newData.eventName = name;
        newData.time = FormatTime(Time.time);
        newData.eventType = type;
        newData.choice = choice;

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
        //int milliseconds = (int)time * 1000 % 1000;
        //return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }



}
