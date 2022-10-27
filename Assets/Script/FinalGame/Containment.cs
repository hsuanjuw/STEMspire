using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Containment : MonoBehaviour
{
    public MiniGameManager.GameStatus currentStatus = MiniGameManager.GameStatus.NotStarted;
    private GameObject[] shard;
    public GameObject brokenShard;
    public float timeGap = 10f; // Every 10f, a shard loosen
    [HideInInspector] public bool isFixed; // For future use

    private MiniGameManager miniGameManager;

    // Start is called before the first frame update
    void Start()
    {
        shard = GameObject.FindGameObjectsWithTag("Shard");
        isFixed = false;
        miniGameManager = GameObject.FindObjectOfType<MiniGameManager>();
    }


    public void StartGame()
    {
        currentStatus = MiniGameManager.GameStatus.InProgress;
        StartCoroutine(ShardLoose());
    }

    public IEnumerator ShardLoose()
    {
        for (int i = 0; i < shard.Length; i++)
        {
            if (currentStatus != MiniGameManager.GameStatus.InProgress)
            {
                break;
            }
            yield return new WaitForSeconds(timeGap);
            shard[i].SetActive(false);
            float z;
            if ( i % 2 == 0)
            {
                z = 135f;
            }
            else
            {
                z = -135f;
            }
            
            GameObject newShard = Instantiate(brokenShard, shard[i].transform.position, Quaternion.Euler(0, 0, z));
            newShard.GetComponent<SpriteRenderer>().sprite = shard[i].GetComponent<SpriteRenderer>().sprite;
            newShard.GetComponent<Rigidbody2D>().AddForce(newShard.transform.up * 80f);
            FindObjectOfType<PowerCoreExplosion>().Explode();
            if (i == shard.Length-1)
            {
                currentStatus = MiniGameManager.GameStatus.Failed;
                miniGameManager.CallRestart();
            }
        }

    }

    private void Reset()
    {
        for (int i = 0; i < shard.Length; i++)
        {
            shard[i].SetActive(true);
        }
    }

    public void EndGame()
    {
        currentStatus = MiniGameManager.GameStatus.NotStarted;
        Reset();
    }
}
