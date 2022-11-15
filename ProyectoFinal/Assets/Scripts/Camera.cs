using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target, jugador,enemigo;
    public float distJugadorAenemigo;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(target.transform);

        distJugadorAenemigo = Vector3.Magnitude(jugador.transform.position - enemigo.transform.position);
       
        transform.position = new Vector3(transform.position.x, 1 + (1f*distJugadorAenemigo), 1 + (-2f * distJugadorAenemigo));
      

    }
}
