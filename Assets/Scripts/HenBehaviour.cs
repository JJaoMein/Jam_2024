using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenBehaviour : MonoBehaviour
{

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

        if (GameManager.GameManagerInstance.IsGameOver)
            return;

        if (isRun)
        {
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 2, Color.green);
                transform.localEulerAngles += new Vector3(0, GetRandomRotation(), 0);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 2, Color.red);
            }
            Vector3 direction = transform.forward;
           
            transform.localPosition += new Vector3(direction.x, 0, direction.z) * henSpeed * Time.deltaTime;
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
        transform.eulerAngles = new Vector3(0, GetRandomRotation(), 0) ;
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
