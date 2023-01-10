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
    float count;
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
                    rb.AddForce(transform.forward * speed[tipo-1]);
                    break;
                case 2:
                    rb.AddForce(transform.forward * speed[tipo - 1]);
                    transform.localScale += new Vector3(3,3,3);
                    break;
                case 3:
                    rb.AddForce(transform.forward * speed[tipo - 1]);
                    break;
                case 4:
                    rb.AddForce(transform.forward * speed[tipo - 1]);
                    break;
                case 5:
                    rb.AddForce(transform.forward * speed[tipo - 1]);
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
            Destroy(gameObject);

        }
    }
    public void ParryOk()
    {
        rb.AddForce(-transform.forward*speed[tipo - 1]);
    }
    public void ParrayNoOk()
    {
        anim.SetBool("ParryOk", false);
    }
}
    
