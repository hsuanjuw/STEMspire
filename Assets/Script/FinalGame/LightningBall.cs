using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBall : MonoBehaviour
{
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IEKillBall());
    }
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    IEnumerator IEKillBall()
    {
        yield return new WaitForSeconds(10f);
        KillBall();
    }

    public void KillBall()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    public void setValues(float _speed, Quaternion _rotation)
    {
        speed = _speed;
        this.gameObject.transform.rotation = _rotation;
    }
}