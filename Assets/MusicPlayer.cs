using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource _source;
    public float lowerVolumeDuration;
    public float raiseVolumeDuration;
    public float muteVolume;
    public float normalVolume;
  
    public void StartLoweringVolume()
    {
        StartCoroutine(LowerVolume());
    }

    public void StartRaisingVolume()
    {
        StartCoroutine(RaiseVolume());
    }

    IEnumerator LowerVolume()
    {
        float currentTime = 0f;
        float startVolume = _source.volume;
        while (currentTime < lowerVolumeDuration)
        {
            currentTime += Time.deltaTime;
            _source.volume = Mathf.Lerp(startVolume, muteVolume, currentTime / lowerVolumeDuration);
            yield return null;
        }

        yield break;
    }
    IEnumerator RaiseVolume()
    {
        float currentTime = 0f;
        float startVolume = _source.volume;
        while (currentTime < raiseVolumeDuration)
        {
            currentTime += Time.deltaTime;
            _source.volume = Mathf.Lerp(startVolume, normalVolume, currentTime / raiseVolumeDuration);
            yield return null;
        }

        yield break;
    }
}