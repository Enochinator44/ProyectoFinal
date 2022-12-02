using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContT : MonoBehaviour
{
    private float horizontalMove;
    private float verticalMove;

    public float playerSpeed;
    private Vector3 movPlayer;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;
    private bool Run;
    private bool left;
    private bool right;
    public float dashtime=0.5f;
    public float dashSpeed=5;
    public TimeManager timeManager;

    public Camera cameraShake;
   

    private bool muerto;



    private Vector3 playerInput;

    public CharacterController player;


    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    public bool isOnSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity;
    public float slopeForceDown;

    public bool conArma = true;
    private Vector3 posicionInicial;


  

    float velocidadmodificada;
    public bool ataque1, ataque2, ataque3;
   


    //Variables Animacion

    public Animator playerAnimatorController;




    void Start()
    {
        player = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        
        playerAnimatorController = GetComponent<Animator>();
        ataque1 = true;
        ataque2 = false;
        ataque3 = false;

    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

       

        playerAnimatorController.SetFloat("PlayerWalkVelocity", playerInput.magnitude * playerSpeed);

        //camDirection();

        movPlayer = playerInput.x * transform.right + playerInput.z * transform.forward;
        movPlayer += movPlayer * playerSpeed * velocidadmodificada;



        //player.transform.LookAt(player.transform.position + movPlayer);
        //Quaternion toRotation = Quaternion.FromToRotation(camForward, player.transform.position + movPlayer);
        //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1);

        SetGravity();

        PlayerSkills();

        //player.Move(movPlayer * Time.unscaledDeltaTime);
        player.SimpleMove(playerInput * playerSpeed);
        
        

       

        





    }

    void camDirection() //funcion para determinar la direccion a la que mira la camara 
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    //Funcion para las habilidades de nuestro personaje 

    public void PlayerSkills()
    {
        //if (player.isGrounded && Input.GetButtonDown("Jump"))
        //{
        //    fallVelocity = jumpForce;
        //    movPlayer.y = fallVelocity;
        //    playerAnimatorController.SetTrigger("PlayerJump");

        //}
        //if (/*player.isGrounded &&*/ Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Debug.Log("shake");
        //    StartCoroutine(cameraShake.Shake(.15f , 4f));

        //}
        //if (player.isGrounded && Input.GetKeyDown(KeyCode.Alpha2))
        //{

        AtaquesBasicos(); 
        //}
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run = true;
            velocidadmodificada = 3f;
            playerAnimatorController.SetBool("PlayerRun", Run); 

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DashCoroutine());
        }
        else
        {
            Run = false;
            playerAnimatorController.SetBool("PlayerRun", Run);
            velocidadmodificada = 1;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            velocidadmodificada = 0.5f;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            velocidadmodificada = 1;
        }
        if (Input.GetKey(KeyCode.T) && Time.timeScale==1)
        {
            timeManager.SlowMotion();
        }
        /*if(Input.GetKeyDown(KeyCode.Space) && trepar == true)
        {
            Animacion;
        }*/




    }


    void SetGravity() //funcion para la gravedad
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movPlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movPlayer.y = fallVelocity;
            playerAnimatorController.SetFloat("PlayerVerticalVelocity", player.velocity.y);
        }

        //playerAnimatorController.SetBool("IsGrounded", player.isGrounded);

        SlideDown();
    }

    public void SlideDown() //compara si esta o no en una rampa y aplica las fuerzas necesarias para deslizar 
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;

        if (isOnSlope)
        {
            movPlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
            movPlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;

            movPlayer.y += slopeForceDown;
        }
    }
   
    void AtaquesBasicos()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&& ataque1==true && ataque2 ==false &&ataque3 ==false)
        {
            Debug.Log("ATAQUE1");
            ataque1 = false;
            StartCoroutine(Ataque1_TiempoAnimacion());
        }
        if (ataque1==false&&ataque2==true&& ataque3 ==false &&Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("ATAQUE2");
            ataque2 = false;
            StartCoroutine(Ataque2_TiempoAnimacion2());
            
            ataque1 = false;
            
        }
        if (ataque1==false &&ataque2==false && ataque3==true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("ATAQUE3");
            ataque3 = false;
            StartCoroutine(Ataque3_TiempoAnimacion3());
            
        }


    }

    

    private void OnControllerColliderHit(ControllerColliderHit hit) //detecta cuando el xharacter controller colisiona con otro objeto pero no al contrario 
    {
        hitNormal = hit.normal;
    }
    private void OnTriggerStay(Collider other)
    {
     
    }

    private void OnAnimatorMove()
    {

    }


    IEnumerator ResetearEscena()
    {
        yield return new WaitForSeconds(5);
        {
            //SceneManager.LoadScene(0);
            muerto = false;
        }
    }

   

    private IEnumerator DashCoroutine()
    {

        player.enabled = false;

        while (dashtime > 0)
        {
            Vector3  v = new Vector3(horizontalMove, 0, verticalMove);
            transform.Translate( v.normalized* (dashSpeed * 2) * Time.unscaledDeltaTime, Space.World);
            dashtime -= Time.unscaledDeltaTime;
            
            yield return null;
        }

        dashtime = 0.3f;
        player.enabled = true;
    }
    IEnumerator Ataque1_TiempoAnimacion()
    {
        yield return new WaitForSeconds(1.5f);
        ataque2 = true;
        yield return new WaitForSeconds(2f);
        ataque2 = false;
        ataque1 = true;
    }
    IEnumerator Ataque2_TiempoAnimacion2()
    {
        yield return new WaitForSeconds(1.5f);
        ataque3 = true;
        
        yield return new WaitForSeconds(2f);
        ataque2 = false;
        ataque3 = false;
    }
    IEnumerator Ataque3_TiempoAnimacion3()
    {
        yield return new WaitForSeconds(1.5f);
        ataque1 = true;
    }

    // public void CancelarAtaques()
    //{
    //    float tiempoAcumulado = 0;
    //    float tiempoMaximo = tiempoAcumulado += Time.deltaTime;
    //    if (tiempoMaximo >= 3)
    //    {
    //        ataque1 = true;
    //        ataque2 = false;
    //        ataque3 = false;
    //    }
    //    yield return new WaitForSeconds(0);
    //}
   



}
