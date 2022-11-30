using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFinaleTask : Task
{
    public override void StartTask()
    {
        base.StartTask();
        DoTask();
    }
    public override void DoTask()
    {
        base.DoTask();
        FindObjectOfType<MiniGameManager>().GameStart();
        FindObjectOfType<PityTimer>().StartTimer();
    }

    
}
