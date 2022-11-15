  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ProyectilF, ProyectilActual, Player , movEnem1 , movEnem2;
    public float speedEnem;
    
    
    public int VelocidadL, VelocidadR;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform);

        transform.Translate(movEnem1.transform.position * speedEnem *Time.deltaTime);
    }
    private IEnumerator Proyectil1()
    {
        ProyectilActual = Instantiate(ProyectilF, transform);
        ProyectilActual.transform.Translate(Vector3.forward*VelocidadL, Space.Self);

        yield return null;
    }
}
