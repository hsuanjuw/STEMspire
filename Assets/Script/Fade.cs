using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public static Fade i;
    [Range(0.01f, 10f)]
    public float fadeInTime = 0.1f;
    [Range(0.01f, 10f)]
    public float stayTime = 0.05f;


    public GameObject teamLogo;

    // Start is called before the first frame update
    private void Awake()
    {
        i = this;
    }
    void Start()
    {
        //StartCoroutine(FadeInAndOut(teamLogo));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator FadeInAndOut(GameObject sprite)
    {
        StartCoroutine(FadeIn(sprite));

        Debug.Log("fadein");
        yield return new WaitForSeconds(stayTime);
        Debug.Log("fadeout");

        StartCoroutine(FadeOut(sprite));

    }

    IEnumerator FadeIn(GameObject sprite)
    {
        Image img = sprite.GetComponent<Image>();
        float timer = .0f;

        yield return new WaitForSeconds(fadeInTime);

        sprite.SetActive(true);

        while (timer < fadeInTime)
        {
            timer += Time.deltaTime;

            Color newColor = img.color;
            //newColor.a = timer / fadeInTime;
            newColor.a = timer / fadeInTime;
            img.color = newColor;

            yield return null;
        }
    }
    IEnumerator FadeOut(GameObject sprite)
    {
        Image img = sprite.GetComponent<Image>();
        float timer = .0f;

        timer = .0f;
        while (timer < fadeInTime)
        {
            timer += Time.deltaTime;
            Color newColor = img.color;
            //newColor.a = 1.0f - (timer / fadeInTime);
            newColor.a = 1.0f - timer;
            img.color = newColor;
            yield return null;
        }
        sprite.SetActive(false);
    }
}
