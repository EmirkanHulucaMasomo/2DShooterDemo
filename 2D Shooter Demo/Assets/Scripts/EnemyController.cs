using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

public class EnemyController : MonoBehaviour
{
    StatController stats;
    Animator animator;
    Rigidbody2D rigBody;
    BoxCollider2D boxCollider;
    float moveX;
    float moveY;
    Vector2 MoveDir;
    float MoveSpeed = 5f;
    public bool isAlive;

    private void OnEnable()
    {
        EventManager.onDashEnd += ResColliders;
    }
    private void OnDisable()
    {
        EventManager.onDashEnd -= ResColliders;
    }
    void Start()
    {
        stats= GetComponent<StatController>();
        rigBody= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        boxCollider= GetComponent<BoxCollider2D>();
        isAlive = true;
        moveX = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.Hp <= 0)
        {
            isAlive= false;
        }
        if (isAlive)
        {
            if (moveX == +1f)
            {
                transform.eulerAngles = new Vector3(0f,0f, 0f);
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            
        }
        else
        {
            moveX= 0f;
            animator.SetBool("Dead", true);


        }
        MoveDir = new Vector2(moveX, moveY);

    }
    private void FixedUpdate()
    {
        rigBody.velocity = new Vector2(MoveDir.x * MoveSpeed, rigBody.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            if (!isAlive)
            {
                rigBody.gravityScale= 0f;
                boxCollider.enabled = false;
            }
        }

        if (collision.collider.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(boxCollider,collision.collider);
        }
        if(collision.collider.tag == "Shell")
        {
            Physics2D.IgnoreCollision(boxCollider, collision.collider);
        }

        if (collision.collider.tag == "Wall")
        {
            if (moveX == -1f)
            {
                moveX = +1f;
            }
            else if (moveX == +1f)
            {
                moveX = -1f;
            }
        }
    }
    public void ResColliders(BoxCollider2D pCo)
    {
        Physics2D.IgnoreCollision(boxCollider, pCo, false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            if (moveX == -1f)
            {
                moveX = +1f;
            }
            else if(moveX == +1f)
            {
                moveX = -1f;
            }
        }
    }
}
