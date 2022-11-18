using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGM.Gameplay;

public class NpcHandler : MonoBehaviour
{
    /// <summary>
    /// Handle NPC being clicked by the player
    /// </summary>
 
    private Sprite npcSprite;

    private DialogueSystem dialogueSystem;
    private ConversationScript conversation;
    private Analytic analytic;

    private Task task;
    public enum CharacterPrefs
    {
        Bot_Enthusiast_Chat,
        Parents_Chat,
        Service_Guide_Chat
    }

    public CharacterPrefs characterPrefs;

    // Start is called before the first frame update
    void Start()
    {
        analytic = GameObject.FindObjectOfType<Analytic>();
        dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
        conversation = this.GetComponentInChildren<ConversationScript>();
        npcSprite = GetComponentInChildren<SpriteRenderer>().sprite;

        // See if there is task object in the children
        task = this.GetComponentInChildren<Task>();

/*        if (this.GetComponentInChildren<Task>()) 
        {
            task = true;
        }
        else
        {
            task = null;
        }*/

    }


    public void OnMouseDown()
    {
        if (!dialogueSystem.dialogueOpened)
        {
            dialogueSystem.StartDialogue(conversation, task);
            // Destroy interaction icon if is triggered
            if (this.GetComponentInChildren<InteractionIcon>())
            {
                Destroy(this.GetComponentInChildren<InteractionIcon>().gameObject);
            }

            // Set Player Prefs after the interaction icon is triggered
            if (PlayerPrefs.GetInt(characterPrefs.ToString()) == 0) 
            {
                PlayerPrefs.SetInt(characterPrefs.ToString(), 1);
            }

            // Save data to analytic
            analytic.SaveNPCData(Time.time, this.name);
        }
    }
}
