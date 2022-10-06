using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (isStartTask)
        {
            Follow();
        }
    }

    public void StartTask()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        playerPos = player.transform.position;
        offsetPos = playerPos - this.transform.parent.position;
        actionbtn.gameObject.SetActive(false);
        isStartTask = true;
        Debug.Log(this.transform.parent.name);
        Debug.Log(this.transform.parent.position);
    }

    public void Follow()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        playerPos = player.transform.position;
        this.transform.parent.position = playerPos - offsetPos;
    }
}
