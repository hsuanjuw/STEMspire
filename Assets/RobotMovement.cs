using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    public float hoverSpeed;

    public float leftBound;
    public float rightBound;
    public float upBound;
    public float downBound;
    public Vector2 _currentDirection;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempVect = new Vector3(_currentDirection.x*hoverSpeed * Time.deltaTime, _currentDirection.y*hoverSpeed * Time.deltaTime, 0);
        this.transform.position += tempVect;

        if (transform.localPosition.x > rightBound)
        {
            transform.localPosition = new Vector3(rightBound, transform.localPosition.y,transform.localPosition.z);
            RandomizeDirection();
        }
        else if (transform.localPosition.x < leftBound)
        {
            transform.localPosition = new Vector3(leftBound, transform.localPosition.y,transform.localPosition.z);
            RandomizeDirection();
        }
        if (transform.localPosition.y > upBound)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, upBound,transform.localPosition.z);
            RandomizeDirection();
        }
        else if (transform.localPosition.y < downBound)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, downBound, transform.localPosition.z);
            RandomizeDirection();
        }
    }

    void RandomizeDirection()
    {
        _currentDirection = Random.insideUnitCircle.normalized;
    }
}

