using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
   public enum AudioClipToTrigger
   {
      Start,
      Finale,
      Other
   };

   public AudioClipToTrigger triggerType;

   private void OnTriggerEnter2D(Collider2D other)
   {
      Debug.Log("Music Trigger");
      if (other.tag != "Player")
         return;
      switch (triggerType)
      {
         case AudioClipToTrigger.Start:
            FindObjectOfType<MusicPlayer>().SetStartMusic();
            break;
         case AudioClipToTrigger.Finale:
            FindObjectOfType<MusicPlayer>().SetFinaleMusic();
            break;
         case AudioClipToTrigger.Other:
            FindObjectOfType<MusicPlayer>().SetOtherMusic();
            break;
      }
   }
}
