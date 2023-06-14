using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rigBody;
    public float moveX;
    private float moveY;
    private Vector2 MoveDir;

    [SerializeField] private GameObject ImpactEffect;
    [SerializeField]
    int damage;
    private readonly string enemytag = "Enemy";
    private EventManager eventManager;
    private bool explosive;

    public delegate void OnHitAction(int dmg);
    public static event OnHitAction OnHit;
    private void OnEnable()
    {
        explosive = false;

        int rand = UnityEngine.Random.Range(0, 11);
        if (rand == 1 || rand == 3 || rand == 5)
        {
            explosive = true;
        }
    }
    void Start()
    {
        eventManager=GameObject.Find("GameManager").GetComponent<EventManager>();
        
        rigBody= GetComponent<Rigidbody2D>();
        
        //InvokeRepeating("DestroySelf", 3f,2.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveDir = new Vector2(moveX, moveY);
        
    }
    private void FixedUpdate()
    {
        rigBody.velocity = new Vector2(MoveDir.x * 40, rigBody.velocity.y);
    }

    private void DestroySelf()
    {
        
        gameObject.SetActive(false);

    }
    
    public string ReturnId(Collision2D col)
    {
        return col.collider.GetInstanceID().ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject ImpactE = ObjectPool.SharedInstance.GetPooledObject("IEffect");
        GameObject ExpoE = ObjectPool.SharedInstance.GetPooledObject("Explosion");
        if (ImpactE != null && ExpoE != null)
        {
            ImpactE.transform.position=transform.position;
            
            if (moveX == 1)
            {
                ImpactE.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (moveX == -1)
            {
                ImpactE.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            ImpactE.SetActive(true);
            if(explosive)
            {
                ExpoE.transform.position =transform.position;
                ExpoE.SetActive(true);
            }
        }
        
        if (collision.collider.tag ==enemytag)
        {
            
            if (moveX == +1f)
            {
                collision.rigidbody.AddForce(new Vector2(25f, 0f),ForceMode2D.Force);
            }
            else
            {
                collision.rigidbody.AddForce(new Vector2(-25f, 0f), ForceMode2D.Force);
            }
            /*if (OnHit != null)
            {
                OnHit(damage);
            }*/

            eventManager.SetId(ReturnId(collision),5);


        }
        if (collision.collider.tag != "Bullet" || collision.collider.tag != "Player")
        {
            gameObject.SetActive(false);
        }
        
    }
}
