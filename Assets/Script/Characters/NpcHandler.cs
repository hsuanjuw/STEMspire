using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGM.Gameplay;

public class NpcHandler : MonoBehaviour
{
    /// <summary>
    /// Handle NPC being clicked by the player
    /// </summary>
    public bool dialogueNotRepeatable = false;
    public bool isClicked = false;

    private Sprite npcSprite;

    private DialogueSystem dialogueSystem;
    private ConversationScript[] conversations;
    //private GameObject conversationObjs;
    private Analytic analytic;

    private Task task;
    public enum CharacterPrefs
    {
        Bot_Enthusiast_Chat,
        Parents_Chat,
        Service_Guide_Chat,
        Family_Chat
    }

    public CharacterPrefs characterPrefs;

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


    public void OnMouseDown()
    {
        if (!dialogueSystem.dialogueOpened && !dialogueNotRepeatable)
        {
            if (!isClicked)
            {
                isClicked = true;
                dialogueSystem.StartDialogue(conversations[0], task);
                // Destroy interaction icon if is triggered
                if (this.GetComponentInChildren<InteractionIcon>())
                {
                    Destroy(this.GetComponentInChildren<InteractionIcon>().gameObject);
                }

                // Set Player Prefs after the interaction icon is triggered
                if (PlayerPrefs.GetInt(characterPrefs.ToString()) == 0) 
                {
                    PlayerPrefs.SetInt(characterPrefs.ToString(), 1);
                    Debug.Log(PlayerPrefs.GetInt(characterPrefs.ToString()));
                }

                // Save data to analytic
                analytic.SaveNPCData(Time.time, this.name);
            }
            else
            {
                dialogueSystem.StartDialogue(conversations[1], null);
                analytic.SaveNPCData(Time.time, this.name+"_FollowUp");
            }

        }
    }
}
