using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCoreColorChanger : MonoBehaviour
{
    public enum CoreColor
    {
        Blue,
        Red
    };

    public CoreColor currentColor;

    private CoreColor _trueColor;
    // Start is called before the first frame update
    void Start()
    {
        _trueColor = currentColor;
        UpdateSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentColor != _trueColor)
            UpdateSprite();
    }

    public void SetColor(CoreColor c)
    {
        currentColor = c;
    }

    public void UpdateSprite()
    {
        switch (currentColor)
        {
            case CoreColor.Blue:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case CoreColor.Red:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
        }
        _trueColor = currentColor;
    }
}
