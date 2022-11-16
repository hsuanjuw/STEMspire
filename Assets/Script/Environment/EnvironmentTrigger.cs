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
    private Analytic analytic;

    //For recording time that popups on
    public static float startTime;
    public static float endTime;
    public static string objPressedName;
    // Start is called before the first frame update
    void Start()
    {
        conversation = this.GetComponent<ConversationScript>();
        dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
        analytic = GameObject.FindObjectOfType<Analytic>();
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
        }
        else if(enviroDialogueIsStart)
        {
            endTime = Time.time;
            float timePassed = Mathf.Round((endTime - startTime) * 100f) / 100f;

            analytic.SaveData("EnvirObjClicked", timePassed, objPressedName);
            //Debug.Log(objPressedName + " :" + timePassed.ToString());
            StopCoroutine(coroutine);
            text.text = conversation.items[0].text;
            coroutine = StartCoroutine(OpenPanel());
        }

        if (this.GetComponentInChildren<InteractionIcon>())
        {
            Destroy(this.GetComponentInChildren<InteractionIcon>().gameObject);
        }
        startTime = Time.time;
        objPressedName = this.name;
        //Debug.Log("Environment Trigger clicked");
    }

    public IEnumerator OpenPanel()
    {
        environmentDialogue.SetActive(true);
        yield return new WaitForSeconds(3f);
        environmentDialogue.SetActive(false);
        enviroDialogueIsStart = false;
        endTime = Time.time;
        float timePassed = Mathf.Round((endTime - startTime) * 100f) / 100f;
        analytic.SaveData("EnvirObjClicked", timePassed, objPressedName);
        //Debug.Log(objPressedName + " :" + timePassed.ToString());
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
