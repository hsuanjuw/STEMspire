using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPolyPattern", menuName = "ScriptableObjects/PolyominoPattern", order = 1)]
public class PolyominoPattern : ScriptableObject
{
    public PolyominoTile.TileState[] allTilesStates = new PolyominoTile.TileState[9];
}
