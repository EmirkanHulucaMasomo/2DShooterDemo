using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public static bool shooting;
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject Sword;
    private SpriteRenderer gunRenderer;
    [SerializeField] private Transform muzzlePoint;
    private Animator playerAnimator;
    private Rigidbody2D rigbody;
    private EventManager eventManager;
    private float nextShootTime;
    public float fireRateCdr;
    private bool bursting;
    public int gunChoser;
    
    public enum GunType {Rifle,auto,burst }

    public GunType gunType;
    // Start is called before the first frame update
    void Start()
    {
        rigbody= GetComponent<Rigidbody2D>();
        playerAnimator= GetComponent<Animator>();
        eventManager = GameObject.Find("GameManager").GetComponent<EventManager>();
        gunRenderer = Gun.GetComponent<SpriteRenderer>();
        gunChoser = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.L))
        {
            Sword.SetActive(true);
            playerAnimator.SetBool("MeleeAttack", true);
            if (EventManager.lookDirr != "Right")
            {
                Sword.transform.eulerAngles=new Vector3(0,180,0);
                Sword.transform.localPosition = new Vector3(-1.8f, 0.18f, 0);
            }
            else
            {
                Sword.transform.eulerAngles = new Vector3(0, 0, 0);
                Sword.transform.localPosition = new Vector3(1.8f, 0.18f, 0);
            }
        }
        if (shooting == true)
        {
            switch (gunType)
            {
                case GunType.Rifle:
                    shooting = true;
                    if (Time.time > nextShootTime)
                    {
                        Shoot();
                        float fireRate = 0.3f - fireRateCdr;
                        nextShootTime = Time.time + fireRate;
                    }
                    break;

                case GunType.auto:
                    shooting = true;
                    if (Time.time > nextShootTime)
                    {
                        Shoot();
                        float fireRate = 0.1F;
                        nextShootTime = Time.time + fireRate;
                    }
                    break;

                case GunType.burst:
                    shooting = true;
                    if (Time.time > nextShootTime && !bursting)
                    {
                        bursting = true;
                        StartCoroutine(BurstFire());
                        float fireRate = 0.5f - fireRateCdr;
                        nextShootTime = Time.time + fireRate;
                    }
                    break;

            }
        }
        switch (gunType)
        {
            case GunType.Rifle:
                if (Input.GetKey(KeyCode.P))
                {
                    
                    shooting= true;
                    if (Time.time > nextShootTime)
                    {
                        Shoot();
                        float fireRate = 0.3f - fireRateCdr;
                        nextShootTime = Time.time + fireRate;
                    }
                }
                break;

            case GunType.auto:
                if (Input.GetKey(KeyCode.P))
                {

                    shooting= true;
                    if (Time.time > nextShootTime)
                    {
                        Shoot();
                        float fireRate = 0.1F;
                        nextShootTime = Time.time + fireRate;
                    }

                }
                break;

            case GunType.burst:
                if (Input.GetKey(KeyCode.P))
                {
                    shooting= true;
                    if (Time.time > nextShootTime && !bursting)
                    {
                        bursting = true;
                        StartCoroutine(BurstFire());
                        float fireRate = 0.5f - fireRateCdr;
                        nextShootTime = Time.time + fireRate;
                    }

                }
                break;

        }
                    
        if (Input.GetKey(KeyCode.P))
        {
            shooting= false;
            playerAnimator.SetBool("Shooting", false);
        }

        
    }

    private void Shoot()
    {
        playerAnimator.SetBool("Shooting", true);
        eventManager.ShotFired();
        List<GameObject> goList = SetObjects();
        if (EventManager.lookDirr != "Right")
        {
            rigbody.AddForce(new Vector2(10f, 0f), ForceMode2D.Force);
            gunRenderer.flipX = false;
            Gun.transform.localPosition = new Vector2(-0.5f, 0.4f);
            muzzlePoint.localPosition = new Vector2(-1.8f, 0.25f);
            if (goList[0] != null && goList[1] != null)
            {

                SetShoot(goList[0], goList[1], goList[2], -1f, -90f,+0.3f);

            }

        }
        else
        {
            rigbody.AddForce(new Vector2(-10f, 0f),ForceMode2D.Force);
            gunRenderer.flipX = true;
            Gun.transform.localPosition = new Vector2(0.5f, 0.4f);
            muzzlePoint.localPosition = new Vector2(1.6f, 0.25f);
            if (goList[0] != null && goList[1] != null)
            {
                SetShoot(goList[0], goList[1], goList[2], +1f, +90f,-0.3f);

            }
        }
    }
    public void ShootButtonDonw()
    {
        Debug.Log("pressing");
        shooting = true;
        
    }
    public void ShootButtonUp()
    {
        shooting = false;
        playerAnimator.SetBool("Shooting", false);
    }
    public void Melee()
    {
        Sword.SetActive(true);
        playerAnimator.SetBool("MeleeAttack", true);
        if (EventManager.lookDirr != "Right")
        {
            Sword.transform.eulerAngles = new Vector3(0, 180, 0);
            Sword.transform.localPosition = new Vector3(-1.8f, 0.18f, 0);
        }
        else
        {
            Sword.transform.eulerAngles = new Vector3(0, 0, 0);
            Sword.transform.localPosition = new Vector3(1.8f, 0.18f, 0);
        }
    }
    private List<GameObject> SetObjects()
    {
        List<GameObject> list = new List<GameObject>();
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject("Bullet");
        GameObject mEffect = ObjectPool.SharedInstance.GetPooledObject("MEffect");
        GameObject shell = ObjectPool.SharedInstance.GetPooledObject("Shell");
        list.Add(bullet);
        list.Add(mEffect);
        list.Add(shell);
        return list;
    }

    private void SetShoot(GameObject bullet,GameObject mEffect,GameObject shell,float force,float angle,float shellLoc)
    {
        float randomRange = Random.Range(-0.6f, 0.6f);
        float randomRangeShell;
        if (force> 0f)
        {
            randomRangeShell = Random.Range(-1f, -2f);
        }
        else
        {
            randomRangeShell = Random.Range(1f, 2f);
        }
        float randomRangeShelltwo = Random.Range(1f, 2f);
        bullet.transform.position = new Vector2(muzzlePoint.position.x, muzzlePoint.position.y + randomRange);
        mEffect.transform.position = muzzlePoint.position;
        shell.transform.position = new Vector2(muzzlePoint.position.x + shellLoc, muzzlePoint.position.y+0.2f);
        bullet.GetComponent<BulletScript>().moveX = force;
        bullet.transform.GetChild(0).eulerAngles = new Vector3(0f, angle, 0);
        bullet.SetActive(true);
        mEffect.SetActive(true);
        mEffect.transform.eulerAngles = new Vector3(0f, angle, 0f);
        shell.SetActive(true);
        shell.GetComponent<Rigidbody2D>().AddForce(new Vector2(randomRangeShell, randomRangeShelltwo));
    }





    IEnumerator BurstFire()
    {
        yield return new WaitForSeconds(0.08f);
        Shoot();
        yield return new WaitForSeconds(0.08f);
        Shoot();
        yield return new WaitForSeconds(0.08f);
        Shoot();
        bursting = false;

    }
    public void ChangeGunType()
    {
        gunChoser++;
        if (gunChoser > 3)
        {
            gunChoser = 1;
        }
        switch (gunChoser)
        {
            case 1:
                gunType = GunType.Rifle;
                break;

            case 2:
                gunType = GunType.auto;
                break;

            case 3:
                gunType = GunType.burst;
                break;
        }

    }
    public void EndCombo()
    {
        Sword.SetActive(false);
        playerAnimator.SetBool("MeleeAttack", false);
        
    }
}
