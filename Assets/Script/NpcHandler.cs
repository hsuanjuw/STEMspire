using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHandler : MonoBehaviour
{
    public int npcType;
    public string npcTypeStr; 
    private string dialoguePath;

    private DialogueSystem dialogueSystem;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
        player = GameObject.FindObjectOfType<Player>();
        npcTypeToString();
        dialoguePath = "Assets/Dialogues/NPC/" + npcTypeStr + "_dialogue1.txt";
    }

    private void npcTypeToString()
    {
        switch (npcType)
        {
            case 1:
                npcTypeStr = "type1";
                break;
            case 2:
                npcTypeStr = "type2";
                break;
        }
    }

    public void OnMouseDown()
    {
        Debug.Log("clicked");
        if (!dialogueSystem.dialogueOpened)
        {
            Vector2 npcPos = new Vector2(this.transform.position.x - 1f, this.transform.position.y);
            dialogueSystem.StartDialogue(dialoguePath, this.gameObject.GetComponent<SpriteRenderer>().sprite);
        }
    }
}
