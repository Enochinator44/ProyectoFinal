using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilF : MonoBehaviour
{
    // Start is called before the first frame update
    public float[] speed;
    public Animator anim;
    bool inicio, parried = false;
    Vector3 pos, axis;
    public GameObject gm, enemy, player;
    float count;
    Rigidbody rb;
    public int daño;


    public int tipo;
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        gm = GameObject.Find("gamemanager");
        enemy = GameObject.Find("Enemy");
        player = GameObject.Find("Player");
        axis = transform.right;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        
            
            switch (tipo)
            {
                case 1:
                    if (inicio == false)
                    {
                        inicio = true;
                        rb.AddForce(transform.forward * speed[tipo - 1]);
                    }
                    break;
                case 2:

                    if (inicio == false)
                    {
                        inicio = true;
                        rb.AddForce(transform.forward * speed[tipo - 1]);
                        transform.localScale += new Vector3(3, 3, 3);
                    }
                    break;
                case 3:
                    transform.RotateAround(enemy.transform.position, Vector3.up, speed[tipo-1]*Time.deltaTime*35);
                    transform.Translate(new Vector3(speed[tipo - 1] * Time.deltaTime, 0, speed[tipo - 1] * Time.deltaTime), Space.Self);
                    break;
                case 4:
                
                pos += -Vector3.forward * Time.deltaTime * speed[tipo-1];
                transform.position = pos + axis * Mathf.Sin(Time.time * 5f) * 10f;
                
                break;
                case 5:
                    if (inicio == false)
                    {
                        inicio = true;
                        rb.AddForce(transform.forward * speed[tipo - 1]);
                    }
                break;
            }

       
        if (Input.GetKeyDown(KeyCode.T))
        {


            rb.AddForce(-transform.forward* speed[tipo-1]/2);

        }
        if (Input.GetKeyUp(KeyCode.T))
        {


            rb.AddForce(transform.forward * speed[tipo-1]/2);

        }

        
        





        count += Time.deltaTime;
        if (count > 10)
        {
            Destroy(gameObject);
        }




    }

    
    private void OnTriggerEnter(Collider collision)
    {

        Debug.Log("colision");
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("colision2");
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Debug.Log("colision2");
            collision.GetComponent<VidaYdañoJugador>().RestarVida(daño);
            Destroy(gameObject);
            

        }else if (collision.gameObject.tag == "Enemy" && parried == true)
        {
            Debug.Log("enemy");
            collision.GetComponent<VidaYdañoJugador>().RestarVidaEnemigo(daño);
            Destroy(gameObject);
        } 
    }
    public void ParryOk()
    {
        
        rb.AddForce(-transform.forward*speed[tipo - 1]);
        
    }
    public void parryDone()
    {
        parried = true;
    }
    public void ParrayNoOk()
    {
        anim.SetBool("ParryOk", false);
    }
}

    
