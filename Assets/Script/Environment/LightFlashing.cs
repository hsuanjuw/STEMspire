using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlashing : MonoBehaviour
{
    float randomWaitTime;
    Color color;

    // Start is called before the first frame update
    void Start()
    {
        Color color = new Color(1f, 1f, 1f, 1f);
        Debug.Log(color);
        StartCoroutine(flash());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator flash()
    {
        randomWaitTime = Random.Range(3f, 5f);
        yield return new WaitForSeconds(randomWaitTime);
        while (true)
        {
            int flickerTime = Random.Range(2, 6);

            for(int i = 0; i < flickerTime; i++)
            {
                color = new Color(1f, 1f, 1f, Random.Range(0f, 0.7f));
                this.GetComponent<SpriteRenderer>().color = color;
                
                yield return new WaitForSeconds(0.2f);
                color = new Color(1f, 1f, 1f, 1f);
                this.GetComponent<SpriteRenderer>().color = color;
                Debug.Log(this.GetComponent<SpriteRenderer>().color);
                randomWaitTime = Random.Range(0f, 0.8f);
                yield return new WaitForSeconds(randomWaitTime);
            }
            color = new Color(1f, 1f, 1f, 1f);
            this.GetComponent<SpriteRenderer>().color = color;
            randomWaitTime = Random.Range(3f, 7f);
            yield return new WaitForSeconds(randomWaitTime);

        }

    }


}
