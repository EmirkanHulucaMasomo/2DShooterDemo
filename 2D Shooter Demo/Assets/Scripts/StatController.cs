using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatController : MonoBehaviour
{
    public int Hp;
    private bool isAlive;
    private Animator animator;
    // Start is called before the first frame update

    private void OnEnable()
    {
        EventManager.onCol += LoseHealth;
    }
    private void OnDisable()
    {
        EventManager.onCol -= LoseHealth;
    }

    void Start()
    {
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
           
    }

   
    public void LoseHealth(int damage,string id)
    {
        
        if (id==gameObject.GetComponent<BoxCollider2D>().GetInstanceID().ToString())
        {
            animator.SetBool("GotHit", true);
            this.Hp -= damage;
        }
        //Hp -= damage;
    }

    public void CancelHitAnim()
    {
        animator.SetBool("GotHit", false);
    }


}
