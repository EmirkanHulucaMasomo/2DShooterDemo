using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

public class EventManager : MonoBehaviour
{
    public string CharId;
    public string lookDir;

    public static string lookDirr;
    

    public delegate void OnCollision(int damage,string id);
    public static event OnCollision onCol;


    public delegate void OnShotFired();
    public static event OnShotFired onShotFired;

    public delegate void SetCamDir(string lookDir);
    public static event SetCamDir onCamDir;

    public delegate void OnDashEnd(BoxCollider2D pCollid);
    public static event OnDashEnd onDashEnd;
    private void Update()
    {
        
        
    }
    public void SetDir(string dir)
    {
        this.lookDir = dir;
        lookDirr = dir;
        if(onCamDir!= null)
        {
            onCamDir(lookDir);
        }
    }

    public void ShotFired()
    {
        onShotFired();
    }

    public void SetId(string id,int damage)
    {
        this.CharId = id;
        if (onCol != null)
        {
            onCol(damage,id);
        }
    }
    
    public void ResetColliders(BoxCollider2D pCollider)
    {
        onDashEnd(pCollider);
    }

}
