using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    private Systems systems;
    public int symbolNum;

    public GameObject activeSymbol;

    public GameObject inactiveSymbol;

    private bool symbolActive = false;
    // Start is called before the first frame update
    void Start()
    {
        systems = GameObject.FindObjectOfType<Systems>();
    }

    public void SymbolClicked()
    {
        if (systems.currentStatus != MiniGameManager.GameStatus.Failed)
        {
            switch (symbolActive)
            {
                case true:
                    activeSymbol.SetActive(false);
                    inactiveSymbol.SetActive(true);
                    symbolActive = false;
                    break;
                case false:
                    activeSymbol.SetActive(true);
                    inactiveSymbol.SetActive(false);
                    symbolActive = true;
                    break;
            }
        }
    }

    public void ResetSymbol()
    {
        activeSymbol.SetActive(false);
        inactiveSymbol.SetActive(true);
        symbolActive = false;
    }
}
