using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFireballs : MonoBehaviour
{
    public GameObject fireBall;
    public bool spawning;
    public bool gameStart;
    Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
    }

    public void CallStartFireballs()
    {
        gameStart = true;
        coroutine = StartCoroutine(StartFireballs());
    }

    public IEnumerator StartFireballs()
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
    public void InitFireballs()
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
        GameObject newFireball;
        Vector3 fireBallPos = new Vector3(0, 1.26f, 0);
        newFireball = GameObject.Instantiate(fireBall, fireBallPos, Quaternion.identity);
        newFireball.GetComponent<FireBall>().setValues(speed, Quaternion.Euler(0f, 0f, 45f));

        newFireball = GameObject.Instantiate(fireBall, fireBallPos, Quaternion.identity);
        newFireball.GetComponent<FireBall>().setValues(speed, Quaternion.Euler(0f, 0f, 135f));

        newFireball = GameObject.Instantiate(fireBall, fireBallPos, Quaternion.identity);
        newFireball.GetComponent<FireBall>().setValues(speed, Quaternion.Euler(0f, 0f, 215f));

        newFireball = GameObject.Instantiate(fireBall, fireBallPos, Quaternion.identity);
        newFireball.GetComponent<FireBall>().setValues(speed, Quaternion.Euler(0f, 0f, 305f));
    }
    public void SpawnType2()
    {
        int speed = 2;
        GameObject newFireball;
        Vector3 fireBallPos = new Vector3(0, 1.26f, 0);
        newFireball = GameObject.Instantiate(fireBall, fireBallPos, Quaternion.identity);
        newFireball.GetComponent<FireBall>().setValues(speed, Quaternion.Euler(0f, 0f, 0f));

        newFireball = GameObject.Instantiate(fireBall, fireBallPos, Quaternion.identity);
        newFireball.GetComponent<FireBall>().setValues(speed, Quaternion.Euler(0f, 0f, 90f));

        newFireball = GameObject.Instantiate(fireBall, fireBallPos, Quaternion.identity);
        newFireball.GetComponent<FireBall>().setValues(speed, Quaternion.Euler(0f, 0f, 180f));

        newFireball = GameObject.Instantiate(fireBall, fireBallPos, Quaternion.identity);
        newFireball.GetComponent<FireBall>().setValues(speed, Quaternion.Euler(0f, 0f, 270f));
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
