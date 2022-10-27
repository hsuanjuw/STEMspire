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
    public AudioClip startMusic;
    public AudioClip finaleMusic;
    public AudioClip otherMusic;
    public AudioClip victoryMusic;

    public void SetStartMusic()
    {
        SetMusic(startMusic);
    }

    public void SetFinaleMusic()
    {
        SetMusic(finaleMusic);
    }

    public void SetOtherMusic()
    {
        SetMusic(otherMusic);
    }

    public void SetVictoryMusic()
    {
        SetMusic(victoryMusic);
    }

    public void SetMusic(AudioClip clip)
    {
        if (clip != _source.clip)
        {
            _source.clip = clip;
            _source.Play();  
        }
    }
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
