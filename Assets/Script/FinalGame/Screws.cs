using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screws : MonoBehaviour
{
    private GameObject[] screws;
    public GameObject screwLong;
    public float timeGap = 10f; // Every 10f, a screw loosen
    [HideInInspector] public bool isFixed; // For future use

    private MiniGameManager miniGameManager;

    // Start is called before the first frame update
    void Start()
    {
        screws = GameObject.FindGameObjectsWithTag("Screw");
        isFixed = false;
        miniGameManager = GameObject.FindObjectOfType<MiniGameManager>();
        //StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(ScrewLoose());
    }

    public IEnumerator ScrewLoose()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(timeGap);
            screws[i].SetActive(false);
            float z;
            if ( i % 2 == 0)
            {
                z = 135f;
            }
            else
            {
                z = -135f;
            }
            
            GameObject newScrew = Instantiate(screwLong, screws[i].transform.position, Quaternion.Euler(0, 0, z));
            newScrew.GetComponent<Rigidbody2D>().AddForce(newScrew.transform.up * 80f);
            if ( i == 4)
            {
                miniGameManager.CallRestart();
            }
        }

    }

    private void Reset()
    {
        for (int i = 0; i < 4; i++)
        {
            screws[i].SetActive(true);
        }
    }
}
