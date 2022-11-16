using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySelf(3f));
    }

    private IEnumerator DestroySelf(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
