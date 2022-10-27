using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform targetPlayer;
    private Vector3 cameraOffset;
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        targetPlayer = GameObject.FindObjectOfType<Player>().transform;
        //cameraOffset = this.transform.position - targetPlayer.position;
        cameraOffset = this.transform.position - targetPlayer.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //71.81
        if (targetPlayer.position.x >= -71f && targetPlayer.position.x <= 47.2f)
        {
            this.transform.position = targetPlayer.position + cameraOffset;
            canvas.transform.position = new Vector3( this.transform.position.x, 0f, 0f );
        }
    }
}
