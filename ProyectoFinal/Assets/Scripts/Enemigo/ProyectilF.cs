using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilF : MonoBehaviour
{
    // Start is called before the first frame update
    public float[] speed;
    
    public int tipo;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        switch (tipo)
        {
            case 1:
                transform.Translate(Vector3.forward*speed[0]*Time.deltaTime, Space.Self);
                break;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("colision");
        if (collision.gameObject.tag == "Wall")
        {
            DestroyImmediate(gameObject);
        }else if (collision.gameObject.tag == "Player")
        {
            DestroyImmediate(gameObject);

        }
    }
}
