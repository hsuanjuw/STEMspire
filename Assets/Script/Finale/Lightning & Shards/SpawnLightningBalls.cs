using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLightningBalls : MonoBehaviour
{
    public GameObject LightningBall;
    public float ballSpeed = 2f;
    public bool gameStart;
    public bool looping = true;
    Coroutine coroutine;
    [HideInInspector]
    public bool finishedLoop = false;

    public enum ProjectilePattern
    {
        Cross,
        X,
        Wave
    };

    public ProjectilePattern nextPattern = ProjectilePattern.Wave;

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
        while (gameStart && !finishedLoop)
        {
            SpawnBalls(nextPattern);
            nextPattern = nextPattern+1;
            if (nextPattern > (ProjectilePattern)2)
            {
                if (looping)
                    nextPattern = ProjectilePattern.Cross;
                else finishedLoop = true;
            }
            yield return new WaitForSeconds(5f);
        }
    }

    public void SpawnBalls(ProjectilePattern _pattern)
    {
        GameObject newLightningBall;
        Vector3 LightningBallPos = new Vector3(0, 2f, 0);
        
        switch (_pattern)
        {
            case ProjectilePattern.Cross:
                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 0f));

                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 90f));

                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 180f));

                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 270f));
                break;
            case ProjectilePattern.X:
                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 45f));

                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 135f));

                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 225f));

                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 315f));
                break;
            case ProjectilePattern.Wave:
                int skip = Random.Range(0, 3);
                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 0f));

                
                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 90f));
                
                
                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 180f));
                
                if(skip != 0)
                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 270f));
                
                
                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 45f));
                
                
                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 135f));

                if(skip != 2)
                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 225f));

                if(skip != 1)
                newLightningBall = Instantiate(LightningBall, LightningBallPos, Quaternion.identity);
                newLightningBall.GetComponent<LightningBall>().setValues(ballSpeed, Quaternion.Euler(0f, 0f, 315f));
                break;
        }
    }

    public void StopSpawning()
    {
        StopCoroutine(coroutine);
        gameStart = false;
    }
}
