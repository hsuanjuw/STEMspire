using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPGM.Gameplay;

public class EnvironmentTrigger : MonoBehaviour
{
    public static bool enviroDialogueIsStart = false;
    public static Coroutine coroutine = null;

    public GameObject environmentDialogue;
    public TextMeshProUGUI text;

    private ConversationScript conversation;

    private DialogueSystem dialogueSystem;
    // Start is called before the first frame update
    void Start()
    {
        conversation = this.GetComponent<ConversationScript>();
        dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseEnter()
    {
        Hover();
    }

    private void OnMouseExit()
    {
        NotHover();
    }

    void OnMouseDown()
    {
        NotHover();
        if (!enviroDialogueIsStart && !dialogueSystem.dialogueOpened)
        {
            enviroDialogueIsStart = true;
            text.text = conversation.items[0].text;
            coroutine = StartCoroutine(OpenPanel());

            if (this.GetComponentInChildren<InteractionIcon>())
            {
                Destroy(this.GetComponentInChildren<InteractionIcon>().gameObject);
            }
        }
        else if(enviroDialogueIsStart)
        {
            StopCoroutine(coroutine);
            text.text = conversation.items[0].text;
            coroutine = StartCoroutine(OpenPanel());
        }

        //Debug.Log("Environment Trigger clicked");
    }

    public IEnumerator OpenPanel()
    {
        environmentDialogue.SetActive(true);
        yield return new WaitForSeconds(3f);
        environmentDialogue.SetActive(false);
        enviroDialogueIsStart = false;

    }

    private void Hover()
    {
        Color newColor = this.GetComponent<SpriteRenderer>().color;
        newColor.a = 0.5f;
        this.GetComponent<SpriteRenderer>().color = newColor;
    }

    private void NotHover()
    {
        Color newColor = this.GetComponent<SpriteRenderer>().color;
        newColor.a = 1f;
        this.GetComponent<SpriteRenderer>().color = newColor;
    }
}
