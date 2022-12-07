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

    [System.Serializable]
    public struct CutsceneEvent
    {
        public int frame;
        public UnityEvent _event;
    }
    public CutsceneEvent[] _indexEvents;
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
            foreach (CutsceneEvent _ce in _indexEvents)
            {
                if(_ce.frame == _index)
                    _ce._event.Invoke();
            }
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
