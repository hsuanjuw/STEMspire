using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private GameObject leftDoor;
    private GameObject rightDoor;
    private float speed = 3;
    private bool doorIsOpen = false;
    private float originalDistance;

    private GameStatus gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        leftDoor = this.transform.GetChild(0).gameObject;
        rightDoor = this.transform.GetChild(1).gameObject;
        originalDistance = Vector3.Distance(leftDoor.transform.position, rightDoor.transform.position);
        gameStatus = GameObject.FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        //Debug.Log("Mouse is over GameObject.");
        if (!doorIsOpen)
        {
            doorIsOpen = true;
            StartCoroutine(OpenDoor());
        }
    }

    void OnMouseExit()
    {
        if (doorIsOpen)
        {
            doorIsOpen = false;
            StartCoroutine(CloseDoor());
        }
    }

    void OnMouseDown()
    {
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
                else
                {
                    SceneManager.LoadScene("Game_2");
                }
                break;
            default:
                break;
        }
    }

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
