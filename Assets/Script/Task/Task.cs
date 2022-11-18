using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public bool isStartTask;

    public string startTaskBtntxt;
    
    // Start is called before the first frame update
    void Start()
    {
        isStartTask = false;
    }

    public virtual void StartTask()
    {
        isStartTask = true;
    }

    public virtual void DoTask()
    {

    }

    public virtual void EndTask()
    {
        isStartTask = false;
    }

    public void setStartTaskBtntxt(string txt)
    {
        startTaskBtntxt = txt;
    }
}
