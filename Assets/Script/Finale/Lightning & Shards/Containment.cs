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
        if (currentStatus == MiniGameManager.GameStatus.NotStarted)
        {
            currentStatus = MiniGameManager.GameStatus.InProgress;
            StartCoroutine(ShardLoose(0));
        }
    }

    public IEnumerator ShardLoose(int shardIndex)
    {
        yield return new WaitForSeconds(timeGap);
        if (currentStatus == MiniGameManager.GameStatus.InProgress)
        {
            Debug.Log("Shard "+shardIndex);
            shard[shardIndex].SetActive(false);
            float z;
            if (shardIndex % 2 == 0)
            {
                FindObjectOfType<PowerCoreExplosion>().Explode();
                z = 135f;
            }
            else z = -135f;
            GameObject newShard = Instantiate(brokenShard, shard[shardIndex].transform.position, Quaternion.Euler(0, 0, z));
            newShard.GetComponent<SpriteRenderer>().sprite = shard[shardIndex].GetComponent<SpriteRenderer>().sprite;
            newShard.GetComponent<Rigidbody2D>().AddForce(newShard.transform.up * 80f);
            if (shardIndex == shard.Length - 1)
            {
                currentStatus = MiniGameManager.GameStatus.Failed;
                miniGameManager.CallRestart();
            }
            else StartCoroutine(ShardLoose(shardIndex + 1));
        }
    }

    private void Reset()
    {
        for (int i = 0; i < shard.Length; i++)
        {
            shard[i].SetActive(true);
        }
        currentStatus = MiniGameManager.GameStatus.NotStarted;
    }

    public void EndGame()
    {
        Reset();
    }
}
