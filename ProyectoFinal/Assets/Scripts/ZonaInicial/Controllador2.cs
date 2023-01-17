using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllador2 : MonoBehaviour
{
    [Header("Movimiento")]
    public float movSpeed;
    public Transform orientacion;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airmultiplier;
    bool readyTojump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded,freeze,activeGrapple;

    float horizontalInput;
    float verticalInput;

    Vector3 movDireccion;
    Rigidbody rb;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyTojump= true;
    }

    private void Update()
    {
        //Ground check

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (freeze)
        {
            movSpeed = 0;
            rb.velocity = Vector3.zero;
        }
       

        Inputs();
        SpeedControl();

        //Drag
        if (grounded&& !activeGrapple)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Inputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey)&& readyTojump&& grounded)
        {
            Debug.Log("hola");
            readyTojump = false;
            Jump();
            Invoke(nameof(ResetJump),jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        if (activeGrapple)
        {
            return;
        }
        movDireccion = orientacion.forward * verticalInput + orientacion.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(movDireccion.normalized * movSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(movDireccion.normalized * movSpeed * 10 * airmultiplier, ForceMode.Force);
        }
        

    }

    private void SpeedControl()
    {
        if (activeGrapple)
        {
            return;
        }
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude>movSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        //resetear velocity importante
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyTojump = true;
    }

    private bool enableMovementOnNextTouch;

    public void JumpToPosition(Vector3 targetPosition,float trajectoryHeight)
    {
        activeGrapple = true;
        velocityToSet= CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity),0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        rb.velocity = velocityToSet;
        enableMovementOnNextTouch = true;
    }

    public  void ResetRestrictions()
    {
        activeGrapple = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            GetComponent<Grappling>().StopGrapple();
        }
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint,Vector3 endpoint,float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endpoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endpoint.x - startPoint.x, 0f, endpoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
        +Mathf.Sqrt(2*(displacementY-trajectoryHeight)/gravity));

        return velocityXZ + velocityY;
    }

   
       
}
