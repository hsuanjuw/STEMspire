using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public enum GameStatus
    {
        NotStarted,
        InProgress,
        Failed,
        Completed
    };

    private StoryManager storyManager;
    private SpawnFireballs spawnFireBalls;
    private Camera mainCamera;
    private Player player;
    private bool gameStarted;
    private bool gamePlayed;
    private bool isCameraShake;
    public bool isCameraUp;
    private Analytic analytic;

    // Mini games
    private Wheel wheel;
    private Screws screws;
    private Systems systems;
    private Power power;

    [SerializeField] private int cameraShakeSpeed = 3;
    [SerializeField] private Text CountdownTxt;
    [SerializeField] private Text ErrorTxt;
    [SerializeField] private GameObject FailImage;


    void Awake()
    {
        isCameraUp = true;
        isCameraShake = false;
        gameStarted = false;
    }

    void Start()
    {
        mainCamera = Camera.main;
        storyManager = GameObject.FindObjectOfType<StoryManager>();
        spawnFireBalls = GameObject.FindObjectOfType<SpawnFireballs>();
        player = GameObject.FindObjectOfType<Player>();
        analytic = GameObject.FindObjectOfType<Analytic>();

        wheel = GameObject.FindObjectOfType<Wheel>();
        screws = GameObject.FindObjectOfType<Screws>();
        systems = GameObject.FindObjectOfType<Systems>();
        power = GameObject.FindObjectOfType<Power>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCameraShake)
        {
            CameraShake();
        }
    }

    public void GameStart()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            CountdownTxt.gameObject.SetActive(true);
            StartCoroutine(Countdown());
        }
    }
    private IEnumerator Countdown()
    {
        //analytic.SaveData("Launch Button Clicked");
        //analytic.SaveIntoJson();
        for (int i = 5; i > 2; i--)
        {
            CountdownTxt.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        CountdownTxt.gameObject.SetActive(false);
        StartCoroutine(SpaceshipCrack());
    }

    private IEnumerator SpaceshipCrack()
    {
        FindObjectOfType<PowerCoreExplosion>().Explode();
        FindObjectOfType<MusicPlayer>().SetFinaleMusic();
        PostProcessVolume volume = mainCamera.gameObject.GetComponent<PostProcessVolume>();
        isCameraShake = true;
        for (int i = 0; i < 5; i++)
        {
            ErrorTxt.gameObject.SetActive(true);
            volume.enabled = true;
            yield return new WaitForSeconds(.5f);
            ErrorTxt.gameObject.SetActive(false);
            volume.enabled = false;
            yield return new WaitForSeconds(.5f);
        }
        isCameraShake = false;
        mainCamera.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        // sth cracking??
        spawnFireBalls.CallStartFireballs();
        StartMiniGame();
    }

    private void CameraShake()
    {
        if (isCameraUp)
        {
            mainCamera.transform.Rotate(new Vector3(0, 0, cameraShakeSpeed * Time.deltaTime));
        }
        else
        {
            mainCamera.transform.Rotate(new Vector3(0, 0, -cameraShakeSpeed * Time.deltaTime));
        }

        
        if (mainCamera.transform.eulerAngles.z > 1.0f && mainCamera.transform.eulerAngles.z < 2.0f)
        {
            isCameraUp = false;
        }
        if (mainCamera.transform.eulerAngles.z < 359f && mainCamera.transform.eulerAngles.z > 358f)
        {
            isCameraUp = true;
        }
    }

    public void CallRestart()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        FindObjectOfType<PowerCoreExplosion>().Explode();
        spawnFireBalls.StopSpawning();
        //FailImage.SetActive(true);
        FindObjectOfType<MusicPlayer>().SetOtherMusic();
        yield return new WaitForSeconds(1f);
        FindObjectOfType<ScreenFader>().levelChangeAnimator.SetTrigger("FinaleFadeOut");
        yield return new WaitForSeconds(5f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player.ResetPosition();
        FindObjectOfType<MusicPlayer>().SetStartMusic();
        FindObjectOfType<PowerCoreExplosion>().ResetLightning();
        FindObjectOfType<ScreenFader>().levelChangeAnimator.SetTrigger("FinaleFadeIn");
        FailImage.SetActive(false);
        //dialogueSystem.StartDialogueIntro();
        storyManager.NextStatus();
        EndMiniGame();
        Debug.Log("Restart");
        gameStarted = false;
    }

    public void EnterSpaceStation(int num)
    {
        if (!gameStarted)
        {
            if (num == 1)
            {
                SceneManager.LoadScene("SpaceStation");
            }
            else
            {
                SceneManager.LoadScene("SpaceStation" + num.ToString());
            }
        }

    }

    private void StartMiniGame()
    {
        wheel.StartGame();
        screws.StartGame();
        systems.StartGame();
        power.StartGame();
    }

    private void EndMiniGame()
    {
        wheel.RestartGame();
        screws.EndGame();
        systems.EndGame();
        power.EndGame();
    }
}
