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
    private float speed=5;

    [SerializeField]
    private Transform directionObject;

    private float distansWeapon;
    private bool withMe;

    private PlayerInput playerInput;
    private InputAction moveAction;

    [SerializeField]
    private Animator jokerAnim;

    public float angle;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");

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

        MovePlayer(); 
    }

    public void MovePlayer()
    {    
        Vector2 direction = moveAction.ReadValue<Vector2>();

        if (direction != Vector2.zero)
        {
            //move
            transform.position += new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;

            //aim
            //directionObject.transform.localEulerAngles = direction.normalized;

            jokerAnim.SetBool("Run", true);
            jokerAnim.SetBool("Idle", false);

            if (direction != Vector2.zero)
            {
                angle = Mathf.Atan2(direction.y - Vector2.zero.y, direction.x - Vector2.zero.x);
                directionObject.rotation = Quaternion.Euler(0f, 90 - angle * Mathf.Rad2Deg, 0f);

                if(Mathf.Abs(angle) >1.5)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*1,transform.localScale.y, transform.localScale.z);
                }
                else if (Mathf.Abs(angle) < 1.5)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
                }
            }
        }
        else
        {
            jokerAnim.SetBool("Run", false);
            jokerAnim.SetBool("Idle", true);
        }
    }

    public void Aim(InputAction.CallbackContext inputContext)
    {

    }

    public void HoldAttack(InputAction.CallbackContext inputContext)
    {
        
    }

    public void BasicAttack(InputAction.CallbackContext inputContext)
    {
        if (inputContext.performed)
        {
            Debug.Log("basic!!!");
            jokerAnim.SetTrigger("AttackTrigger");
        }
    }

    public void ActivateAim(InputAction.CallbackContext inputContext)
    {
        if (inputContext.performed)
        {
            Debug.Log("Aim!!!");
        }
    }

    public void Throw(InputAction.CallbackContext inputContext)
    {

        

        if (inputContext.performed)
        {
            Debug.Log("shoot!!!");
            jokerAnim.SetTrigger("AttackTrigger");

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
            myWeapon.transform.localRotation = Quaternion.identity;
            myWeapon.transform.localEulerAngles = Vector3.zero;
            myWeapon.transform.localPosition = Vector3.zero;
            withMe = true;
        }
    }

}
