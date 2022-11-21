using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    /// <summary>
    /// Handle door close or open and which scene to enter.
    /// </summary>
    
    private GameObject leftDoor;
    private GameObject rightDoor;
    private float speed = 3;
    private bool doorIsOpen = false;
    private float originalDistance;

    private GameStatus gameStatus;
    private DialogueSystem dialogueSystem;

    // Start is called before the first frame update
    void Start()
    {
        leftDoor = this.transform.GetChild(0).gameObject;
        rightDoor = this.transform.GetChild(1).gameObject;
        originalDistance = Vector3.Distance(leftDoor.transform.position, rightDoor.transform.position);
        gameStatus = GameObject.FindObjectOfType<GameStatus>();
        dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        // Open the door if dialogue is not opened &&  door is not open
        if (!dialogueSystem.dialogueOpened && !(gameStatus.status == GameStatus.Status.spaceStation2 && !gameStatus.isFinishedDialogue)) 
        {
            if (!doorIsOpen)
            {
                doorIsOpen = true;
                StartCoroutine(OpenDoor());
            }
        }
    }

    void OnMouseExit()
    {
        // Close the door if dialogue is not opened &&  door is open
        if (!dialogueSystem.dialogueOpened && !(gameStatus.status == GameStatus.Status.spaceStation2 && !gameStatus.isFinishedDialogue))
        {
            if (doorIsOpen)
            {
                doorIsOpen = false;
                StartCoroutine(CloseDoor());
            }
        }
    }

    void OnMouseDown()
    {
        // Change scene if the dialogue is not opened
        if (!dialogueSystem.dialogueOpened)
        {
            // If the main dialogue is finished,
            // change to the next finale otherwise stay at original finale 
            switch (gameStatus.status)
            {
                case GameStatus.Status.spaceStation:
                    if (gameStatus.isFinishedDialogue)
                    {
                        SceneManager.LoadScene("Game_2");
                    }
                    else
                    {
                        SceneManager.LoadScene("Game");
                    }
                    break;
                case GameStatus.Status.spaceStation2:
                    if (gameStatus.isFinishedDialogue)
                    {
                        SceneManager.LoadScene("Game_3");
                    }
                    break;
                default:
                    break;
            }
        }
    }

    // Door Open movement
    IEnumerator OpenDoor()
    {
        while (Vector3.Distance(leftDoor.transform.position,rightDoor.transform.position) < 4.8f)
        {
            float step = speed * Time.deltaTime;
            leftDoor.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
            rightDoor.transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
            // move sprite towards the target location
            yield return null; 
        }
    }

    // Door close movement
    IEnumerator CloseDoor()
    {
        while (Vector3.Distance(leftDoor.transform.position, rightDoor.transform.position) > originalDistance)
        {
            float step = speed * Time.deltaTime;
            leftDoor.transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
            rightDoor.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
            // move sprite towards the target location
            yield return null;
        }
    }
}
