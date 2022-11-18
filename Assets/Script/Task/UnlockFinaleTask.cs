using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockFinaleTask : Task
{
    private GameStatus gameStatus;
    private Vector3 playerPos;
    private Vector3 offsetPos;
    public GameObject hint;
    // Start is called before the first frame update
    void Start()
    {
        gameStatus = GameObject.FindObjectOfType<GameStatus>();
        base.isStartTask = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (base.isStartTask)
        {
            DoTask();
        }
    }

    public override void DoTask()
    {
        base.DoTask();
        Unlock();

    }

    public void Unlock()
    {
        FindObjectOfType<LevelChanger>().triggerActive = true;
    }

}
