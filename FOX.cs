using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOX : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] Collider2D standingCollider;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] Transform overheadCheckCollider;
    [SerializeField] LayerMask groundLayer;

    public float speed = 1;
    public float jumpPower = 150;
    public float flashPower = 100;
    [SerializeField] bool crouch = false;
    [SerializeField] bool grounded;
    [SerializeField] bool flash = false;

    const float groundCheckRadius = 0.2f;
    const float overheadCheckRadius = 0.2f;
    float horizontalValue;
    float crouchSpeedModifier = 0.5f;
    float runSpeedModifier = 1.8f;
    bool facingRight = true;
    bool jump;
    bool running = false;
    bool DEAD = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jumping", true);
        }
        else if (Input.GetButtonUp("Jump"))
            jump = false;

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            // animator.SetBool("Crouching", true);
        }
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;


        if (Input.GetButtonDown("Flash"))
            flash = true;

        else if (Input.GetButtonUp("Flash"))
            flash = false;


        animator.SetFloat("yVelocity", rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            running = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
            running = false;

    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue, jump, crouch);
    }

    void GroundCheck()
    {
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
            grounded = true;

        animator.SetBool("Jumping", !grounded);

    }

    void Move(float dir, bool jumpFlag, bool crouchFlag)
    {
        #region RUN

        float xVal = dir * speed * 50 * Time.fixedDeltaTime;

        if (crouchFlag)
            xVal *= crouchSpeedModifier;

        if (running)
            xVal *= runSpeedModifier;


        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;




        if (flash == true && facingRight == true)
        {
            rb.AddForce(new Vector2(flashPower, 0f));
            flash = false;
        }
        if (flash == true && facingRight == false)
        {
            rb.AddForce(new Vector2(-flashPower, 0f));
            flash = false;
        }


        #region INTOARCERE

        //Daca e cu fata spre dreapta si apas stanga (se intoarce cu fata spre stanga)
        if (facingRight && dir < 0 )
        {
            transform.localScale = new Vector3(-2, 2, 1);
            facingRight = false;
        }
        //Daca e cu fata la stanga si apasa dreapta (se intoarce cu fata spre dreapta)
        else if (!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(2, 2, 1);
            facingRight = true;

           
        }
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        //Debug.Log(rb.velocity.x);

        #endregion

        // idle 0, run 2.5 

        #region fostul shift

        // float runningShift;
        // float xVal2 = dir * speed * 80 * Time.fixedDeltaTime;

        /*
        if (running)
        {
            runningShift = xVal2;
            speed = 3;
        }
        else
            runningShift = xVal;

        Vector2 targetrunningShift = new Vector2(runningShift, rb.velocity.y);
        rb.velocity = targetrunningShift;
        */

        #endregion

        #endregion

        #region  JUMP & CROUCH

        if (grounded)
        {
            standingCollider.enabled = !crouchFlag;

            if (jumpFlag)
            {
                jumpFlag = false;
                grounded = false;

                rb.AddForce(new Vector2(0f, jumpPower));
            }
        }

        //Daca se apasa crouch se va dezactiva primul collider (standing collider)

        if (grounded && !crouchFlag)
        {
            if (Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundLayer))
                crouchFlag = true;

        }

        if (crouchFlag)
        {
            running = false;
        }


        animator.SetBool("Crouching", crouchFlag);
        // standingCollider.enabled = !crouchFlag;
        //crouchingCollider.enabled = crouchFlag;


        #endregion 

    }

       #region DESTROY
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("GEM"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("STAR"))
        {
            
            Destroy(other.gameObject);
            jumpPower = 200;

        }

        if (other.gameObject.CompareTag("STAR"))
        {

            Destroy(other.gameObject);
            jumpPower = 200;

        }

        if (other.gameObject.CompareTag("OPOSSUM"))
        {
            DEAD = true;

            animator.SetBool("DEAD", DEAD);

            if(!DEAD)
            {
                Destroy(other.gameObject);
            }

        }

        if (other.gameObject.CompareTag("Ciuperca"))
        {
            
            jumpPower = 50;
        }


    }
         #endregion




}






