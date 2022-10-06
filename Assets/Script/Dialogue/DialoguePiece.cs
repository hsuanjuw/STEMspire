using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialoguePiece 
{
    public string id;
    [Multiline]
    public string text;
    public Sprite image;
    //public AudioClip audio;
    //public Quest quest;
    public List<string> options;
}
