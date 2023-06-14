using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatController : MonoBehaviour
{
    public int maxHp;
    public int Hp;
    private bool isAlive;
    private Animator animator;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Text tmp;
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
        this.SetMaxPoints(this.maxHp);
        this.SetHpText(this.maxHp);
    }

    // Update is called once per frame
    void Update()
    {
           
    }
    public void PlayerisDead()
    {

    }
   
    public void LoseHealth(int damage,string id)
    {
        
        if (id==gameObject.GetComponent<BoxCollider2D>().GetInstanceID().ToString())
        {
            animator.SetBool("GotHit", true);
            this.Hp -= damage;
            this.SetPoints(this.Hp);
            this.SetHpText(this.Hp);
        }
        //Hp -= damage;
    }

    public void CancelHitAnim()
    {
        animator.SetBool("GotHit", false);
    }
    public void SetMaxPoints(int health)
    {
        hpBar.maxValue = health;
        hpBar.value = health;
    }
    public void SetPoints(int health)
    {
        hpBar.value = health;
    }
    private void SetHpText(int health)
    {
        if (this.tmp != null)
        {
            this.tmp.text = " "+health.ToString();
        }
    }
}
