using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    /// <summary>
    /// Show Game status. 
    /// Will be used in changing scene in door.cs   
    /// </summary>
    
    public bool isFinishedDialogue = false; // whether the main dialogue is finished 
    public enum Status
    {
        spaceStation,
        spaceStation2,
    };
    public Status status;

}
