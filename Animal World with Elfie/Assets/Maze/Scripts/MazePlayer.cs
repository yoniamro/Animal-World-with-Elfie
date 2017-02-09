using UnityEngine;
using System.Collections;

public class MazePlayer : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float smoothSpeed = 2f;
    public float gravity = 20f;
    public float climbingSpeed = 50f;

    private Vector3 moveAmount;

    private Vector3 currentVelocity;
    private Rigidbody rb;
    private Animator anim;


    private bool climbingCliff = false;

    private CharacterController cc;



    public event System.Action OnCoinPickup;
    [HideInInspector]
    public bool gotBusted = false;
    [HideInInspector]
    public bool collectedCoins = false;


    private float slopeLimit;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

	void Update ()
    {

        Vector3 moveInput = new Vector3(ControlFreak2.CF2Input.GetAxis("Horizontal"), 0, ControlFreak2.CF2Input.GetAxis("Vertical"));
        moveAmount = moveInput.normalized * moveSpeed;

        //transform.position = Vector3.SmoothDamp(transform.position, transform.position + moveAmount, ref currentVelocity, smoothSpeed);
        //transform.rotation = Quaternion.LookRotation(_vel);


        if(!gotBusted && !collectedCoins)
        {
            
            if (cc.isGrounded && !climbingCliff)
            {
                moveAmount.y = 0;
            }
            

            if (climbingCliff)
            {
                moveAmount.y += climbingSpeed;
                //rb.useGravity = false;
            }
            else if (!climbingCliff)
            {
                moveAmount.y -= gravity;
            }

            cc.Move(moveAmount * Time.deltaTime);
            transform.LookAt(transform.position + new Vector3(moveAmount.x, 0, moveAmount.z), Vector3.up);

            if (anim != null)
            {
                float runMag = new Vector3(moveAmount.x, 0, moveAmount.z).magnitude;
                anim.SetFloat("Run", runMag);
            }
        }


        //else if (!cc.isGrounded)
        //{
        //    anim.SetBool("Grounded", false);
        //}
        //transform.Translate(moveAmount * Time.deltaTime, Space.World);


        
	}

    //void FixedUpdate()
    //{
    //    if (!gotBusted && !collectedCoins)
    //    {
    //        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    //    }
    //}

    void OnTriggerEnter(Collider c)
    {
        if(c.GetComponent<Collider>().CompareTag("Coin"))
        {
            if(OnCoinPickup != null)
            {
                OnCoinPickup();
            }

            Destroy(c.gameObject);
         
        }

        //if (c.GetComponent<Collider>().CompareTag("Cliff"))
        //{
        //    climbingCliff = true;
        //}
    }

    void OnTriggerStay(Collider c)
    {
        if (c.GetComponent<Collider>().CompareTag("Cliff"))
        {
            climbingCliff = true;
        }
    }

    void OnTriggerExit(Collider c)
    {

        if (c.GetComponent<Collider>().CompareTag("Cliff"))
        {
            climbingCliff = false;
        }
    }
}
