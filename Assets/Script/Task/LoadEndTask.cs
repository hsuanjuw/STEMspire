using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEndTask : Task
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void StartTask()
    {
        base.StartTask();
        DoTask();
    }
    public override void DoTask()
    {
        base.DoTask();
        //FindObjectOfType<ScreenFader>().SwitchScene("Thanks");
        FindObjectOfType<EngineerMovement>().ChangeMovement(CharacterMovement.MovementType.Hide);
        //SetRobotStatus();
        FindObjectOfType<ButtonFlash>().MakeClickable();
    }
    private void SetRobotStatus()
    {
        DialogueSystem dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
        int playerChoice = dialogueSystem.playerChoice;

        if (playerChoice == 0)
        {
            FindObjectOfType<RobotMovement>().ChangeMovement(CharacterMovement.MovementType.FollowEngineer);
        }
    } 
}
