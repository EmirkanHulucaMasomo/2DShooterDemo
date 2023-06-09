using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpoContro : MonoBehaviour
{
    bool expandable;
    CircleCollider2D circleCollider;
    private readonly string enemytag = "Enemy";

    private EventManager cds;
    // Start is called before the first frame update

    void Start()
    {
        cds = GameObject.Find("GameManager").GetComponent<EventManager>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (circleCollider.radius < 2f)
        {
            circleCollider.radius += 0.5f * Time.timeScale;
            //transform.localScale += new Vector3(0.5f, 0.5f, 1) * Time.time * 2f;
        }
        
        

        if (Input.GetKey(KeyCode.B))
        {
            if (circleCollider.radius < 1.5f)
            {
                //circleCollider.radius += 0.5f * Time.time;
                //transform.localScale += new Vector3(0.5f, 0.5f, 1) * Time.time * 2f;
            }
            if (transform.localScale.x<5.5f)
            {
                //circleCollider.radius += 0.5f * Time.time;
                transform.localScale += new Vector3(0.5f, 0.5f, 1) * Time.time * 0.5f;
            }

            if (transform.lossyScale.x < 3f)
            {
                expandable = true;
            }
            else
            {
                expandable = false;
            }

        }
        if (expandable)
        {
            //transform.localScale = new Vector3(transform.localScale.x + 0.5f * Time.time, transform.localScale.y + 0.5f * Time.time, 0);
        }
    }



    public string ReturnId(Collider2D col)
    {
        return col.GetInstanceID().ToString();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        if (collision.tag == enemytag)
        {
            cds.SetId(ReturnId(collision),100);
        }
        circleCollider.enabled = false;
    }
}
