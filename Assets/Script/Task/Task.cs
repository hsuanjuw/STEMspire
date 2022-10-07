using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Task : MonoBehaviour
{
    public bool isStartTask;
    public Button actionbtn;

    private Vector3 playerPos;
    private Vector3 offsetPos;
    // Start is called before the first frame update
    void Start()
    {
        isStartTask = false;
    }

    // Update is called once per frame
    void Update()
    {
/*        if (isStartTask)
        {
            Follow();
        }*/
    }

    public void StartTask()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        playerPos = player.transform.position;
        offsetPos = playerPos - this.transform.parent.position;
        actionbtn.gameObject.SetActive(false);
        isStartTask = true;
        SceneManager.LoadScene("Game_2");
    }

    public void Follow()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        playerPos = player.transform.position;
        this.transform.parent.position = playerPos - offsetPos;
    }

    public void StartTask2()
    {
        actionbtn.gameObject.SetActive(false);

        SceneManager.LoadScene("Game_3");
    }
}
