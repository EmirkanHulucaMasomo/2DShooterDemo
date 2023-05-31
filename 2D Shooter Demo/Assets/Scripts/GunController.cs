using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject Gun;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            playerAnimator.SetBool("Shooting", true);
            if (MovementController.lookingDir != "Right")
            {
                Gun.GetComponent<SpriteRenderer>().flipX= false;
                Gun.transform.localPosition = new Vector2(-0.5f, 0.4f);
                //Gun.transform.SetPositionAndRotation(new Vector3(0,0,0), new Quaternion());
            }
            else
            {
                Gun.GetComponent<SpriteRenderer>().flipX = true;
                Gun.transform.localPosition = new Vector2(0.5f, 0.4f);
            }
        }
    }
}
