using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGM.Gameplay;

public class DialogueTrigger : MonoBehaviour
{    
    /// <summary>
    /// Show corresponding dialogue if the player collides with this trigger object.   
    /// </summary>

    private DialogueSystem dialogueSystem;
    // Start is called before the first frame update
    void Start()
    {
        dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            if (this.name == "Engineer2DialogueTrigger")
            {
                Engineer2DialogueTrigger();
            }
            else
            {
                StartDialogue();
            }
        }
    }

    private void StartDialogue()
    {
        ConversationScript conversation = this.GetComponentInChildren<ConversationScript>();
        dialogueSystem.StartDialogue(conversation, null);
        Destroy(this.gameObject);
    }

    private void Engineer2DialogueTrigger()
    {
        if (PlayerPrefs.GetInt("Zun_Chat") == 0)
        {
            PlayerPrefs.SetInt("Zun_Chat", 1);
            StartDialogue();
        }
    }
}
