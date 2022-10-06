using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float speed;
    private bool isSeen;
    SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        isSeen = false;
        renderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;

        if (renderer.isVisible)
            isSeen = true;

        if (isSeen && !renderer.isVisible)
            Destroy(gameObject);
    }
    // Update is called once per frame
    public void setValues(float _speed, Quaternion _rotation)
    {
        speed = _speed;
        this.gameObject.transform.rotation = _rotation;
    }
}