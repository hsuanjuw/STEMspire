using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFade : MonoBehaviour
{
    public float defaultAlpha = 1f;
    public float fadeSpeed = 0.4f;
    public bool fading = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (!fading)
        {
            Color _col = GetComponent<SpriteRenderer>().color;
            _col.a = defaultAlpha/255;
            GetComponent<SpriteRenderer>().color = _col;
            yield return new WaitForSeconds(Random.Range(0.7f,2f));
            if (!fading)
            {
                _col = GetComponent<SpriteRenderer>().color;
                _col.a = defaultAlpha/255/2;
                GetComponent<SpriteRenderer>().color = _col;
                yield return new WaitForSeconds(Random.Range(0.2f,0.4f)); 
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(fading)
            LowerAlpha();
    }

    public void LowerAlpha()
    {
        Color _col = GetComponent<SpriteRenderer>().color;
        _col.a -= (fadeSpeed*Time.deltaTime);
        if (_col.a <= 0)
        {
            _col.a = 0;
            fading = false;
        }
        GetComponent<SpriteRenderer>().color = _col;
    }
    public void ResetAlpha()
    {
        fading = false;
        Color _col = GetComponent<SpriteRenderer>().color;
        _col.a = defaultAlpha/255;
        GetComponent<SpriteRenderer>().color = _col;
        StartCoroutine(Blink());
    }
}
