using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("WallRunning")]

    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float wallJumpUpForce;
    public float wallJumpSideForce;
    public float wallClimbSpeed;
    public float maxWallRunTime;
    private float wallRunTimer;

    [Header("Inputs")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode upwardsRunKey = KeyCode.LeftShift;
    public KeyCode downwardsRunKey = KeyCode.LeftControl;
    public bool upwardsRunning;
    public bool downwardsRunning;
    private float horizontalInput;
    private float verticalInput;

    [Header("Detecciones")]

    public float wallCheckDistance;
    public float miniJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    public bool wallLeft;
    public bool wallRight;

    [Header("ExitingWallrun")]

    public bool exitingWall;
    public float exitWallTime;
    public float exitWallTimer;
    [Header("Referencias")]

    public Transform orientation;
    private Controllador2 pm;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<Controllador2>();
    }

    private void Update()
    {
        CheckForWall();
        StateMachine();
        
        Debug.Log(wallLeft);
        Debug.Log(wallRight);
        

    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);

        if (wallRight==false&&wallLeft==false)
        {
            StopWallRun();
        }

        //if (!wallRight && !wallLeft&& )
        //{
        //    pm.wallrunning = false;
        //}
    }

    private void FixedUpdate()
    {
        Debug.Log(pm.state);
        if (pm.state == Controllador2.MovementState.wallrunning)
        {
            
            WallRunningMovement();
        }
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, miniJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        upwardsRunning = Input.GetKey(upwardsRunKey);
        downwardsRunning = Input.GetKey(downwardsRunKey);

       

        //Estado1- WallRunning

        if ((wallLeft||wallRight)&&verticalInput>0 && AboveGround() && !exitingWall)
        {
            //Empieza la movida


            
            if (pm.state != Controllador2.MovementState.wallrunning)
            {

                
                Debug.Log("empieza el wallrun");
                StartWallRun();
                //if (wallRunTimer>0)
                //{
                //    wallRunTimer -= Time.deltaTime;
                //}
                //if (wallRunTimer<=0 && pm.wallrunning == true)
                //{
                //    exitingWall = true;
                //    exitWallTimer = exitWallTime;
                //}

                
            }

            if (Input.GetKeyDown(jumpKey) && pm.state == Controllador2.MovementState.wallrunning)
            {
                WallJump();
                Debug.Log("Jump");
            }

            //Estado2 - Exiting
            if (exitingWall)
            {
                if (pm.state == Controllador2.MovementState.wallrunning)
                {
                    StopWallRun();
                    Debug.Log("Exit " + exitingWall);
                    StartCoroutine(AfterJump());
                }
                if (exitWallTimer>0)
                {
                    exitWallTimer -= Time.deltaTime;
                }
                if (exitWallTimer<=0)
                {
                    exitingWall = false;
                }
            }
            //else
            //{
            //    if (pm.wallrunning)
            //    {
            //        StopWallRun();
            //    }
            //}

        }
    }

    private void StartWallRun()
    {
        pm.state = Controllador2.MovementState.wallrunning;

        //wallRunTimer = maxWallRunTime;
    }

    private void WallRunningMovement()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward-wallForward).magnitude>(orientation.forward- -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
        {
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }

        if (upwardsRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
        }
        if (downwardsRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);
        }

    }

    private void StopWallRun()
    {
        pm.state = Controllador2.MovementState.air;
    }

    private void WallJump()
    {
        //StopWallRun();
        //exitWallTimer = exitWallTime;
        Vector3 wallnormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 forceToApply = transform.up * wallJumpUpForce + wallnormal * wallJumpSideForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

        exitingWall = true;
        
    }
    private IEnumerator AfterJump()
    {
        yield return new WaitForSeconds(0.5f);
        exitingWall = false;
    }


}