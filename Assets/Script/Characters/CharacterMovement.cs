using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public enum MovementType
    {
        Floating,
        FollowPlayer,
        LeadPlayer,
        Hide,
        Random,
        Waiting,
        Patrol,
        Idling,
        Frozen,
        FollowEngineer,
    };

    public MovementType currentMovement = MovementType.Floating;
    public float hoverSpeed;

    public float leftBound;
    public float rightBound;
    public float upBound;
    public float downBound;
    public Vector2 _currentDirection = Vector2.zero;

    public Vector3 defaultPosition;

    public Vector3 _hidingPlace;

    public int _hidingLayer;

    protected bool peeking = false;

    protected float prevFollowX = float.NaN;

    public virtual void StopPeeking()
    {
        peeking = false;
    }
    public virtual void Move()
    {
        DialogueSystem _ds = FindObjectOfType<DialogueSystem>();
        if (_ds != null && _ds.dialogueOpened)
            return;
        switch (currentMovement)
        {
            case MovementType.FollowEngineer:
                EngineerMovement _em = FindObjectOfType<EngineerMovement>();
                if(_currentDirection.y != 0)
                    _currentDirection.y = 0;
                
                _currentDirection.x = _em.transform.position.x - transform.position.x;
                
                break;
            case MovementType.FollowPlayer:
                Player _p = FindObjectOfType<Player>();
                float x_offset = 2f;
                if(_currentDirection.y != 0)
                _currentDirection.y = 0;

                if (Input.GetAxisRaw("Horizontal") == 0)
                {
                    if (float.IsNaN(prevFollowX))
                    {
                        if (_p.GetComponent<SpriteRenderer>().flipX)
                            x_offset *= -1;
                        prevFollowX = _p.transform.position.x + x_offset;
                    }
                }
                else
                {
                    if (Input.GetAxisRaw("Horizontal") > 0)
                        x_offset *= -1;
                    prevFollowX = _p.transform.position.x + x_offset;
                }
                
                _currentDirection.x =  prevFollowX - transform.position.x;

                if(Mathf.Abs(_currentDirection.x)<0.08)
                    ChangeMovement(MovementType.Waiting);
                break;
            case MovementType.Waiting:
                if(_currentDirection!= Vector2.zero)
                    _currentDirection = Vector2.zero;
                if(Input.GetAxisRaw("Horizontal") != 0)
                    ChangeMovement(MovementType.FollowPlayer);
                break;
            case MovementType.Hide:
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
            case MovementType.Floating:
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
            case MovementType.Random:
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
        transform.position += tempVect;
    }
    // Update is called once per frame
    public virtual void Update()
    {
        Move();
    }

    public virtual void ResetPosition()
    {
        transform.position = defaultPosition;
        transform.rotation = Quaternion.identity;
        StopPeeking();
    }
    public virtual void ChangeMovement(MovementType newMovement)
    {
        currentMovement = newMovement;
        _currentDirection = Vector2.zero;
        prevFollowX = float.NaN;
    }
    public virtual void RandomizeDirection()
    {
        _currentDirection = Random.insideUnitCircle.normalized;
    }
}
