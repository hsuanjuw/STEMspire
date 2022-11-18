using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public bool triggerActive = true;
    public int sceneToLoad;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (triggerActive)
        {
            if (col.transform.tag == "Player")
            {
                FindObjectOfType<MiniGameManager>().EnterSpaceStation(sceneToLoad);
            }
        }
    }
}
