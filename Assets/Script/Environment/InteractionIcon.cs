using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionIcon : MonoBehaviour
{
    /// <summary>
    /// Handle InteractionIcon Shine Effect
    /// </summary>
    /// 
    public bool isInteracted = false;

    public Sprite orgrinalSprite;
    public Sprite envirShineInteractionIcon;
    public Sprite npcShineInteractionIcon;


    // Start is called before the first frame update
    void Start()
    {
        orgrinalSprite = this.GetComponent<SpriteRenderer>().sprite;
        if (this.tag == "NPCInteractionIcon")
        {
            StartCoroutine(Shine(npcShineInteractionIcon));
        }
        else if (this.tag == "EnvirInteractionIcon")
        {
            StartCoroutine(Shine(envirShineInteractionIcon));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Shine(Sprite shineSprite)
    {
        while (!isInteracted)
        {
            yield return new WaitForSeconds(0.2f);
            this.GetComponent<SpriteRenderer>().sprite = shineSprite;
            yield return new WaitForSeconds(0.2f);
            this.GetComponent<SpriteRenderer>().sprite = orgrinalSprite;
            yield return new WaitForSeconds(0.2f);
            this.GetComponent<SpriteRenderer>().sprite = shineSprite;
            yield return new WaitForSeconds(0.2f);
            this.GetComponent<SpriteRenderer>().sprite = orgrinalSprite;

            yield return new WaitForSeconds(1.5f);
        }
    }

}
