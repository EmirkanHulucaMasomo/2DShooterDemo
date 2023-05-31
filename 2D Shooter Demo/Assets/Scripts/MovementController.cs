using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public int MoveSpeed;
    public float JumpForce;
    private float moveX;
    private float moveY;
    private Vector2 MoveDir;
    public static string lookingDir;

    private Rigidbody2D rigBody;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask platformLayerMask;
    private Animator animator;
    private SpriteRenderer pSprite;

    private enum MoveState { idle,run}

    

    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        pSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = 0f;
        moveY = 0f;
        animator.SetBool("Running", false);

        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
            
            animator.SetBool("Running", true);
            animator.SetBool("LeftRun", false);
            pSprite.flipX = false;
            lookingDir = "Right";
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
            animator.SetBool("Running", true);
            animator.SetBool("LeftRun", true);
            pSprite.flipX = true;
            lookingDir = "Left";
            
        }
        if (Input.GetKeyDown(KeyCode.Space)&&IsGrounded())
        {
            rigBody.velocity = new Vector2(rigBody.velocity.x, JumpForce);
            animator.SetBool("Jumping", true);
            moveY = +1f;
            
        }
        if (IsGrounded() == true && rigBody.velocity.y == 0)
        {
            
        }
        animator.SetFloat("MoveX", Mathf.Abs(moveX));
        animator.SetFloat("YMove", rigBody.velocity.y);
        MoveDir = new Vector2(moveX, moveY).normalized;



    }

    private void FixedUpdate()
    {
        rigBody.velocity = new Vector2(MoveDir.x * MoveSpeed, rigBody.velocity.y);
    }


    private bool IsGrounded()
    {
        float extraHeight = 1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, extraHeight, platformLayerMask);
        Color rayColour;
        if (raycastHit.collider != null)
        {
            rayColour = Color.green;

            
        }
        else
        {
            rayColour = Color.red;
            
            

        }

        return raycastHit.collider != null;
    }



    private void OnLanding()
    {
        animator.SetBool("Jumping", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            OnLanding();

        }
    }
}




/*public class MovementController : MonoBehaviour
{
    public int MoveSpeed;
    public float Jump;
    private float moveX;
    private float moveY;
    private Vector3 MoveDir;
    private BoxCollider2D collider2D;

    private Rigidbody2D rigBody;

    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");



    }

    private void FixedUpdate()
    {
        if (moveX > 0.1f || moveX < -0.1f)
        {
            rigBody.AddForce(new Vector2(moveX * MoveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (moveY > 0.1f)
        {
            rigBody.AddForce(new Vector2(0f, moveY * Jump), ForceMode2D.Impulse);
        }
    }


    

}*/