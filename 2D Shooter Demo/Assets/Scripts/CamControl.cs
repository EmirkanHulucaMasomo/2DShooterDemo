using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 1f;

    private void OnEnable()
    {
        EventManager.onCamDir += SetCamPos;
        EventManager.onShotFired += ShakeCall;
    }
    private void OnDisable()
    {
        EventManager.onCamDir -= SetCamPos;
        EventManager.onShotFired -= ShakeCall;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(Shake());
        }
    }


    public void ShakeCall()
    {
        StartCoroutine(Shake());
    }
    public IEnumerator Shake()
    {
        Vector3 StartPosition=transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength=curve.Evaluate(elapsedTime/duration);
            //transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, -10f) + Random.insideUnitSphere*strength;
            transform.position = new Vector3(transform.position.x,transform.position.y,-10f)+ Random.insideUnitSphere * strength;
            yield return null;
        }
        //transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, -10f);
        
    }


    public void SetCamPos(string dir)
    {
        switch (dir)
        {
            case "Right":
                Vector3 initPos= new Vector3(transform.parent.position.x + 4.50f, transform.parent.position.y, -10f);
                transform.position = Vector3.Lerp(transform.position,initPos, Time.deltaTime*5);
                break;

            case "Left":
                Vector3 iniPos = new Vector3(transform.parent.position.x - 4.50f, transform.parent.position.y, -10f);
                transform.position = Vector3.Lerp(transform.position, iniPos, Time.deltaTime * 5);
                break;
        }
    }
}
