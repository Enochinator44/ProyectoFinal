using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilF : MonoBehaviour
{
    // Start is called before the first frame update
    public float[] speed;
    public Animator anim;
    bool inicio = false;
    public GameObject gm;
    
    Rigidbody rb;
    

    public int tipo;
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        gm = GameObject.Find("gamemanager");
    }

    // Update is called once per frame
    void Update()
    {

        if (inicio == false) {
            inicio = true;
            switch (tipo)
            {
                case 1:

                    rb.AddForce(transform.forward * speed[0]);

                    break;
            }

        }
        if (Input.GetKeyDown(KeyCode.T))
        {


            rb.AddForce(-transform.forward* speed[tipo-1]/2);

        }
        if (Input.GetKeyUp(KeyCode.T))
        {


            rb.AddForce(transform.forward * speed[tipo-1]/2);

        }

        Debug.Log(gm.GetComponent<GameManager>().vSlowMotion);

        
        







    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("colision");
        if (collision.gameObject.tag == "Wall")
        {
            DestroyImmediate(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            DestroyImmediate(gameObject);

        }
    }
    public void ParryOk()
    {
        rb.AddForce(-transform.forward*speed[0]);
    }
    public void ParrayNoOk()
    {
        anim.SetBool("ParryOk", false);
    }
}
    
