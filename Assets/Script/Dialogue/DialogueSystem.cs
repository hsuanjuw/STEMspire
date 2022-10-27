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
    public ConversationScript conversation;

    public TextMeshProUGUI text;
    public Button option1Btn;
    public Button option2Btn;
    public Button continuePcBtn;
    public Button continueNpcBtn;
    public Button closeBtn;
    public Image npcImage;
    public Image pcImage;
    public Sprite guideRobotSprite;

    public GameObject actionbtn;
    public bool hasTask;

    public float textSpeed;

    public GameObject dialoguePanel;
    public bool dialogueOpened;

    public int currentConvIndex; // record which conversation we are at


    private void Awake()
    {
        dialogueOpened = false;
    }

    void Start()
    {
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
        StartDialogue(conversation, false);
    }

    public void StartDialogue(ConversationScript npcConversation, bool _hasTask)
    {
        hasTask = _hasTask;
        conversation = npcConversation;
        //Debug.Log(conversation.items[0].text);
        currentConvIndex = 0;
        bool isEnd = CheckEndConversation();
        npcImage.sprite = conversation.items[currentConvIndex].image;
        OpenDialoguePanel(isEnd);
        StartCoroutine(TypeLine(conversation.items[currentConvIndex].text, isEnd));
    }

    private IEnumerator TypeLine(string dialogue, bool isEnd)
    {
        Debug.Log("Type1");
        foreach (char c in dialogue.ToCharArray())
        {
            if (c == '\r' || c == '\n')
            {
                text.text += " ";
            }
            else
            {
                text.text += c;
            }
            yield return new WaitForSeconds(textSpeed);
        }
        if (!isEnd)
        {
            DisplayOption();
        }
        else
        {
            closeBtn.gameObject.SetActive(true);
        }

    }

    private IEnumerator TypeLine(string dialogue, Sprite sprite) // pc talking
    {
        Debug.Log("Type2");
        text.text = string.Empty;
        npcImage.gameObject.SetActive(false);
        pcImage.sprite = sprite;
        pcImage.gameObject.SetActive(true);

        foreach (char c in dialogue.ToCharArray())
        {
            if (c == '\r' || c == '\n') {
                text.text += " ";
            }
            else {
                text.text += c;
            }
            
            yield return new WaitForSeconds(textSpeed);
        }

        continueNpcBtn.gameObject.SetActive(true);
    }

    public void option1BtnClicked()
    {
        NextLine(0);
    }

    public void option2BtnClicked()
    {
        NextLine(1);
    }

    public void NextLine(int optionNum)
    {
        for (int i = 0; i < conversation.items.Count; i++)
        {
            if(conversation.items[i].id == conversation.items[currentConvIndex].options[optionNum].targetId)
            {
                currentConvIndex = i;
                break;
            }
        }
        option1Btn.gameObject.SetActive(false);
        option2Btn.gameObject.SetActive(false);
        Debug.Log(conversation.items[currentConvIndex].text);
        if (conversation.items[currentConvIndex].text == "")
        {
            CloseDialoguePanel();
            // do task
        }
        else
        {
            bool isEnd = CheckEndConversation();
            npcImage.sprite = conversation.items[currentConvIndex].image;
            text.text = string.Empty;
            StartCoroutine(TypeLine(conversation.items[currentConvIndex].text, isEnd));
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
        pcImage.gameObject.SetActive(false);
        npcImage.gameObject.SetActive(true);
        continueNpcBtn.gameObject.SetActive(false);
        NextLine(0);
    }

    public void ContinuePcDialogue()
    {
        continuePcBtn.gameObject.SetActive(false);
        StartCoroutine(TypeLine(conversation.items[currentConvIndex].options[0].text, conversation.items[currentConvIndex].options[0].image));
    }

    public void OpenDialoguePanel(bool isEnd)
    {
        dialogueOpened = true;
        if (!isEnd)
        {
            option1Btn.onClick.AddListener(option1BtnClicked);
            option2Btn.onClick.AddListener(option2BtnClicked);
        }
        text.text = string.Empty;
        dialoguePanel.SetActive(true);
    }

    public void CloseDialoguePanel()
    {
        closeBtn.gameObject.SetActive(false);
        dialoguePanel.SetActive(false);
        dialogueOpened = false;
        //option1Btn.onClick.RemoveListener(option1BtnClicked);
        //option2Btn.onClick.RemoveListener(option2BtnClicked);
        if (hasTask)
        {
            actionbtn.SetActive(true);
        }   
    }

}
