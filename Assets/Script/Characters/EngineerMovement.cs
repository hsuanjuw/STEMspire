using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerMovement : CharacterMovement
{
    public override void Move()
    {
        DialogueSystem _ds = FindObjectOfType<DialogueSystem>();
        if (_ds != null && _ds.dialogueOpened)
            return;
        switch (currentMovement)
        {
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
                if (_currentDirection.x > 0)
                {
                    if(_currentDirection.x > 1)
                    _currentDirection.x = 1;
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (_currentDirection.x < 0)
                {
                    if(_currentDirection.x < -1)
                    _currentDirection.x = -1;
                    GetComponent<SpriteRenderer>().flipX = true;
                }

                if (Mathf.Abs(_currentDirection.x) < 0.08)
                {
                    GetComponent<Animator>().SetBool("Walking", false);
                    ChangeMovement(MovementType.Waiting);
                }
                else GetComponent<Animator>().SetBool("Walking", true);

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
        Vector3 tempVect = new Vector3(_currentDirection.x*hoverSpeed * Time.deltaTime, _currentDirection.y*hoverSpeed * Time.deltaTime, 0);
        transform.position += tempVect;
    }

    public override void ResetPosition()
    {
        base.ResetPosition();
        GetComponent<SpriteRenderer>().flipX = false;
    }
}
