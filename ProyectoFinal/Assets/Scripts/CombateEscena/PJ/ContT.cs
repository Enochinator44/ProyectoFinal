using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContT : MonoBehaviour
{
    private float horizontalMove;
    private float verticalMove;

    public Rigidbody playerRB;
    public float playerSpeed, dashCool;
    private Vector3 movPlayer;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;
    private bool Run;
    private bool left;
    private bool right;
    public float dashtime=0.35f;
    public float dashSpeed=5;
    public TimeManager timeManager;
    public float rotationSpeed;
    public Camera cameraShake;
   

   

    private bool muerto;
    public float activeTime = 2f;
    private bool isTrailActive;


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
    public GameObject parry,gamemanager,escudo,enemigo;


  

    float velocidadmodificada;
    public bool ataque1, ataque2, ataque3/*,sigAtaque*/;
    public bool bCombo;
    public bool bWaitForCombo;
    public float TiempoCombo;
    public bool sigGolpe;
    public bool bEscudo;
    public bool bAtaqueCargado;
    public float cdEscudo;
    public float cdAtaqueCargado;
    public float tAtaqueCargado;
    public Image cooldownA;
    public Image cooldownE;
    Vector3 desiredMoveDirection;
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

        var forward = mainCamera.transform.forward;
        var right = mainCamera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        desiredMoveDirection = forward * verticalMove + right * horizontalMove;
        transform.Translate(desiredMoveDirection * playerSpeed * Time.deltaTime, Space.World);
        if (desiredMoveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(desiredMoveDirection,Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed + Time.deltaTime);
        }
        
        //playerInput = new Vector3(horizontalMove, 0, verticalMove);
        //playerInput = Vector3.ClampMagnitude(playerInput, 1);
        //transform.Translate(playerInput * playerSpeed * Time.unscaledDeltaTime);
        
        if (desiredMoveDirection.magnitude>0)
        {
            playerAnimatorController.SetBool("run", true);
            Debug.Log("Entra en el if");
        }
        else
        {
            playerAnimatorController.SetBool("run", false);
        }
        
        playerAnimatorController.SetFloat("PlayerWalkVelocity", playerInput.magnitude * playerSpeed);

        //camDirection();

        //movPlayer = playerInput.x * transform.right + playerInput.z * transform.forward;
        //movPlayer += movPlayer * playerSpeed * velocidadmodificada;



        //player.transform.LookAt(player.transform.position + movPlayer);
        //Quaternion toRotation = Quaternion.FromToRotation(camForward, player.transform.position + movPlayer);
        //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1);

        SetGravity();

        PlayerSkills();

        //player.Move(movPlayer * Time.unscaledDeltaTime);
        //player.SimpleMove(playerInput * playerSpeed);

        cdAtaqueCargado += Time.deltaTime;
        cdEscudo += Time.deltaTime;
        if (cdAtaqueCargado >= 5)
        {
            bAtaqueCargado = true;

        }
        else
        {
            
            //Debug.Log(cooldownA.fillAmount + "a");

            cooldownA.fillAmount += 1.0f / 5 * Time.unscaledDeltaTime;
        }
        if (cdEscudo >= 10)
        {
            bEscudo = true;

        }
        else
        {
            cooldownE.fillAmount += 1.0f / 10 * Time.unscaledDeltaTime;
        }

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
        
            AtaqueCargado();
        
        

        

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
        if (Input.GetKeyDown(KeyCode.LeftControl) && bEscudo == true)
        {
            bEscudo = false;
            cdEscudo = 0;
            cooldownE.fillAmount = 0;
            escudo.SetActive(true);
            StartCoroutine(Invulnerabilidad());

        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run = true;
            velocidadmodificada = 3f;
            playerAnimatorController.SetBool("PlayerRun", Run); 

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCool > 1)
            {
                //StartCoroutine(ActivateTrail(activeTime));
                StartCoroutine(DashCoroutine());
                RastroDash();
                dashCool = 0;
            }
            
        }
        else
        {
            Run = false;
            playerAnimatorController.SetBool("PlayerRun", Run);
            velocidadmodificada = 1;
        }
        dashCool += Time.unscaledDeltaTime;
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
        //playerAnimatorController.SetTrigger("Dash");
        playerAnimatorController.SetBool("bDash", true);

        while (dashtime > 0)
        {
            Vector3  v = new Vector3(horizontalMove, 0, verticalMove);
            transform.Translate( desiredMoveDirection* (dashSpeed * 2) * Time.unscaledDeltaTime, Space.World);
            dashtime -= Time.unscaledDeltaTime;
            Debug.Log("dashtime");
            yield return null;  
        }
        playerAnimatorController.SetBool("bDash", false);
        dashtime = 0.35f;
        player.enabled = true;
    }
   
    public IEnumerator Combos()
    {
        Debug.Log("Empieza Corrutina Combos");

        playerAnimatorController.SetBool("Ataque3", false);
        playerAnimatorController.SetBool("Ataque2", false);
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

            if (playerAnimatorController.GetCurrentAnimatorStateInfo(0).IsName("A1"))
            {
                playerAnimatorController.SetBool("Ataque1", false);
            }
            
           
           
            yield return null;
        }
        TiempoCombo = 0;
        playerAnimatorController.SetBool("Ataque1", false);
        playerAnimatorController.SetBool("Ataque2", true);
        //playerAnimatorController.SetBool("Ataque3", false);


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
        playerAnimatorController.SetBool("Ataque1", false);
        
        Debug.Log("Tercer ataque");
        
        bCombo = false;

    }
   
    IEnumerator TiempoParry()
    {
        yield return new WaitForSeconds(2);
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
    void AtaqueCargado()
    {

        if (Input.GetKey(KeyCode.F) && bAtaqueCargado == true)
        {



            cooldownA.fillAmount = 0;
            cdAtaqueCargado = 0;
            player.enabled = false;
            transform.LookAt(enemigo.transform.position);
            tAtaqueCargado += Time.deltaTime;
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * (30000 * tAtaqueCargado), Color.yellow, 0);
            Debug.DrawRay(transform.position, transform.forward * 2 * tAtaqueCargado, Color.yellow, 0);
            Debug.Log("Cargando Ataque");

        }
        else if (Input.GetKeyUp(KeyCode.F) && tAtaqueCargado >= 3f)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100 * tAtaqueCargado);

            Debug.Log("AtaqueCargado");
            

            bAtaqueCargado = false;
            player.enabled = true;

        }
        else if (Input.GetKeyUp(KeyCode.F) && tAtaqueCargado < 3f)
        {
            
            
            if (bAtaqueCargado == true)
            {
                tAtaqueCargado = 0;
                cooldownA.fillAmount = 0;
                bAtaqueCargado = false;
            }
            player.enabled = true;

            
        }
       
    }
    IEnumerator Invulnerabilidad()
    {
        //Cancelar El daño cuando el sistema de vida Este programado
        escudo.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        Debug.Log("Escudook");
        escudo.SetActive(false);
    }

    public void RastroDash()
    {
        StartCoroutine(DashRastroCo());
    }
     public IEnumerator DashRastroCo()
    {
        player.GetComponent<TrailRenderer>().enabled = true;
        yield return new WaitForSeconds(dashtime);
        player.GetComponent<TrailRenderer>().enabled = false;
    }

  






}
