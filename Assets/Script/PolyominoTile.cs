using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyominoTile : MonoBehaviour
{
   public GameObject tileOn;
   public GameObject tileOff;
   public bool acceptInput;
   public enum TileState
   {
      On,
      Off
   };

   public TileState CurrentState = TileState.Off;

   void Start()
   {
      switch (CurrentState)
      {
         case TileState.Off:
            SetOff();
            break;
         case TileState.On:
            SetOn();
            break;
      }
   }

   public void OnMouseDown()
   {
      if(acceptInput)
         SwitchState();
   }

   public void SwitchState()
   {
      switch (CurrentState)
      {
         case TileState.Off:
            CurrentState = TileState.On;
            SetOn();
            break;
         case TileState.On:
            CurrentState = TileState.Off;
            SetOff();
            break;
      }
   }

   public void SetOff()
   {
      tileOn.SetActive(false);
      tileOff.SetActive(true);
      CurrentState = TileState.Off;
   }
   public void SetOn()
   {
      tileOn.SetActive(true);
      tileOff.SetActive(false);
      CurrentState = TileState.On;
   }
}
