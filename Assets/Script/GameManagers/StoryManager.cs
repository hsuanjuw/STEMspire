using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGM.Gameplay;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    private DialogueSystem dialogueSystem;
    private int status;
    private GameData gameData;
    // Start is called before the first frame update
    void Start()
    {
        gameData = GameObject.FindObjectOfType<GameData>();
        dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
        dialogueSystem.StartDialogueIntro();
        status = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextStatus()
    {
        status += 1;
        ShowGameStory();
    }

    public void ShowGameStory()
    {
        ConversationScript dialogue = GameObject.Find("DialogueSystem").transform.GetChild(1).GetComponent<ConversationScript>();
        dialogueSystem.StartDialogue(dialogue, false);
    }

    public void DoTask()
    {
        if (gameData.GameNum == 1)
        {
            DoTask1();
        }
    }

    public void DoTask1()
    {
        SceneManager.LoadScene("SpaceStation");
    }
}
