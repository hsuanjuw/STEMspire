using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGM.Gameplay;

public class FixSystemTask : Task
{
    public GameObject systemStatic;
    public GameObject wheelStatic;
    public override void StartTask()
    {
        base.StartTask();
        DoTask();
    }
    public override void DoTask()
    {
        base.DoTask();
        StartCoroutine(FixSystem());
    }

    IEnumerator FixSystem()
    {
        FindObjectOfType<ScreenFader>().levelChangeAnimator.SetTrigger("FinaleFadeOut");
        yield return new WaitForSeconds(6f);
        systemStatic.SetActive(false);
        wheelStatic.SetActive(false);
        FindObjectOfType<ScreenFader>().levelChangeAnimator.SetTrigger("FinaleFadeIn");

        DialogueSystem dialogueSystem = FindObjectOfType<DialogueSystem>();
        ConversationScript npcConversation = dialogueSystem.transform.GetChild(1).GetComponent<ConversationScript>();
        Task _task = npcConversation.transform.GetComponentInChildren<Task>();
        dialogueSystem.StartDialogue(npcConversation, _task);
    }
}
