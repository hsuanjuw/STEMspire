using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerMovement : CharacterMovement
{
    public float patrolWait;
    private bool patrolDone = true;
    public float[] patrolLocations;
    private int patrolIndex = 0;
    private float animatorSpeed = 1f;
    private Animator _engiAnimator;
    private SpriteRenderer _spriteRenderer;

    protected virtual void Start()
    {
        Animator _a = GetComponent<Animator>();
        if (_a == null)
            _a = GetComponentInChildren<Animator>();
        _engiAnimator = _a;
        
        SpriteRenderer _s = GetComponent<SpriteRenderer>();
        if (_s == null)
            _s = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer = _s;
    }
    private IEnumerator Patrol()
    {
        prevFollowX = patrolLocations[patrolIndex];
        yield return new WaitForSeconds(patrolWait);
        patrolIndex++;
        patrolIndex %= patrolLocations.Length;
        patrolDone = true;
    }
    public override void Move()
    {
        DialogueSystem _ds = FindObjectOfType<DialogueSystem>();
        if (_ds != null && _ds.dialogueOpened)
        {
            _engiAnimator.speed = 0f;
            return;
        }
        else
        {
            _engiAnimator.speed = animatorSpeed;
        }
            
        switch (currentMovement)
        {
            case MovementType.Hide:
                _currentDirection = _hidingPlace - transform.position;
                _currentDirection.y = 0;
                if (_currentDirection.magnitude < .1)
                {
                    _currentDirection = Vector2.zero;
                }
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
                if (Mathf.Abs(_currentDirection.x) < 0.08)
                {
                    ChangeMovement(MovementType.Waiting);
                }
                break;
            case MovementType.Patrol:
                if (patrolDone)
                {
                    patrolDone = false;
                    StartCoroutine(Patrol());
                }
                else
                {
                    _currentDirection.x = prevFollowX - transform.position.x;
                }
                break;
            case MovementType.Waiting:
                if(_currentDirection!= Vector2.zero)
                    _currentDirection = Vector2.zero;
                if(Input.GetAxisRaw("Horizontal") != 0)
                    ChangeMovement(MovementType.FollowPlayer);
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
                break;
        }
        if (_currentDirection.x > 0)
        {
            if(_currentDirection.x > 1)
                _currentDirection.x = 1;
            _spriteRenderer.flipX = false;
        }
        else if (_currentDirection.x < 0)
        {
            if(_currentDirection.x < -1)
                _currentDirection.x = -1;
            _spriteRenderer.flipX = true;
        }

        if (Mathf.Abs(_currentDirection.x) < 0.08)
        {
            _engiAnimator.SetBool("Walking", false);
        }
        else _engiAnimator.SetBool("Walking", true);
        Vector3 tempVect = new Vector3(_currentDirection.x*hoverSpeed * Time.deltaTime, _currentDirection.y*hoverSpeed * Time.deltaTime, 0);
        transform.position += tempVect;
    }

    public override void ResetPosition()
    {
        base.ResetPosition();
        patrolIndex = 0;
        patrolDone = true;
        _spriteRenderer.flipX = false;
    }
}
