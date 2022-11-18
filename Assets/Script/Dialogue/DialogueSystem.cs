using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;
using RPGM.Gameplay;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{
    /// <summary>
    /// Handle the dialogue panel open/close, start Dialogues
    /// </summary>

    public ConversationScript conversation;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueName;
    public Button option1Btn;
    public Button option2Btn;
    public Button continuePcBtn;
    public Button continueNpcBtn;
    public Button closeBtn;
    public Button taskBtn;
    public Image npcImage;
    public Image pcImage;

    //Task
    public GameObject actionbtn;
    public bool hasTask;
    public Task task;

    private float textSpeed;
    private bool dialogueIsStarted;

    public GameObject dialoguePanel;
    public bool dialogueOpened;

    public int currentConvIndex; // record which conversation we are at

    private Analytic analytic;

    //For recording time that popups on
    private float startTime;
    private float endTime;

    private void Awake()
    {
        dialogueOpened = false;
    }

    void Start()
    {
        textSpeed = 0.01f;
        dialogueIsStarted = false;
        analytic = GameObject.FindObjectOfType<Analytic>();
        /*        text = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();
                option1Btn = GameObject.Find("Option1Btn").GetComponent<Button>();
                option2Btn = GameObject.Find("Option2Btn").GetComponent<Button>();
                continuePcBtn = GameObject.Find("ContinuePcBtn").GetComponent<Button>();
                continueNpcBtn = GameObject.Find("ContinueNpcBtn").GetComponent<Button>();
                closeBtn = GameObject.Find("CloseBtn").GetComponent<Button>();*/
    }

    public void StartDialogueIntro()
    {
        conversation = this.transform.GetChild(0).GetComponent<ConversationScript>();
        StartDialogue(conversation, null);
    }


    // Start dialogue if dialogue is not started. 
    // Check if there is task and whether the converstion is end 
    public void StartDialogue(ConversationScript npcConversation, Task _task)
    {
        if (!dialogueIsStarted)
        {
            dialogueIsStarted = true;
            task = _task;
            conversation = npcConversation;
            //Debug.Log(conversation.items[0].text);
            currentConvIndex = 0;
            bool isEnd = CheckEndConversation();
            npcImage.sprite = conversation.items[currentConvIndex].imagePrefab.GetComponent<SpriteRenderer>().sprite;
            OpenDialoguePanel(isEnd);
            StartCoroutine(TypeLine(conversation.items[currentConvIndex], isEnd));
        }
    }

    private IEnumerator TypeLine(ConversationPiece conversationPiece, bool isEnd) 
    {
        startTime = Time.time;
        //Debug.Log("Type1");
        string dialogue = conversationPiece.text; // Set dialogue texts 
        dialogueName.text = conversationPiece.name; // Set dialogue name

        foreach (char c in dialogue.ToCharArray()) // Display char by char
        {
            // Convert '\r', '\n' to " " because it will cause formatting issue in textmeshpro
            if (c == '\r' || c == '\n')   
            {
                dialogueText.text += " ";
            }
            else
            {
                dialogueText.text += c;
            }
            yield return new WaitForSeconds(textSpeed);
        }

        // Set close button active if this dialogue ends or else display dialogue choices 
        if (!isEnd)
        {
            DisplayOption();
        }
        else
        {
            if (task == null)
            {
                closeBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
            }
            else
            {
                closeBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = task.startTaskBtntxt;
            }
            closeBtn.gameObject.SetActive(true);

        }
    }

    private IEnumerator TypeLine(ConversationOption conversationOption, Sprite sprite) // pc talking
    {
        startTime = Time.time;
        //Debug.Log("Type2");
        string dialogue = conversationOption.text;
        dialogueName.text = conversationOption.name;

        dialogueText.text = string.Empty;
        npcImage.gameObject.SetActive(false);
        pcImage.sprite = sprite;
        pcImage.gameObject.SetActive(true);

        foreach (char c in dialogue.ToCharArray())
        {
            if (c == '\r' || c == '\n') {
                dialogueText.text += " ";
            }
            else {
                dialogueText.text += c;
            }
            
            yield return new WaitForSeconds(textSpeed);
        }

        continueNpcBtn.gameObject.SetActive(true);
    }

    public void option1BtnClicked()
    {
        endTime = Time.time;
        SaveAnalyticData(0, true);
        NextLine(0);
    }

    public void option2BtnClicked()
    {
        endTime = Time.time;
        SaveAnalyticData(1, true);
        NextLine(1);
    }

    public void NextLine(int optionNum)
    {
        for (int i = 0; i < conversation.items.Count; i++)
        {
            if (conversation.items[i].id == conversation.items[currentConvIndex].options[optionNum].targetId)
            {
                currentConvIndex = i;
                break;
            }
        }
        option1Btn.gameObject.SetActive(false);
        option2Btn.gameObject.SetActive(false);

        if (conversation.items[currentConvIndex].text == "") // close panel when player clicked the choice and there's no following NPC talking
        {
            CloseDialoguePanel();
            // do task
        }
        else
        {
            bool isEnd = CheckEndConversation();
            npcImage.sprite = conversation.items[currentConvIndex].imagePrefab.GetComponent<SpriteRenderer>().sprite;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine(conversation.items[currentConvIndex], isEnd));
        }

    }

    public bool CheckEndConversation() // No option
    {
        if (conversation.items[currentConvIndex].options.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void DisplayOption()
    {
        if (conversation.items[currentConvIndex].options.Count == 1)
        {
            if (conversation.items[currentConvIndex].options[0].text == "") // PC didn't talk, next NPC line
            {
                continueNpcBtn.gameObject.SetActive(true);
            }
            else  // PC talk
            {
                continuePcBtn.gameObject.SetActive(true);
            }
        }
        else  // PC makes choice
        {
            option1Btn.GetComponentInChildren<Text>().text = conversation.items[currentConvIndex].options[0].text;
            option2Btn.GetComponentInChildren<Text>().text = conversation.items[currentConvIndex].options[1].text;
            option1Btn.gameObject.SetActive(true);
            option2Btn.gameObject.SetActive(true);
        }
    }
    public void ContinueNpcDialogue()
    {
        saveContinueBtnData();
        pcImage.gameObject.SetActive(false);
        npcImage.gameObject.SetActive(true);
        continueNpcBtn.gameObject.SetActive(false);
        NextLine(0);
    }

    public void ContinuePcDialogue()
    {
        saveContinueBtnData();
        continuePcBtn.gameObject.SetActive(false);
        StartCoroutine(TypeLine(conversation.items[currentConvIndex].options[0], conversation.items[currentConvIndex].options[0].imagePrefab.GetComponent<SpriteRenderer>().sprite));
    }

    public void OpenDialoguePanel(bool isEnd)
    {
        dialogueOpened = true;
        if (!isEnd)
        {
            option1Btn.onClick.AddListener(option1BtnClicked);
            option2Btn.onClick.AddListener(option2BtnClicked);
        }
        dialogueText.text = string.Empty;
        dialoguePanel.SetActive(true);
    }

    public void CloseDialoguePanel()
    {
        dialogueIsStarted = false;
        closeBtn.gameObject.SetActive(false);
        dialoguePanel.SetActive(false);
        dialogueOpened = false;
        option1Btn.onClick.RemoveListener(option1BtnClicked);
        option2Btn.onClick.RemoveListener(option2BtnClicked);
        if (task != null)
        {
            task.StartTask();
        }   
    }

    public void saveContinueBtnData()
    {
        endTime = Time.time;
        SaveAnalyticData(0, false); 
    }

    private void SaveAnalyticData(int optionNum, bool hasChoice)
    {
        float timePassed = (int)Mathf.Round((endTime - startTime) * 100f) / 100f;

        if (hasChoice)
        {
            analytic.SaveData(
                "DialogueChoices",
                SceneManager.GetActiveScene().name,
                timePassed,
                conversation.items[currentConvIndex].text,
                conversation.items[currentConvIndex].options[optionNum].text
            );
        }
        else{
            analytic.SaveData(
                "DialogueChoices",
                SceneManager.GetActiveScene().name,
                timePassed,
                conversation.items[currentConvIndex].text,
                "No choice"
            );
        }
    }

}
