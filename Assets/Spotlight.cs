using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : MonoBehaviour
{
    private float highAlpha = 0.4f;
    private float lowAlpha = 0.1f;
    public void StartFlashing()
    {
        StartCoroutine(SpotFlash());
    }

    private IEnumerator SpotFlash()
    {
        Color _c;
        _c = GetComponent<SpriteRenderer>().color;
        _c.a = highAlpha;
        GetComponent<SpriteRenderer>().color = _c;
        yield return new WaitForSeconds(.5f);
        for (int flashes = 0; flashes < 4; flashes++)
        {
             _c = GetComponent<SpriteRenderer>().color;
            _c.a = lowAlpha;
            GetComponent<SpriteRenderer>().color = _c;
            yield return new WaitForSeconds(.5f);
            
            _c = GetComponent<SpriteRenderer>().color;
            _c.a = highAlpha;
            GetComponent<SpriteRenderer>().color = _c;
            yield return new WaitForSeconds(.5f);
        }
        _c = GetComponent<SpriteRenderer>().color;
        _c.a = 0;
        GetComponent<SpriteRenderer>().color = _c;
    }
}
