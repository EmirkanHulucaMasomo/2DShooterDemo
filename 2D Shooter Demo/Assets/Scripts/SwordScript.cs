using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    private PolygonCollider2D polCollider;
    private EventManager eventManager;
    // Start is called before the first frame update
    void Start()
    {
        eventManager = GameObject.Find("GameManager").GetComponent<EventManager>();
        polCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string ReturnId(Collision2D col)
    {
        return col.collider.GetInstanceID().ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Enemy")
        {
            eventManager.SetId(ReturnId(collision), 10);
            if (MovementController.lookingDir == "Right")
            {
                collision.rigidbody.AddForce(new Vector2(40f, 0f), ForceMode2D.Force);
            }
            else
            {
                collision.rigidbody.AddForce(new Vector2(-40f, 0f), ForceMode2D.Force);
            }
        }
    }
}
