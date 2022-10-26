using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public enum GameMode
    {
        MainMenu,
        Ingame,
        Credits
    };

    public GameMode currentGameMode;

    public Animator levelChangeAnimator;

    private string _nextSceneToLoad;
    

    public void SwitchScene(string _sceneNameToLoad)
    {
        levelChangeAnimator.SetTrigger("FadeOut");
        _nextSceneToLoad = _sceneNameToLoad;
    }

    public void SceneFadeComplete()
    {
        SceneManager.LoadScene(_nextSceneToLoad);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
