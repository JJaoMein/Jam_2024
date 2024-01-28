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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 2, Color.green);
            transform.localEulerAngles += new Vector3(0, GetRandomRotation(), 0);
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 2, Color.red);
            //Debug.Log("Did not Hit");
        }

        if(isRun)
        {
            Vector3 direction = transform.forward;
            //transform.position = Vector3.forward * henSpeed * Time.deltaTime;
            //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + henSpeed * Time.deltaTime);

            transform.localPosition += new Vector3(direction.x, 0, direction.z) * henSpeed * Time.deltaTime;

            //if (direction != Vector2.zero)
            //{
            //    float angle = Mathf.Atan2(direction.y - Vector2.zero.y, direction.x - Vector2.zero.x);
            //    transform.rotation = Quaternion.Euler(0f, 90 - angle * Mathf.Rad2Deg, 0f);
            //}
        }
    }

    private float GetRandomRotation()
    {
        return Random.Range(-180, 180);
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
