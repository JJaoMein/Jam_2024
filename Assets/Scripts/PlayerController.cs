using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //[SerializeField]
    //private GameObject r_weaponPrefab;
    [SerializeField]
    private Transform handPlace;

    [SerializeField]
    private PlayerWeapon myWeapon;

    [SerializeField]
    private float throwForce=300f;

    [SerializeField]
    private Transform directionObject;

    private float distansWeapon;
    private bool withMe;

    private void Awake()
    {
        
        withMe = true;
        //r_myWeapon = Instantiate(r_weaponPrefab, r_handPlace.transform.position, Quaternion.identity).GetComponent<PlayerWeapon>();
        myWeapon.transform.SetParent(handPlace);
        myWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        myWeapon.transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.LeftControl)&&withMe==true)
        //{
        //    Throw();
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    PickUp();
        //}
    }

    public void Aim()
    {

    }

    public void Throw(InputAction.CallbackContext inputContext)
    {

        if (inputContext.started)
        {
            if (withMe == false)
                return;

            myWeapon.transform.SetParent(null);
            myWeapon.Throw(throwForce, directionObject.forward);
            withMe = false;
        }
    }

    public void PickUp(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started)
        {
            Debug.Log(inputContext.phase);
            myWeapon.PickUp();
            myWeapon.transform.SetParent(handPlace);
            myWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
            myWeapon.transform.localPosition = Vector3.zero;
            withMe = true;
        }
    }

}
