using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public bool isFinishedDialogue = false;
    public enum Status
    {
        spaceStation,
        spaceStation2,
    };
    public Status status;

}
