using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    private Rigidbody myRB;

    private HenBehaviour henBehaviour;
    public bool HenRunning;

    public PlayerController MyOwner;

    [SerializeField]
    private ParticleSystem featherTrail;

    [SerializeField]
    private ParticleSystem featherExplosion;

    private void Awake()
    {
        henBehaviour = GetComponent<HenBehaviour>();
        MyOwner = GetComponentInParent<PlayerController>();
        myRB = GetComponent<Rigidbody>();
        myRB.useGravity = false;
        myRB.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.CompareTag("Wall"))
        //{
        //    HitSome();
        //}
    }

    public void HitSome()
    {
        featherExplosion.Play();
    }


    private void Update()
    {
        if (GameManager.GameManagerInstance.IsGameOver || GameManager.GameManagerInstance.IsPause)
            return;

        if (Vector3.SqrMagnitude(myRB.velocity)<=0.5f&&HenRunning==false&&transform.parent==null)
        {
            HenRunning = true;
            henBehaviour.StartRun();
            featherTrail.Stop();
            //myRB.useGravity = false;
            //myRB.isKinematic = true;
            myRB.freezeRotation = true;
            transform.eulerAngles = new Vector3(0, transform.localRotation.y, 0);
            RemoveOwenr();
        }

    }

    public void Throw(float force,Vector3 direction)
    {

        if (GameManager.GameManagerInstance.IsGameOver || GameManager.GameManagerInstance.IsPause)
            return;

        henBehaviour.Throw();
        myRB.useGravity = true;
        myRB.isKinematic = false;
        myRB.velocity = direction * force * Time.deltaTime;
        featherTrail.IsAlive(false);
        featherTrail.Play();
    }

    public bool CanHurt()
    {
        return (HenRunning==false && myRB.useGravity && myRB.velocity.magnitude>1&&!IsSingle());
    }

    public void PickUp()
    {

        if (GameManager.GameManagerInstance.IsGameOver || GameManager.GameManagerInstance.IsPause)
            return;

        HenRunning = false;
        myRB.useGravity = false;
        myRB.isKinematic = true;
        henBehaviour.PickUp();
        myRB.freezeRotation = false;

    }

    public bool IsSingle()
    {
        return MyOwner == null;
    }

    public void SetOwner(PlayerController owner)
    {
        MyOwner = owner;
    }

    public void RemoveOwenr()
    {
        MyOwner.LetHen();
        MyOwner = null;
    }
}
