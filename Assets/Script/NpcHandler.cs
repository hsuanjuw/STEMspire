using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGM.Gameplay;

public class NpcHandler : MonoBehaviour
{
    private Sprite npcSprite;

    private DialogueSystem dialogueSystem;
    private ConversationScript conversation;

    private bool hasTask;

    // Start is called before the first frame update
    void Start()
    {
        dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
        conversation = this.GetComponentInChildren<ConversationScript>();
        npcSprite = GetComponentInParent<SpriteRenderer>().sprite;
        if (this.GetComponentInChildren<Task>())
        {
            hasTask = true;
        }
        else
        {
            hasTask = false;
        }

    }


    public void OnMouseDown()
    {
        Debug.Log("clicked");
        if (!dialogueSystem.dialogueOpened)
        {
            dialogueSystem.StartDialogue(conversation, npcSprite, hasTask);
        }
    }
}
