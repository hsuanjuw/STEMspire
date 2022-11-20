using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGM.Gameplay;

public class FixShipTask : Task
{
    public GameObject blackScreen;
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
        StartCoroutine(FixShip());
    }

    IEnumerator FixShip()
    {
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        FindObjectOfType<PowerCoreColorChanger>().SetColor(PowerCoreColorChanger.CoreColor.Blue);
        blackScreen.SetActive(false);

        DialogueSystem dialogueSystem = FindObjectOfType<DialogueSystem>();
        ConversationScript npcConversation = dialogueSystem.transform.GetChild(1).GetComponent<ConversationScript>();
        dialogueSystem.StartDialogue(npcConversation, null);
    }

}
