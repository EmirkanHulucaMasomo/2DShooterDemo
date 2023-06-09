using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledBullets;
    public List<GameObject> pooledMEffects;
    public List<GameObject> pooledShells;
    public List<GameObject> pooledIEffects;
    public List<GameObject> pooledExplosions;
    public GameObject bulletPrefab;
    public GameObject muzzleEffectPrefab;
    public GameObject shellPrefab;
    public GameObject impacteffectPrefab;
    public GameObject expoeffectPrefab;
    public int amountToPool;
    public GameObject bPoolObject;
    public GameObject mPoolObject;
    public GameObject sPoolObject;
    public GameObject iPoolObject;
    public GameObject ePoolObject;
    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        SetPools();
    }

    private void Update()
    {
        
    }
    private void SetPools()
    {
        pooledBullets = new List<GameObject>();
        pooledMEffects = new List<GameObject>();
        pooledShells = new List<GameObject>();
        pooledIEffects=new List<GameObject>();
        pooledExplosions= new List<GameObject>();
        GameObject tmp;
        GameObject emp;
        GameObject smp;
        GameObject imp;
        GameObject eemp;
        for (int i = 0; i < amountToPool; i++)
        {
            emp = Instantiate(muzzleEffectPrefab);
            tmp = Instantiate(bulletPrefab);
            smp = Instantiate(shellPrefab);
            imp= Instantiate(impacteffectPrefab);
            eemp= Instantiate(expoeffectPrefab);
            tmp.SetActive(false);
            emp.SetActive(false);
            smp.SetActive(false);
            imp.SetActive(false);
            eemp.SetActive(false);
            pooledBullets.Add(tmp);
            pooledMEffects.Add(emp);
            pooledShells.Add(smp);
            pooledIEffects.Add(imp);
            pooledExplosions.Add(eemp);
            tmp.transform.parent = bPoolObject.transform;
            emp.transform.parent = mPoolObject.transform;
            smp.transform.parent = sPoolObject.transform;
            imp.transform.parent = iPoolObject.transform;
            eemp.transform.parent = ePoolObject.transform;
        }
    }

    public GameObject GetPooledObject(string objectName)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            switch(objectName)
            {
                case "Bullet":
                    if (!pooledBullets[i].activeInHierarchy)
                    {
                        return pooledBullets[i];
                    }
                    break;
                case "MEffect":
                    if (!pooledMEffects[i].activeInHierarchy)
                    {
                        return pooledMEffects[i];
                    }
                    break;
                case "Shell":
                    if (!pooledShells[i].activeInHierarchy)
                    {
                        return pooledShells[i];
                    }
                    break;
                case "IEffect":
                    if (!pooledIEffects[i].activeInHierarchy)
                    {
                        return pooledIEffects[i];
                    }
                    break;
                case "Explosion":
                    if (!pooledExplosions[i].activeInHierarchy)
                    {
                        return pooledExplosions[i];
                    }
                    break;

            }
        }

        if (2 == 2)
        {
            switch (objectName)
            {
                case "Shell":
                    GameObject obj = (GameObject)Instantiate(shellPrefab);
                    pooledShells.Add(obj);
                    obj.transform.parent = sPoolObject.transform;
                    return obj;

                case "Explosion":
                    GameObject emp= (GameObject)Instantiate(expoeffectPrefab);
                    pooledExplosions.Add(emp);
                    emp.transform.parent = ePoolObject.transform;
                    return emp;

                   
            }
            
        }
        return null;
    }
}
