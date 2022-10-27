using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLightningBalls : MonoBehaviour
{
    public GameObject LightningBall;
    public bool spawning;
    public bool gameStart;
    Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
    }

    public void CallStartLightningballs()
    {
        gameStart = true;
        coroutine = StartCoroutine(StartLightningBalls());
    }

    public IEnumerator StartLightningBalls()
    {
        while (gameStart)
        {
            SpawnType2();
            yield return new WaitForSeconds(5f);
            SpawnType1();
            yield return new WaitForSeconds(5f);
            SpawnType3();
            yield return new WaitForSeconds(5f);
        }
    }

    // Update is called once per frame
    public void InitLightningBalls()
    {
        int type = Random.Range(1, 4);

        switch (type)
        {
            case 1:
                SpawnType1();
                break;
            case 2:
                SpawnType2();
                break;
            case 3:
                SpawnType3();
                break;
        }
    }

    public void SpawnType1()
    {
        int speed = 2;
        GameObject newLightningBall;
        Vector3 LightningBallPos = new Vector3(0, 1.26f, 0);
        newLightningBall = GameObject.Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
        newLightningBall.GetComponent<LightningBall>().setValues(speed, Quaternion.Euler(0f, 0f, 45f));

        newLightningBall = GameObject.Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
        newLightningBall.GetComponent<LightningBall>().setValues(speed, Quaternion.Euler(0f, 0f, 135f));

        newLightningBall = GameObject.Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
        newLightningBall.GetComponent<LightningBall>().setValues(speed, Quaternion.Euler(0f, 0f, 215f));

        newLightningBall = GameObject.Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
        newLightningBall.GetComponent<LightningBall>().setValues(speed, Quaternion.Euler(0f, 0f, 305f));
    }
    public void SpawnType2()
    {
        int speed = 2;
        GameObject newLightningBall;
        Vector3 LightningBallPos = new Vector3(0, 1.26f, 0);
        newLightningBall = GameObject.Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
        newLightningBall.GetComponent<LightningBall>().setValues(speed, Quaternion.Euler(0f, 0f, 0f));

        newLightningBall = GameObject.Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
        newLightningBall.GetComponent<LightningBall>().setValues(speed, Quaternion.Euler(0f, 0f, 90f));

        newLightningBall = GameObject.Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
        newLightningBall.GetComponent<LightningBall>().setValues(speed, Quaternion.Euler(0f, 0f, 180f));

        newLightningBall = GameObject.Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
        newLightningBall.GetComponent<LightningBall>().setValues(speed, Quaternion.Euler(0f, 0f, 270f));
    }
    public void SpawnType3()
    {
        SpawnType1();
        SpawnType2();
    }

    public void StopSpawning()
    {
        StopCoroutine(coroutine);
        gameStart = false;
    }
}
