using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

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

    private bool withMe;

    private PlayerInput playerInput;
    private InputAction moveAction;

    [SerializeField]
    private Animator jokerAnim;

    private Rigidbody myRB;
    [SerializeField]
    private float dashForce = 10;

    public float angle;

    float dashTime;
    [SerializeField]
    private float dashCooldown=2;

    public int MyPoints;

    [SerializeField]
    private TMP_Text pointText;

    [SerializeField]
    private GameObject myPointsContainer;

    private void Awake()
    {
        MyPoints = 0;
        dashTime = 0;
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        myRB=GetComponent<Rigidbody>();
        withMe = true;
        myWeapon.transform.SetParent(handPlace);
        myWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        myWeapon.transform.localPosition = Vector3.zero;

        GameManager.GameManagerInstance.SetNewPlayer(myPointsContainer);

    }

    private void Update()
    {
        if(dashTime>0)
        {
            dashTime -= Time.deltaTime;
        }
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

    public void BasicAttack(InputAction.CallbackContext inputContext)
    {
        if (inputContext.performed && myWeapon != null)
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

        if (inputContext.performed&&myWeapon!=null)
        {
            //Debug.Log("shoot!!!");
            jokerAnim.SetTrigger("AttackTrigger");

            if (withMe == false)
                return;

            myWeapon.transform.SetParent(null);
            myWeapon.Throw(throwForce, directionObject.forward);
            withMe = false;
        }
    }

    public void Dash(InputAction.CallbackContext inputContext)
    {
        if (inputContext.performed&& dashTime<=0)
        {
            dashTime = dashCooldown;
            myRB.velocity = directionObject.forward * dashForce * Time.deltaTime;
        }
    }

    public void LetHen()
    {
        myWeapon = null;
    }

    public void PickUp()
    {
        if (myWeapon != null)
        {
            myWeapon.PickUp();
            myWeapon.transform.SetParent(handPlace);
            myWeapon.transform.localRotation = Quaternion.identity;
            myWeapon.transform.localEulerAngles = Vector3.zero;
            myWeapon.transform.localPosition = Vector3.zero;
            withMe = true;
        }
    }

    public void UpdatePoints()
    {
        MyPoints += 1;
        pointText.text = MyPoints.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerWeapon plWaepon = other.GetComponent<PlayerWeapon>();
        if (plWaepon != myWeapon && plWaepon.CanHurt())
        {
            //Debug.Log("aaaaaa!!!");
            plWaepon.MyOwner.UpdatePoints();
            plWaepon.HitSome();
        }

        if(plWaepon.IsSingle()&&myWeapon==null)
        {
            plWaepon.SetOwner(this);
            myWeapon = plWaepon;
            PickUp();
        }
    }

}
