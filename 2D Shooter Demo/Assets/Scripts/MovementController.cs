using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

public class MovementController : MonoBehaviour
{
    public int MoveSpeed;
    public float JumpForce;
    public float DashForce;
    private float moveX;
    private float moveY;
    private Vector2 MoveDir;
    public static string lookingDir;
    public bool dashing;

    private BoxCollider2D enCollider;
    private Rigidbody2D rigBody;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask platformLayerMask;
    private Animator animator;
    private SpriteRenderer pSprite;
    private EventManager eventManager;

    [SerializeField] private GameObject dashvFX;
    
    private enum MoveState { idle,run}

    

    void Start()
    {
        lookingDir = "Right";
        rigBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        pSprite = GetComponent<SpriteRenderer>();
        eventManager = GameObject.Find("GameManager").GetComponent<EventManager>();
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
            
            
            if (GunController.shooting == false)
            {
                pSprite.flipX = false;
                lookingDir = "Right";
            }
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
            animator.SetBool("Running", true);
            
            if (GunController.shooting == false)
            {
                pSprite.flipX = true;
                lookingDir = "Left";
            }
            
            
        }
        if (Input.GetKey(KeyCode.M))
        {
            animator.SetBool("Dashing", true);
            dashvFX.SetActive(true);
            dashing = true;
            //rigBody.velocity = new Vector2(DashForce, rigBody.velocity.y);
            if (moveX == 1f)
            {
                dashvFX.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                rigBody.AddForce(new Vector2(4f, 0f), ForceMode2D.Force);
            }
            if (moveX == -1f)
            {
                dashvFX.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                rigBody.AddForce(new Vector2(-4f, 0f), ForceMode2D.Force);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Space)&&IsGrounded())
        {
            rigBody.velocity = new Vector2(rigBody.velocity.x, JumpForce);
            animator.SetBool("Jumping", true);
            moveY = +1f;
            
        }
        
        animator.SetFloat("MoveX", Mathf.Abs(moveX));
        animator.SetFloat("YMove", rigBody.velocity.y);
        MoveDir = new Vector2(moveX, moveY).normalized;

        eventManager.SetDir(lookingDir);

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

    private string ReturnId()
    {
        return boxCollider2D.GetInstanceID().ToString();
    }


    private void OnLanding()
    {
        animator.SetBool("Jumping", false);
    }


    public void DashStart()
    {

    }
    public void DashEnd()
    {
        dashvFX.SetActive(false);
        animator.SetBool("Dashing", false);
        dashing = false;
        
        eventManager.ResetColliders(boxCollider2D);
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            OnLanding();

        }
        if (collision.collider.tag == "Enemy")
        {
            
            if (dashing == true)
            {
                Physics2D.IgnoreCollision(boxCollider2D, collision.collider, true);
            }

            if (dashing == false&&collision.otherCollider.tag!="Sword")
            {
                
                eventManager.SetId(ReturnId(), 5);
            }
            
            
        }
    }
}