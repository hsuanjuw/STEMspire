using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyominoBoard : MonoBehaviour
{
    public PolyominoTile[] allTiles;

    public PolyominoPattern[] allPatterns;

    private PolyominoPattern currentPattern;
    // Start is called before the first frame update
    public void RandomizeBoard()
    {
        PolyominoPattern newPattern = allPatterns[Random.Range(0, allPatterns.Length)];
        for(int tries = 0;tries < 10;tries++)
        {
            if (newPattern != currentPattern)
                break;
            newPattern = allPatterns[Random.Range(0, allPatterns.Length)];
        }

        currentPattern = newPattern;
        for (int tileIndex = 0; tileIndex < allTiles.Length; tileIndex++)
        {
            if(newPattern.allTilesStates[tileIndex] == PolyominoTile.TileState.On)
                allTiles[tileIndex].SetOn();
            else allTiles[tileIndex].SetOff();
        }
    }
}
