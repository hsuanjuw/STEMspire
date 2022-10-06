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
    public Image npcImage;
    public Image pcImage;
    public Sprite guideRobotSprite;

    public GameObject actionbtn;
    public bool hasTask;

    public float textSpeed;

    public GameObject dialoguePanel;
    public bool dialogueOpened;

    public int currentConvIndex; // record which conversation we are at

    

    // Start is called before the first frame update
    void Start()
    {
        text.text = string.Empty;
        dialogueOpened = false;
        if (SceneManager.GetActiveScene().name == "SpaceStation")
        {
            StartDialogueIntro();
        }
        //StartDialogueIntro();
    }

    public void StartDialogueIntro()
    {
        hasTask = false;
        npcImage.sprite = guideRobotSprite;
        //Debug.Log(conversation.items[0].text);
        currentConvIndex = 0;
        bool isEnd = checkEndConversation();
        OpenDialoguePanel(isEnd);
        StartCoroutine(TypeLine(conversation.items[currentConvIndex].text, isEnd));
        
    }

    public void StartDialogue(ConversationScript npcConversation, Sprite sprite, bool _hasTask)
    {
        hasTask = _hasTask;
        conversation = npcConversation;
        //Debug.Log(conversation.items[0].text);
        currentConvIndex = 0;
        bool isEnd = checkEndConversation();
        npcImage.sprite = sprite;
        OpenDialoguePanel(isEnd);
        StartCoroutine(TypeLine(conversation.items[currentConvIndex].text, isEnd));
    }

    private IEnumerator TypeLine(string dialogue, bool isEnd)
    {
        foreach (char c in dialogue.ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        if (!isEnd)
        {
            DisplayOption();
        }
        else
        {
            yield return new WaitForSeconds(2f);
            CloseDialoguePanel();
        }

    }

    private IEnumerator TypeLine(string dialogue, Sprite sprite)
    {
        yield return new WaitForSeconds(2f);
        text.text = string.Empty;
        npcImage.gameObject.SetActive(false);
        pcImage.sprite = sprite;
        pcImage.gameObject.SetActive(true);

        foreach (char c in dialogue.ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(2f);

        pcImage.gameObject.SetActive(false);
        npcImage.gameObject.SetActive(true);
        NextLine(0);
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
        for (int i = currentConvIndex; i < conversation.items.Count; i++)
        {
            if(conversation.items[i].id == conversation.items[currentConvIndex].options[optionNum].targetId)
            {
                currentConvIndex = i;
                break;
            }
        }
        option1Btn.gameObject.SetActive(false);
        option2Btn.gameObject.SetActive(false);
        bool isEnd = checkEndConversation();
        text.text = string.Empty;
        StartCoroutine(TypeLine(conversation.items[currentConvIndex].text, isEnd));
    }

    public bool checkEndConversation()
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
            StartCoroutine(TypeLine(conversation.items[currentConvIndex].options[0].text, conversation.items[currentConvIndex].options[0].image));
        }
        else
        {
            option1Btn.GetComponentInChildren<Text>().text = conversation.items[currentConvIndex].options[0].text;
            option2Btn.GetComponentInChildren<Text>().text = conversation.items[currentConvIndex].options[1].text;
            option1Btn.gameObject.SetActive(true);
            option2Btn.gameObject.SetActive(true);
        }
    }
    public void BackLine()
    {

    }

    public void OpenDialoguePanel(bool isEnd)
    {
        dialogueOpened = true;
        if (!isEnd)
        {
            option1Btn.onClick.AddListener(option1BtnClicked);
            option2Btn.onClick.AddListener(option2BtnClicked);
            //option1Btn.GetComponentInChildren<Text>().text = conversation.items[currentConvIndex].options[0].text;
            //option2Btn.GetComponentInChildren<Text>().text = conversation.items[currentConvIndex].options[1].text;
        }
        text.text = string.Empty;
        dialoguePanel.SetActive(true);
    }

    public void CloseDialoguePanel()
    {
        dialoguePanel.SetActive(false);
        dialogueOpened = false;
        option1Btn.onClick.RemoveListener(option1BtnClicked);
        option2Btn.onClick.RemoveListener(option2BtnClicked);
        if (hasTask)
        {
            actionbtn.SetActive(true);
        }
        
    }

}
