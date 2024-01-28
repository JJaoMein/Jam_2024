using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenBehaviour : MonoBehaviour
{
    private bool isHit;

    private bool isRun;
    [SerializeField]
    private float henSpeed=10f;

    [SerializeField]
    private Animator henAnim;

    private void Awake()
    {
        isRun = false;
    }
    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1, Color.green);
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1, Color.red);
            //Debug.Log("Did not Hit");
        }

        if(isRun)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + henSpeed * Time.deltaTime);
        }
    }

    public void Throw()
    {
        //henAnim.SetTrigger("Throw");
        henAnim.SetBool("IsFlying", true);
    }

    public void StartRun()
    {
        isRun = true;
        henAnim.SetBool("IsRun", true);
        henAnim.SetBool("IsFlying", false);
    }

    public void PickUp()
    {
        isRun = false;
        henAnim.SetBool("IsRun", false);
        henAnim.SetBool("IsFlying", false);


    }
}
