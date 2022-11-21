using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPGM.Gameplay;

public class Player : MonoBehaviour
{
    public float speed;
    public bool npcClicked;
    public Vector2 npcPosition;
    private MiniGameManager miniGameManager;
    private Vector3 originalPos;
    public Vector3 resetPosition;
    public Vector3 altSpawnPosition;
    private DialogueSystem dialogueSystem;
    public GameObject hitByBallPrefab;

    private float animatorSpeed = 1f;

    private float runSpeed = 6f;

    private float walkSpeed = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        miniGameManager = GameObject.FindObjectOfType<MiniGameManager>();
        dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
        switch (SceneManager.GetActiveScene().name)
        {
            case "Game":
                if (PlayerPrefs.GetInt("Game1_dialogueStarted") == 1)
                {
                    transform.position = altSpawnPosition;
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                break;
        }
/*        if (SceneManager.GetActiveScene().name == "SpaceStation")
        {
            this.transform.position
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueSystem.dialogueOpened)
        {
            GetComponent<Animator>().speed = animatorSpeed;
            Move();
        }
        else
        {
            GetComponent<Animator>().speed = 0f;
        }
        
    }
    
    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if(h.Equals(0f))
            GetComponent<Animator>().SetBool("Walking",false);
        else GetComponent<Animator>().SetBool("Walking",true);
        if (Input.GetKey(KeyCode.LeftShift))
            speed = runSpeed;
        else speed = walkSpeed;
        Vector3 tempVect = new Vector3(h * speed * Time.deltaTime, 0, 0);
        this.transform.position += tempVect;
        bool facingRight = GetComponent<SpriteRenderer>().flipX;
        if (facingRight && tempVect.x < 0)
            GetComponent<SpriteRenderer>().flipX = false;
        else if(!facingRight && tempVect.x > 0)
            GetComponent<SpriteRenderer>().flipX = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.tag);
        if (col.CompareTag("Fireball") && miniGameManager.gameStarted)
        {
            col.GetComponent<LightningBall>().KillBall();
            Instantiate(hitByBallPrefab,col.transform.position,col.transform.rotation);
            miniGameManager.CallRestart();
        }
/*        else if (col.name == "IntroDialogueTrigger")
        {
            dialogueSystem.StartDialogueIntro();
            Destroy(col.gameObject);
        }
        else if (col.name == "Engineer2DialogueTrigger")
        {
            ConversationScript conversation = GameObject.Find("Engineer 2").GetComponentInChildren<ConversationScript>();
            dialogueSystem.StartDialogue(conversation, false);
            Destroy(col.gameObject);
        }*/
    }

    public void ResetPosition()
    {
        transform.position = resetPosition;
    }


}
