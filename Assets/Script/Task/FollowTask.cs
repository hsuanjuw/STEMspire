using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowTask : Task
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

    public override void StartTask()
    {
        hint.SetActive(true);
        Player player = GameObject.FindObjectOfType<Player>();
        playerPos = player.transform.position;
        offsetPos = playerPos - this.transform.parent.position;
        gameStatus.isFinishedDialogue = true;
        player.speed = 2 * player.speed;
        base.StartTask();
    }
    public override void DoTask()
    {
        base.DoTask();
        Follow();

    }
    public override void EndTask()
    {
        base.EndTask();
    }

    public void Follow()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        playerPos = player.transform.position;
        this.transform.parent.position = playerPos - offsetPos;
    }

}
