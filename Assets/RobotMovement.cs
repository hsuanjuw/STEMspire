using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    public enum RobotMovementType
    {
        Floating,
        FollowPlayer,
        LeadPlayer,
        Hide,
        Random,
        Idling
    };

    public RobotMovementType currentMovement = RobotMovementType.Floating;
    public float hoverSpeed;

    public float leftBound;
    public float rightBound;
    public float upBound;
    public float downBound;
    public Vector2 _currentDirection = Vector2.zero;

    public Vector3 defaultPosition;

    public Vector3 _hidingPlace;

    public int _hidingLayer;

    private bool peeking = false;
    private int peeks = 0;
    private float peekSpeed = 3000f;
    // Start is called before the first frame update

    public void StopPeeking()
    {
        peeking = false;
    }
    void MoveRobot()
    {
        switch (currentMovement)
        {
            case RobotMovementType.Hide:
                if (GetComponent<SpriteRenderer>().sortingLayerID != _hidingLayer)
                    GetComponent<SpriteRenderer>().sortingLayerID = _hidingLayer;
                _currentDirection = _hidingPlace - transform.position;
                if (_currentDirection.magnitude < .1)
                {
                    _currentDirection = Vector2.zero;
                    if (!peeking)
                    {
                        peeking = true;
                        GetComponent<Animator>().SetTrigger("Peek");
                    }
                }
                break;
            case RobotMovementType.Floating:
                if(_currentDirection == Vector2.zero)
                    _currentDirection = Vector2.down;
                if (transform.position.y > upBound)
                {
                    transform.position = new Vector3(transform.position.x, upBound,transform.position.z);
                    _currentDirection = Vector2.down;
                }
                else if (transform.position.y < downBound)
                {
                    transform.position = new Vector3(transform.position.x, downBound, transform.position.z);
                    _currentDirection = Vector2.up;
                }
                break;
            case RobotMovementType.Random:
                if (transform.position.x > rightBound)
                {
                    transform.position = new Vector3(rightBound, transform.position.y,transform.position.z);
                    RandomizeDirection();
                }
                else if (transform.position.x < leftBound)
                {
                    transform.position = new Vector3(leftBound, transform.position.y,transform.position.z);
                    RandomizeDirection();
                }
                if (transform.position.y > upBound)
                {
                    transform.position = new Vector3(transform.position.x, upBound,transform.position.z);
                    RandomizeDirection();
                }
                else if (transform.localPosition.y < downBound)
                {
                    transform.position = new Vector3(transform.position.x, downBound, transform.position.z);
                    RandomizeDirection();
                }
                break;
        }
        Vector3 tempVect = new Vector3(_currentDirection.x*hoverSpeed * Time.deltaTime, _currentDirection.y*hoverSpeed * Time.deltaTime, 0);
        this.transform.position += tempVect;
    }
    // Update is called once per frame
    void Update()
    {
        MoveRobot();
    }

    public void ResetPosition()
    {
        transform.position = defaultPosition;
        transform.rotation = Quaternion.identity;
    }
    public void ChangeMovement(RobotMovementType newMovement)
    {
        currentMovement = newMovement;
        _currentDirection = Vector2.zero;
    }
    void RandomizeDirection()
    {
        _currentDirection = Random.insideUnitCircle.normalized;
    }
}

