using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float vSlowMotion;

    //Srcripts

    public Enemigo gmEnemigo;
    public ProyectilF gmProyectilF;
    void Start()
    {
        
        vSlowMotion = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlowMotion()
    {
        vSlowMotion = 0.25f;
        gmEnemigo.agente.speed = gmEnemigo.agente.speed *= vSlowMotion;
        gmEnemigo.vProvisional = gmEnemigo.vProvisional *= vSlowMotion;
        //El proyectil se mueve al aplicarle una fuerza , tiene que tener una velocidad constante y multiplicar a esta por el vSlowMotion , hasta que eso no este hecho el proytectil no funcionara como debe 

    }
    public void SlowMotionOff()
    {
        //todas las velocidaes vulven a su numero original , hay que cambiarlas aqui a mano 
        vSlowMotion = 1;
        gmEnemigo.agente.speed = gmEnemigo.agente.speed=3.2f;
        gmEnemigo.vProvisional = gmEnemigo.vProvisional=1000f;
    }
}
