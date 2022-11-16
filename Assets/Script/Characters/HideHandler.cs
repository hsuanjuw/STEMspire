using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHandler : MonoBehaviour
{
    /// <summary>
    /// Handle whether the charactor should be show or not. 
    /// The charactor would be destoryed if it shouldn't be shown.
    /// </summary>

    public enum CharacterPrefs{
        Zun_Chat,
        Bot_Enthusiast_Chat,
        Parents_Chat,
        Service_Guide_Chat
    }

    public CharacterPrefs characterPrefs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.transform.GetChild(0).GetComponent<Renderer>().isVisible)
        {
            if (PlayerPrefs.GetInt(characterPrefs.ToString()) == 1)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
