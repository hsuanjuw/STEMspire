using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IEKillExplosion());
    }

    void Update()
    {
        transform.Rotate(0f,0f,500 * Time.deltaTime);
        float scaleFactor = .2f;
        transform.localScale +=new Vector3(scaleFactor*Time.deltaTime,scaleFactor*Time.deltaTime,0);
    }

    public IEnumerator IEKillExplosion()
    {
        yield return new WaitForSeconds(2f);
        KillExplosion();
    }

    public void KillExplosion()
    {
        Destroy(gameObject);
    }
}
