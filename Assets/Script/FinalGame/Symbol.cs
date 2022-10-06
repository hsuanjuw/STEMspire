using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    private Systems systems;
    public int symbolNum;
    // Start is called before the first frame update
    void Start()
    {
        systems = GameObject.FindObjectOfType<Systems>();
        symbolNum = (int)Char.GetNumericValue(this.name[6]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SymbolClicked()
    {
        systems.symbolsClicked[symbolNum - 1] = !systems.symbolsClicked[symbolNum - 1];
        if (systems.symbolsClicked[symbolNum - 1])
        {
            this.GetComponent<Image>().color = new Color(189f/255f, 244f/255, 1f, 1f);
        }
        else
        {
            this.GetComponent<Image>().color = Color.white;
        }
        
    }
}
