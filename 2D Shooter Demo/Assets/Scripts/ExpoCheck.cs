using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpoCheck : MonoBehaviour
{
    private void OnEnable()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
