using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public bool npcClicked;
    public Vector2 npcPosition;
    private MiniGameManager miniGameManager;
    private Vector3 originalPos;
    private DialogueSystem dialogueSystem;


    // Start is called before the first frame update
    void Start()
    {
        miniGameManager = GameObject.FindObjectOfType<MiniGameManager>();
        dialogueSystem = GameObject.FindObjectOfType<DialogueSystem>();
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
            Move();
        }
        
    }
    
    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
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
        Debug.Log(col.tag);
        if (col.CompareTag("Fireball") && miniGameManager.gameStarted)
        {
            miniGameManager.CallRestart();
        }
        else if (col.name == "IntroDialogueTrigger")
        {
            dialogueSystem.StartDialogueIntro();
            Destroy(col.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "EnterSpaceStationTrigger")
        {
            miniGameManager.EnterSpaceStation(1);
        }
        if (col.gameObject.name == "EnterSpaceStationTrigger2")
        {
            miniGameManager.EnterSpaceStation(2);
        }
    }

    public void ResetPosition()
    {
        this.transform.position = new Vector3(-5.44f, -2.73f, 0);
    }


}
