using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGM.Gameplay;

public class FixShipTask : Task
{
    public GameObject blackScreen;
    
    public override void StartTask()
    {
        base.StartTask();
        DoTask();
    }
    public override void DoTask()
    {
        base.DoTask();
        StartCoroutine(FixShip());
    }

    IEnumerator FixShip()
    {
        FindObjectOfType<ScreenFader>().levelChangeAnimator.SetTrigger("FinaleFadeOut");
        yield return new WaitForSeconds(6f);
        FindObjectOfType<PowerCoreColorChanger>().SetColor(PowerCoreColorChanger.CoreColor.Blue);
        FindObjectOfType<ScreenFader>().levelChangeAnimator.SetTrigger("FinaleFadeIn");
        
        DialogueSystem dialogueSystem = FindObjectOfType<DialogueSystem>();
        ConversationScript npcConversation = dialogueSystem.transform.GetChild(1).GetComponent<ConversationScript>();
        Task _task = dialogueSystem.transform.Find("ConversationScript").transform.Find("StartFinaleTask").GetComponent<Task>();
        dialogueSystem.StartDialogue(npcConversation, _task);
    }

}
