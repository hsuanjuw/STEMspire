using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutscenePlayer : MonoBehaviour
{
    public Sprite[] allSprites;

    public SpriteRenderer renderPoint;

    public float displayTime;

    public UnityEvent playOnFinish;
    // Start is called before the first frame update
    public void StartCutscene()
    {
        StartCoroutine(Cutscene());
    }

    private IEnumerator Cutscene()
    {
        int _index = 0;
        
        while (_index < allSprites.Length)
        {
            renderPoint.sprite = allSprites[_index];
            yield return new WaitForSeconds(displayTime);
            _index++;
        }

        Finished();
    }
    public void Finished()
    {
        playOnFinish.Invoke();
    }
}
