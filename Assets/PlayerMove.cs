using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public Vector2 direction;
    private Animator animator;

    public Vector2 attackDirection = Vector2.down;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        TakeInput();
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * GetComponent<Player>().MSpeed * Time.deltaTime);

        if (direction.x != 0 || direction.y != 0)
        {
            SetAnimatorMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }


    }
    bool wasMovingVertical = false;
    private void TakeInput()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        bool isMovingHorizontal = Mathf.Abs(horizontal) > 0.5f;

        float vertical = Input.GetAxisRaw("Vertical");
        bool isMovingVertical = Mathf.Abs(vertical) > 0.5f;
        
        if (isMovingVertical && isMovingHorizontal)
        {
            if (wasMovingVertical)
            {
                direction = new Vector2(horizontal, 0);
                attackDirection = direction;
            }
            else
            {
                direction = new Vector2(0, vertical);
                attackDirection = direction;
            }
        }
        else if (isMovingHorizontal)
        {
            direction = new Vector2(horizontal, 0);
            wasMovingVertical = false;
            attackDirection = direction;
        }
        else if (isMovingVertical)
        {
            direction = new Vector2(0, vertical);
            wasMovingVertical = true;
            attackDirection = direction;
        }
        else
        {
            direction = Vector2.zero;
        }

    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
    }
}
