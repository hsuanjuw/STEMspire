using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PityTimer : MonoBehaviour
{
    public float pityTimeDefault;

    public float pityTimeRemaining;

    public float alphaOverTimeInterval;
    public SpriteRenderer _spre;
    public enum PityType
    {
        AlphaOverTime,
        AlphaPopup,
        IntroduceDialog,
    };

    public PityType currentType;

    private Coroutine activeCo;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (pityTimeRemaining > 0)
        {
            pityTimeRemaining -= Time.deltaTime;
            if (pityTimeRemaining <= 0)
            {
                pityTimeRemaining = 0;
                if (currentType == PityType.AlphaPopup)
                {
                    Color c = _spre.color;
                    c.a = 1;
                    _spre.color = c;
                }
            }
        }
    }

    public void StartTimer()
    {
        ResetTimer();
        switch (currentType)
        {
            case PityType.AlphaOverTime:
                if (activeCo != null)
                {
                    StopCoroutine(activeCo);
                    activeCo = null;
                }
                activeCo = StartCoroutine(IncreaseAlphaOverTime());
                break;
            case PityType.AlphaPopup:
                Color c = _spre.color;
                c.a = 0;
                _spre.color = c;
                break;
        }
    }

    private IEnumerator IncreaseAlphaOverTime()
    {
        Color c = _spre.color;
        c.a = 0;
        _spre.color = c;
        while (_spre.color.a < 1f)
        {
            yield return new WaitForSeconds(alphaOverTimeInterval);
            c = _spre.color;
            c.a += alphaOverTimeInterval / pityTimeDefault;
            _spre.color = c;
        }
    }

    public void ResetTimer()
    {
        pityTimeRemaining = pityTimeDefault;
    }

    public void EndTimer()
    {
        pityTimeRemaining = 0f;
    }

    public void Hide()
    {
        EndTimer();
        _spre.enabled = false;
    }
}
