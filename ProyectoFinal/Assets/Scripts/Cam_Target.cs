using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Target : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject jugador, enemigo;
    private Vector3 aim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (jugador.transform.position - enemigo.transform.position)/2;
    }
}
