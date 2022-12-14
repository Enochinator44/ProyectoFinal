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
    public GameObject parry;


  

    float velocidadmodificada;
    public bool ataque1, ataque2, ataque3/*,sigAtaque*/;
    public bool bCombo;
    public bool bWaitForCombo;
    public float TiempoCombo;

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
            playerAnimatorController.SetBool("Ataque1", true);
            
          

            StartCoroutine(Ataque1_TiempoAnimacion());
            //CancelarAtaques();
        }
        if ( ataque1==false&&ataque2==true&& ataque3 ==false &&Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("ATAQUE2");
            
            playerAnimatorController.SetBool("Ataque2", true);
            
            
            StartCoroutine(Ataque2_TiempoAnimacion2());
            
            
        }
        if ( ataque1==false &&ataque2==false && ataque3==true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("ATAQUE3");
            playerAnimatorController.SetBool("Ataque3", true);
            
           
            StartCoroutine(Ataque3_TiempoAnimacion3());
            //CancelarAtaques();


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
        ataque1 = false;
        yield return new WaitForSeconds(0.1f);
        playerAnimatorController.SetBool("Ataque1", false);
        StartCoroutine(CancelarCombo());
        
        ataque2 = true;
      
    }
    IEnumerator Ataque2_TiempoAnimacion2()
    {
        StopCoroutine(CancelarCombo());
        ataque2 = false;
        yield return new WaitForSeconds(0.1f);
        playerAnimatorController.SetBool("Ataque2", false);
        ataque3 = true;
        StartCoroutine(CancelarCombo());
        
        
       
    }
    IEnumerator Ataque3_TiempoAnimacion3()
    {
        StopCoroutine(CancelarCombo());
        ataque3 = false;
        yield return new WaitForSeconds(0.1f);
        playerAnimatorController.SetBool("Ataque3", false);
  
        ataque1 = true;
        ataque2 = false;
        
        
    }
    public IEnumerator Combos()
    {
       
        playerAnimatorController.SetBool("Ataque3", false);
        playerAnimatorController.SetBool("Ataque1", true);
        Debug.Log("Primer ataque");


        bWaitForCombo = true;
        while (bWaitForCombo)
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
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                bWaitForCombo = false;
            }
           
            TiempoCombo += Time.deltaTime;
            yield return null;
        }
        TiempoCombo = 0;
        playerAnimatorController.SetBool("Ataque1", false);
        playerAnimatorController.SetBool("Ataque2", true);
        Debug.Log("Segundo ataque");
        bWaitForCombo = true;
        while (bWaitForCombo /*|| TiempoCombo < 1*/)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                bWaitForCombo = false;
            }
            
            TiempoCombo += Time.deltaTime;
            yield return null;
        }
        TiempoCombo = 0;
        playerAnimatorController.SetBool("Ataque2", false);
        playerAnimatorController.SetBool("Ataque3", true);
        Debug.Log("Tercer ataque");
        bCombo = false;

    }
    IEnumerator CancelarCombo()
    {
        yield return new WaitForSeconds(3);
        ataque1 = true;
        ataque2 = false;
        ataque3 = false;
        playerAnimatorController.SetBool("Ataque1", false);
        playerAnimatorController.SetBool("Ataque2", false);
        playerAnimatorController.SetBool("Ataque3", false);
    }
    IEnumerator TiempoParry()
    {
        yield return new WaitForSeconds(1);
        parry.SetActive(false);
    }

    




}
