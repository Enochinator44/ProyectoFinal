using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContT : MonoBehaviour
{
    private float horizontalMove;
    private float verticalMove;

    public Rigidbody playerRB;
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
    public GameObject parry,gamemanager;


  

    float velocidadmodificada;
    public bool ataque1, ataque2, ataque3/*,sigAtaque*/;
    public bool bCombo;
    public bool bWaitForCombo;
    public float TiempoCombo;
    public bool sigGolpe;
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
        bCombo = false;
        parry.SetActive(false);
        //sigAtaque = false;

    }

    // Update is called once per frame
    void Update()
    {
       

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        //playerRB.velocity = new Vector3(horizontalMove, 0, verticalMove) * playerSpeed;

        //transform.Translate(playerInput * playerSpeed*Time.unscaledDeltaTime, Space.World);
        
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
       
        if ( Input.GetKeyDown(KeyCode.Alpha1))
        {
           Debug.Log("shake");
            StartCoroutine(cameraShake.Shake(.15f , 4f));

        }

        //AtaquesBasicos();
        if (Input.GetKeyDown(KeyCode.Mouse0) && bCombo == false)
        {
            bCombo = true;
            StartCoroutine(Combos());

            //AtaqueCargado
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            parry.SetActive(true);
            StartCoroutine(TiempoParry());
        }

       
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
        if (Input.GetKeyDown(KeyCode.T) && gamemanager.GetComponent<GameManager>().vSlowMotion == 1)
        {
            gamemanager.GetComponent<GameManager>().SlowMotion();
        }
        if (Input.GetKeyUp(KeyCode.T)&&gamemanager.GetComponent<GameManager>().vSlowMotion!=1)
        {
            gamemanager.GetComponent<GameManager>().SlowMotionOff();
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
            Debug.Log("dashtime");
            yield return null;
        }

        dashtime = 0.3f;
        player.enabled = true;
    }
   
    public IEnumerator Combos()
    {
        Debug.Log("Empieza Corrutina Combos");
       
        playerAnimatorController.SetBool("Ataque3", false);
        playerAnimatorController.SetBool("Ataque1", true);
        Debug.Log("Primer ataque");

        
        bWaitForCombo = true;
        while (bWaitForCombo )
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                bWaitForCombo = false;
            }            
            yield return null;
        }


        bWaitForCombo = true;
        while (bWaitForCombo /*|| TiempoCombo < 1*/) 
        {
            
            TiempoCombo += Time.deltaTime;
            if (TiempoCombo > 2)
            {
                Debug.Log("saleAtaque2");
                CancelarCombo();
                yield break;
                
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("atque2");
                bWaitForCombo = false;
                bCombo = false;
                TiempoCombo = 0;
            }
            
           
           
            yield return null;
        }
        TiempoCombo = 0;
        playerAnimatorController.SetBool("Ataque1", false);
        playerAnimatorController.SetBool("Ataque2", true);
        Debug.Log("Segundo ataque");
        bWaitForCombo = true;
        while (bWaitForCombo /*|| TiempoCombo < 1*/)
        {
            TiempoCombo += Time.deltaTime;
            if (TiempoCombo > 2)
            {
                Debug.Log("saleAtaque3");
                CancelarCombo();
                yield break;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("atque3");
                bWaitForCombo = false;
                TiempoCombo = 0;
            }
            
           
            yield return null;
        }
        TiempoCombo = 0;
        playerAnimatorController.SetBool("Ataque2", false);
        playerAnimatorController.SetBool("Ataque3", true);
        Debug.Log("Tercer ataque");
        bCombo = false;

    }
   
    IEnumerator TiempoParry()
    {
        yield return new WaitForSeconds(1);
        parry.SetActive(false);
    }
    void CancelarCombo()
    {
        TiempoCombo = 0;
        bCombo = false;
        playerAnimatorController.SetBool("Ataque3", false);
        playerAnimatorController.SetBool("Ataque2", false);
        playerAnimatorController.SetBool("Ataque1", false);
    }


   



}
