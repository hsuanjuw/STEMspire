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

    public enum ShipIntegrity
    {
        Broken,
        Fixed
    };

    public ShipIntegrity currentIntegrity = ShipIntegrity.Broken;
    public GameStatus currentStatus = GameStatus.NotStarted;
   // private StoryManager storyManager;
    private SpawnLightningBalls lightningBalls;
    private Camera mainCamera;
    private Player player;
    public bool gameStarted;
    private bool gamePlayed;
    private bool isCameraShake;
    public bool isCameraUp;
    private DialogueSystem dialogueSystem;
    private bool notFinished = true;

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
        CheckStartDialogue();
        if(currentIntegrity == ShipIntegrity.Fixed)
            GameStart();
    }

    private void CheckStartDialogue()
    {
        string playerPrefsKey = "";
        switch (SceneManager.GetActiveScene().name)
        {
            case "Game":
                playerPrefsKey = "Game1_dialogueStarted";
                break;
            case "Game_2":
                playerPrefsKey = "Game2_dialogueStarted";
                break;
            case "Game_3":
                playerPrefsKey = "Game3_dialogueStarted";
                break;
            default:
                break;
        }
        if (PlayerPrefs.GetInt(playerPrefsKey) == 0)
        {
            PlayerPrefs.SetInt(playerPrefsKey, 1);
            dialogueSystem.StartDialogueIntro();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCameraShake)
        {
            CameraShake();
        }

        if (currentStatus == GameStatus.InProgress && AllMinigameStatus() == GameStatus.Completed)
        {
            currentStatus = GameStatus.Completed;
            FindObjectOfType<ButtonFlash>().MakeClickable();
        }
    }

    public void GameStart()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            if (currentIntegrity == ShipIntegrity.Broken)
            {
                launchBtnPressedCount++;
                CountdownTxt.gameObject.SetActive(true);
                GameObject.Find("Light").GetComponent<LightFade>().fading = true;
                StartCoroutine(Countdown()); 
            }
            else
            {
                StartMiniGame();
                currentStatus = GameStatus.InProgress; 
            }
        }
    }

    public void InitiateFinalLaunch()
    {
        if (currentStatus == GameStatus.Completed)
        {
            launchBtnPressedCount++;
            CountdownTxt.gameObject.SetActive(true);
            GameObject.Find("Light").GetComponent<LightFade>().fading = true;
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

        if (currentIntegrity == ShipIntegrity.Broken)
        {
            CountdownTxt.gameObject.SetActive(false);
            StartCoroutine(SpaceshipCrack()); 
        }
        else
        {
            for (int i = 2; i > 0; i--)
            {
                CountdownTxt.text = i.ToString();
                yield return new WaitForSeconds(1f);
            }
            CountdownTxt.text = "Launch!";
            FindObjectOfType<SpaceScrolling>().currentScroll = SpaceScrolling.ScrollType.Up;
            yield return new WaitForSeconds(1f);
            CountdownTxt.gameObject.SetActive(false);
            CallSuccess();
        }
    }

    private IEnumerator SpaceshipCrack()
    {
        FindObjectOfType<SpaceScrolling>().currentScroll = SpaceScrolling.ScrollType.Angle;
        FindObjectOfType<PowerCoreExplosion>().Explode();
        FindObjectOfType<MusicPlayer>().SetFinaleMusic();
        PostProcessVolume volume = mainCamera.gameObject.GetComponent<PostProcessVolume>();
        RobotMovement _robotMovement = FindObjectOfType<RobotMovement>();
        if(_robotMovement!=null)
            _robotMovement.ChangeMovement(RobotMovement.RobotMovementType.Hide);
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
        currentStatus = GameStatus.InProgress;
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
        if(currentStatus != GameStatus.Failed)
            StartCoroutine(Restart());
    }

    public void CallSuccess()
    {
        if(currentStatus != GameStatus.Failed)
            StartCoroutine(Victory());
    }

    private IEnumerator Victory()
    {
        WinAllMiniGames();
        foreach (var ball in FindObjectsOfType<LightningBall>())
        {
            ball.KillBall();
        }
        FindObjectOfType<MusicPlayer>().SetVictoryMusic();
        if(!lightningBalls.finishedLoop && lightningBalls.gameStart)
            lightningBalls.StopSpawning();
        FindObjectOfType<PowerCoreExplosion>().ResetLightning();
        yield return new WaitForSeconds(2f);
        FindObjectOfType<ScreenFader>().SwitchScene("Thanks");
    }
    private IEnumerator Restart()
    {
        currentStatus = GameStatus.Failed;
        foreach (var ball in FindObjectsOfType<LightningBall>())
        {
            ball.KillBall();
        }
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
        currentStatus = GameStatus.NotStarted;
        GameObject.Find("Light").GetComponent<LightFade>().ResetAlpha();
        RobotMovement _robotMovement = FindObjectOfType<RobotMovement>();
        if (_robotMovement != null)
        {
            _robotMovement.ResetPosition();
            _robotMovement.ChangeMovement(RobotMovement.RobotMovementType.Floating);
        }
            
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
    private void WinAllMiniGames()
    {
        wheel.currentStatus = GameStatus.Completed;
        systems.currentStatus = GameStatus.Completed;
        power.currentStatus = GameStatus.Completed;
    }

    public GameStatus AllMinigameStatus()
    {
        if (wheel.currentStatus == GameStatus.Completed && systems.currentStatus == GameStatus.Completed &&
            power.currentStatus == GameStatus.Completed)
            return GameStatus.Completed;
        else return GameStatus.InProgress;
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
