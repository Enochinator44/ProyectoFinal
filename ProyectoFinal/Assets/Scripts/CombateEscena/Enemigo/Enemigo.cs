  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ProyectilF, ProyectilActual, Player;
    public float speedEnem, timing,  timing2;
    public GameObject[] movEnem;
    public NavMeshAgent agente;
    public int ciclo2, ciclo, exclusivo;
    public float vProvisional, estado;
    Coroutine cProyectil1;

    
    public int VelocidadL, VelocidadR;
    
    void Start()
    {
        vProvisional = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform);

        timing2 += Time.deltaTime;

        if (timing2 >=45)
        {
            exclusivo = Random.Range(0, 5);
            while (exclusivo == ciclo2)
            {
                exclusivo = Random.Range(0, 5);
            }
            
            if (ciclo2 != exclusivo)
            {
                ciclo2 = exclusivo;
                estado = ciclo2;
                agente.SetDestination(movEnem[ciclo2].transform.position);

            }
            timing2 = 0;
        }
       

        
        if (timing > 5)
        {
            exclusivo = Random.Range(1,5);
            if (ciclo != exclusivo)
            {
                ciclo = exclusivo;
                switch (ciclo)
                {
                    case 1:
                        StartCoroutine("Proyectil1");
                        break;
                    case 2:
                        StartCoroutine("Proyectil2");
                        break;
                    case 3:
                        StartCoroutine("Proyectil3");
                        break;
                    case 4:
                        StartCoroutine("Proyectil4");
                        break;
                    case 5:
                        StartCoroutine("Proyectil5");
                        break;

                }
            }
            
            
            timing = 0;
        }

        timing += Time.deltaTime;
       

       
    }
    private IEnumerator Proyectil1()
    {
        ProyectilActual = Instantiate(ProyectilF, transform.position, transform.localRotation);
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 1;
        
        yield return null;
    }
    private IEnumerator Proyectil2()
    {
        ProyectilActual = Instantiate(ProyectilF, transform.position, transform.localRotation);
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 2;

        yield return null;
    }
    private IEnumerator Proyectil3()
    {
        ProyectilActual = Instantiate(ProyectilF, transform.position, transform.localRotation);
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 3;

        yield return null;
    }
    private IEnumerator Proyectil4()
    {
        ProyectilActual = Instantiate(ProyectilF, transform.position, transform.localRotation);
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 4;

        yield return null;
    }
    private IEnumerator Proyectil5()
    {
        ProyectilActual = Instantiate(ProyectilF, transform.position, transform.localRotation);
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 5;

        yield return null;
    }
}
