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


    private void Awake()
    {
        dialogueOpened = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "SpaceStation")
        {
            StartDialogueIntro();
        }
        //StartDialogueIntro();
    }

    public void StartDialogueIntro()
    {
        conversation = this.transform.GetChild(0).GetComponent<ConversationScript>();
        /*hasTask = false;
        npcImage.sprite = guideRobotSprite;
        currentConvIndex = 0;
        bool isEnd = CheckEndConversation();
        OpenDialoguePanel(isEnd);
        StartCoroutine(TypeLine(conversation.items[currentConvIndex].text, isEnd));*/

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
            StartCoroutine(DisplayOption());
        }
        else
        {
            yield return new WaitForSeconds(2f);
            CloseDialoguePanel();
        }

    }

    private IEnumerator TypeLine(string dialogue, Sprite sprite) // pc talking
    {
        Debug.Log("Type2");
        yield return new WaitForSeconds(2f);
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


    public IEnumerator DisplayOption()
    {
        if (conversation.items[currentConvIndex].options.Count == 1)
        {
            if (conversation.items[currentConvIndex].options[0].text == "")
            {
                yield return new WaitForSeconds(1f);
                NextLine(0);
            }
            else
            {
                yield return new WaitForSeconds(2f);
                StartCoroutine(TypeLine(conversation.items[currentConvIndex].options[0].text, conversation.items[currentConvIndex].options[0].image));
            }
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
