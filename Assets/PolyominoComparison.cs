using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyominoComparison : MonoBehaviour
{
    public PolyominoBoard boardOne;
    public PolyominoBoard boardTwo;

    public GameObject lightOn;
    public GameObject lightOff;

    private void Update()
    {
        CompareBoards(boardOne, boardTwo);
    }

    public void CompareBoards(PolyominoBoard one, PolyominoBoard two)
    {
        for (int tileIndex = 0;tileIndex < one.allTiles.Length;tileIndex++)
        {
            if (one.allTiles[tileIndex].CurrentState != two.allTiles[tileIndex].CurrentState)
            {
                LightOff();
                return;
            }
        }
        LightOn();
    }

    public void LightOn()
    {
        lightOn.SetActive(true);
        lightOff.SetActive(false);
    }
    
    public void LightOff()
    {
        lightOff.SetActive(true);
        lightOn.SetActive(false);
    }
}
