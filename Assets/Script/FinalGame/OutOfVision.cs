using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfVision : MonoBehaviour
{
    private bool isSeen;
    private SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer.isVisible)
            isSeen = true;

        if (isSeen && !renderer.isVisible)
            Destroy(gameObject);
    }
}
