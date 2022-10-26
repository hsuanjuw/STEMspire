using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyominoRandomizer : MonoBehaviour
{
    public PolyominoBoard boardToRandom;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        boardToRandom.RandomizeBoard();
    }
}
