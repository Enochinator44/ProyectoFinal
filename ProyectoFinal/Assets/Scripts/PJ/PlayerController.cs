using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
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
    public bool LlavePortal1; //Esta es para el templo y hacer tp al minijuego al que hay que poner texturas y eso 
    public bool LLavePortal2; //Esta es para  que se active el tp hacia la zona elevada que recuerdo que la teneis que arreglar 
    public bool llavePortal3; //esta es para que puedas activar el portal de arriba que finalizara el juego , teneis que buscarle un sitio en el que ponerla (preferiblemente abajo )

    
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


    public GameObject Filo;
    public Renderer rend;

    float velocidadmodificada;
    private bool trepar = false;

    private bool AtaqueMelee,
    AtaqueRango;

    //Variables Animacion

    public Animator playerAnimatorController;




    void Start()
    {
        player = GetComponent<CharacterController>();
        
        Cursor.lockState = CursorLockMode.Locked; //cursor del mouse no salga del juego. si en vez de usar .locked usas .none el cursor puede moverse perono tiene captura 
        rend = Filo.GetComponent<Renderer>();
        playerAnimatorController = GetComponent<Animator>();
        LlavePortal1 = false;
        LLavePortal2 = false;
        llavePortal3 = false;

    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        playerAnimatorController.SetFloat("PlayerWalkVelocity", playerInput.magnitude * playerSpeed);

        camDirection();

        movPlayer = playerInput.x * camRight + playerInput.z * camForward;
        movPlayer += movPlayer * playerSpeed * velocidadmodificada;



        player.transform.LookAt(player.transform.position + movPlayer);
        //Quaternion toRotation = Quaternion.FromToRotation(camForward, player.transform.position + movPlayer);
        //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1);

        SetGravity();

        PlayerSkills();

        player.Move(movPlayer * Time.unscaledDeltaTime);






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
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movPlayer.y = fallVelocity;
            playerAnimatorController.SetTrigger("PlayerJump");
          
        }
        if (player.isGrounded && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Filo.SetActive(true);
            AtaqueMelee = true;
            AtaqueRango = false;
        }
        if (player.isGrounded && Input.GetKeyDown(KeyCode.Alpha2))
        {
            Filo.SetActive(false);
            AtaqueMelee = false;
            AtaqueRango = true;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run = true;
            velocidadmodificada =3f;
            playerAnimatorController.SetBool("PlayerRun",Run);
          
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

        playerAnimatorController.SetBool("IsGrounded", player.isGrounded);

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



    private void OnControllerColliderHit(ControllerColliderHit hit) //detecta cuando el xharacter controller colisiona con otro objeto pero no al contrario 
    {
        hitNormal = hit.normal;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ventana")
        {
            trepar = true;
        }
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

    



}
