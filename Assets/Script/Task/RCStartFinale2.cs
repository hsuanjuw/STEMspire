using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCStartFinale2 : Task
{
    public override void StartTask()
    {
        base.StartTask();
        DoTask();
    }
    public override void DoTask()
    {
        base.DoTask();
        FindObjectOfType<ButtonFlash>().RemoveFlashAbility();
        FindObjectOfType<MiniGameManager>().GameStart();
    }
}
