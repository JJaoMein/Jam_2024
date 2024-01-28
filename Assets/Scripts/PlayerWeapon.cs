using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    private Rigidbody myRB;

    private HenBehaviour henBehaviour;
    private bool henRunning;

    private void Awake()
    {
        henBehaviour = GetComponent<HenBehaviour>();
        myRB = GetComponent<Rigidbody>();
        myRB.useGravity = false;
        myRB.isKinematic = true;
    }

    private void Update()
    {
        if(Vector3.SqrMagnitude(myRB.velocity)<=0.2f&&henRunning==false&&transform.parent==null)
        {
            henRunning = true;
            henBehaviour.StartRun();
            //myRB.useGravity = false;
            //myRB.isKinematic = true;
            myRB.freezeRotation = true;
            transform.localEulerAngles = new Vector3(0, transform.localRotation.y, 0);
        }

    }

    public void Throw(float force,Vector3 direction)
    {
        henBehaviour.Throw();
        myRB.useGravity = true;
        myRB.isKinematic = false;
        myRB.velocity = direction * force * Time.deltaTime;
    }

    public void PickUp()
    {
        henRunning = false;
        myRB.useGravity = false;
        myRB.isKinematic = true;
        henBehaviour.PickUp();
        myRB.freezeRotation = false;

    }
}
