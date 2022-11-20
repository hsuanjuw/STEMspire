using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRobotDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Robot_Stay") == 1)
        {
            Destroy(GameObject.Find("GuideRobot"));
            Destroy(GameObject.Find("ConversationScript_RCPresent"));
        }
        else
        {
            Destroy(GameObject.Find("ConversationScript_RCNotPresent"));
        }
    }

}
