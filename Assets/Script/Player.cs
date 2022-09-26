using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool npcClicked;
    public Vector2 npcPosition;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    
    public void move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 tempVect = new Vector3(h * speed * Time.deltaTime, 0, 0);
        this.transform.position += tempVect;
    }
}
