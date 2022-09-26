using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DialogueSystem dialogueSystem;

    public GameObject teamLogo;
    // Start is called before the first frame update
    void Start()
    {
        dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
    }

}
