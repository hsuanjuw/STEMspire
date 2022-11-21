using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockFinaleTask : Task
{
    void Start()
    {
        base.isStartTask = false;
    }
    
    public override void StartTask()
    {
        base.StartTask();
        Unlock();
    }
    public void Unlock()
    {
        FindObjectOfType<LevelChanger>().triggerActive = true;
        SetRobotStatus();
    }

    private void SetRobotStatus()
    {
        DialogueSystem dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
        int playerChoice = dialogueSystem.playerChoice;

        if (playerChoice == 0)
        {
            PlayerPrefs.SetInt("Robot_Stay", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Robot_Stay", 0);
            FindObjectOfType<RobotMovement>().ChangeMovement(CharacterMovement.MovementType.FollowPlayer);
        }

    }

}
