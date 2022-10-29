using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RPGM.Gameplay;

public class MiniGameManager : MonoBehaviour
{
    public enum GameStatus
    {
        NotStarted,
        InProgress,
        Failed,
        Completed
    };
   // private StoryManager storyManager;
    private SpawnLightningBalls lightningBalls;
    private Camera mainCamera;
    private Player player;
    public bool gameStarted;
    private bool gamePlayed;
    private bool isCameraShake;
    public bool isCameraUp;
    private DialogueSystem dialogueSystem;

    //Analytics
    private Analytic analytic;
    public int launchBtnPressedCount;

    // Mini games
    public Wheel wheel;
    public Containment shards;
    public Systems systems;
    public Power power;

    [SerializeField] private int cameraShakeSpeed = 3;
    [SerializeField] private Text CountdownTxt;
    [SerializeField] private Text ErrorTxt;


    void Awake()
    {
        isCameraUp = true;
        isCameraShake = false;
        gameStarted = false;
    }

    void Start()
    {
        launchBtnPressedCount = 0;
        mainCamera = Camera.main;
        lightningBalls = GameObject.FindObjectOfType<SpawnLightningBalls>();
        player = GameObject.FindObjectOfType<Player>();
        analytic = GameObject.FindObjectOfType<Analytic>();
        dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
        dialogueSystem.StartDialogueIntro();
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
            launchBtnPressedCount++;
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
        lightningBalls.CallStartLightningballs();
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

    public void CallSuccess()
    {
        StartCoroutine(Victory());
        
    }

    private IEnumerator Victory()
    {
        FindObjectOfType<MusicPlayer>().SetVictoryMusic();
        lightningBalls.StopSpawning();
        FindObjectOfType<PowerCoreExplosion>().ResetLightning();
        yield return new WaitForSeconds(2f);
        FindObjectOfType<ScreenFader>().SwitchScene("Thanks");
    }
    private IEnumerator Restart()
    {
        LoseAllMiniGames();
        FindObjectOfType<PowerCoreExplosion>().Explode();
        lightningBalls.StopSpawning();
        FindObjectOfType<MusicPlayer>().SetOtherMusic();
        yield return new WaitForSeconds(1f);
        FindObjectOfType<ScreenFader>().levelChangeAnimator.SetTrigger("FinaleFadeOut");
        yield return new WaitForSeconds(5f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player.ResetPosition();
        FindObjectOfType<MusicPlayer>().SetStartMusic();
        FindObjectOfType<PowerCoreExplosion>().ResetLightning();
        FindObjectOfType<ScreenFader>().levelChangeAnimator.SetTrigger("FinaleFadeIn");
        if (launchBtnPressedCount == 1)
        {
            StartRestartDialogue();
        }
        
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
                analytic.SaveData("EnterSpaceStation", Time.time);
                analytic.SaveData("LaunchBtnPressedCount", Time.time, launchBtnPressedCount);
                SceneManager.LoadScene("SpaceStation");
            }
            else
            {
                analytic.SaveData("EnterSpaceStation2", Time.time);
                analytic.SaveData("LaunchBtnPressedCount", Time.time, launchBtnPressedCount);
                SceneManager.LoadScene("SpaceStation" + num.ToString());
            }
        }

    }

    private void StartMiniGame()
    {
        wheel.StartGame();
        shards.StartGame();
        systems.StartGame();
        power.StartGame();
    }

    private void LoseAllMiniGames()
    {
        wheel.currentStatus = GameStatus.Failed;
        systems.currentStatus = GameStatus.Failed;
        power.currentStatus = GameStatus.Failed;
    }

    private void EndMiniGame()
    {
        wheel.RestartGame();
        if(shards.currentStatus != GameStatus.Completed)
            shards.EndGame();
        systems.EndGame();
        power.RestartGame();
        FindObjectOfType<ButtonFlash>().ResetFlash();
    }
    
    private void StartRestartDialogue()
    {
        ConversationScript npcConversation = dialogueSystem.transform.GetChild(1).GetComponent<ConversationScript>();
        dialogueSystem.StartDialogue(npcConversation, false);
    }
}
