using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    private Rigidbody myRB;

    private HenBehaviour henBehaviour;

    private void Awake()
    {
        myRB = GetComponent<Rigidbody>();
        myRB.useGravity = false;
        myRB.isKinematic = true;
    }

    private void Update()
    {
        if(Vector3.SqrMagnitude(myRB.velocity)<=0.1f)
        {

        }

    }

    public void Throw(float force,Vector3 direction)
    {
        myRB.useGravity = true;
        myRB.isKinematic = false;
        myRB.velocity = direction * force * Time.deltaTime;
    }

    public void PickUp()
    {
        myRB.useGravity = false;
        myRB.isKinematic = true;


    }
}
