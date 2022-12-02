using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGM.Gameplay;

public class FinaleNpcHandler : MonoBehaviour
{
    /// <summary>
    /// Handle NPC being clicked by the player
    /// </summary>
    public bool isClicked = false;

    private Sprite npcSprite;

    private DialogueSystem dialogueSystem;
    private ConversationScript[] conversations;
    //private GameObject conversationObjs;
    private Analytic analytic;

    private Task task;

    private bool clickable = false;

    private void Awake()
    {
        //PlayerPrefs.SetInt("Game3_DialogueStarted", 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        analytic = GameObject.FindObjectOfType<Analytic>();
        dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
        conversations = this.GetComponentsInChildren<ConversationScript>();
        npcSprite = GetComponentInChildren<SpriteRenderer>().sprite;

        // See if there is task object in the children
        task = this.GetComponentInChildren<Task>();
    }

    public void MakeClickable()
    {
        clickable = true;
    }

    public void MakeNotClickable()
    {
        clickable = false;
    }
    public void OnMouseDown()
    {
        if (!dialogueSystem.dialogueOpened && clickable)
        {
                dialogueSystem.StartDialogue(conversations[0], task);
                // Destroy interaction icon if is triggered

                // Save data to analytic
                analytic.SaveNPCData(Time.time, this.name);
        }
    }
}
