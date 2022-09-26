using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI text;
    public static List<string> dialogues;
    public float textSpeed;
    public GameObject backBtn;
    public GameObject guideNPC;
    private int dialogueNum;

    public Image npcImage;
    public GameObject dialoguePanel;
    public bool dialogueOpened;

    // Start is called before the first frame update
    void Start()
    {
        dialogues = new List<string>();
        text.text = string.Empty;
        dialogueOpened = false;
        string introDialoguePath = "Assets/Dialogues/Intro/dialogues1.txt";
        StartDialogue(introDialoguePath, guideNPC.GetComponent<SpriteRenderer>().sprite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(string path, Sprite npcSprite)
    {
        backBtn.SetActive(false);
        OpenDialoguePanel(npcSprite);
        ReadDialogueFile(path);
        dialogueNum = 0;
        StartCoroutine(TypeLine(0));
    }

    private IEnumerator TypeLine(int index)
    {
        Debug.Log(dialogueNum + " typing");
        foreach (char c in dialogues[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
            if (dialogueNum != index)
            {
                break;
            }
        }
    }

    public void NextLine()
    {
        Debug.Log("dialogues.Count:"+dialogues.Count);
        Debug.Log("dialogueNum:" + dialogueNum);
        if (dialogueNum < dialogues.Count - 1 )
        {
            dialogueNum++;
            if (dialogueNum == 1)
            {
                backBtn.SetActive(true);
            }
            text.text = string.Empty;
            StartCoroutine(TypeLine(dialogueNum));
        }
        else
        {
            CloseDialoguePanel();

        }
    }
    public void BackLine()
    {

        dialogueNum--;
        if (dialogueNum == 0)
        {
            backBtn.SetActive(false);
        }
        text.text = dialogues[dialogueNum];
    }

    public void ReadDialogueFile(string path)
    {
        dialogues = File.ReadAllLines(path).ToList();

        foreach ( string d in dialogues)
        {
            Debug.Log(d);
        }
    }

    public void OpenDialoguePanel(Sprite npcSprite)
    {
        dialogueOpened = true;
        npcImage.sprite = npcSprite;
        text.text = string.Empty;
        dialoguePanel.SetActive(true);
    }

    public void CloseDialoguePanel()
    {
        dialoguePanel.SetActive(false);
        dialogueOpened = false;
    }

}
