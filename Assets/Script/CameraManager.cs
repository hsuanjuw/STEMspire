using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform targetPlayer;
    private Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindObjectOfType<Player>().transform;
        //cameraOffset = this.transform.position - targetPlayer.position;
        cameraOffset = this.transform.position - new Vector3(84.37f, -2.86f, 0); // offset count when player is in the middle

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //71.81
        if (targetPlayer.position.x >= 0f && targetPlayer.position.x <= 84.37f)
        {
            this.transform.position = targetPlayer.position + cameraOffset;
        }
    }
}
