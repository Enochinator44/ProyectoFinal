  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject  ProyectilActual, Player;
    public float speedEnem, timing,  timing2;
    public GameObject[] movEnem, ProyectilF;
    public NavMeshAgent agente;
    public int ciclo2, ciclo, exclusivo, fase;
    public float vProvisional, estado;

    Coroutine cProyectil1;
    public float[] min, max;

    private Animator EnemyAnimatorController;
    public int VelocidadL, VelocidadR;
    
    void Start()
    {
        EnemyAnimatorController = GetComponent<Animator>();
        EnemyAnimatorController.SetInteger("Cast", Random.Range(1, 3));
        fase = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
        if (fase == 1 && vProvisional <=0)
        {
            fase++;
            vProvisional = 1000;
        }
        timing2 += Time.deltaTime;
        if (timing2 >= 15)
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
                EnemyAnimatorController.SetTrigger("Dash");
                agente.SetDestination(movEnem[ciclo2].transform.position);
                EnemyAnimatorController.SetInteger("Cast", Random.Range(1, 3));
                transform.LookAt(Player.transform);

            }
            timing2 = 0;
        }
        if (fase == 1)
        {
            



            if (timing > 3)
            {
                exclusivo = Random.Range(1, 5);

                if (ciclo != exclusivo)
                {
                    
                    
                    ciclo = exclusivo;
                    transform.LookAt(Player.transform);
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
        }
        if (fase == 2)
        {




            if (timing > 3)
            {
                exclusivo = Random.Range(1, 7);

                if (ciclo != exclusivo)
                {
                    transform.LookAt(Player.transform);
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
                        case 6:
                            StartCoroutine("Zones");
                            break;
                        case 7:
                            StartCoroutine("Proyectil7");
                            break;
                    }
                }


                timing = 0;
            }
        }


        timing += Time.deltaTime;
       

       
    }
    private IEnumerator Proyectil1()
    {
        ProyectilActual = Instantiate(ProyectilF[0], transform.position, transform.localRotation);
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 1;
        
        yield return null;
    }
    private IEnumerator Proyectil2()
    {
        ProyectilActual = Instantiate(ProyectilF[0], transform.position, transform.localRotation);
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 2;
        ProyectilActual = Instantiate(ProyectilF[0], transform.position, transform.localRotation*Quaternion.Euler(new Vector3(0, 70,0)));
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 2;
        ProyectilActual = Instantiate(ProyectilF[0], transform.position, transform.localRotation * Quaternion.Euler(new Vector3(0, -70, 0)));
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 2;
        ProyectilActual = Instantiate(ProyectilF[0], transform.position, transform.localRotation * Quaternion.Euler(new Vector3(0, 35, 0)));
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 2;
        ProyectilActual = Instantiate(ProyectilF[0], transform.position, transform.localRotation * Quaternion.Euler(new Vector3(0, -35, 0)));
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 2;

        yield return null;
    }
    private IEnumerator Proyectil3()
    {
        for (float i = 0; i<=5; i++)
        {
            float angle = i * 72;
            Vector3 position = transform.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * 2;
            ProyectilActual = Instantiate(ProyectilF[0], position, Quaternion.identity);
            ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 3;
            ProyectilActual.gameObject.GetComponent<ProyectilF>().angle = i*72;
        }
        

        yield return null;
    }
    private IEnumerator Proyectil4()
    {
        ProyectilActual = Instantiate(ProyectilF[0], transform.position, transform.localRotation);
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 4;
        ProyectilActual = Instantiate(ProyectilF[0], transform.position, transform.localRotation * Quaternion.Euler(new Vector3(0, 70, 0)));
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 4;
        ProyectilActual = Instantiate(ProyectilF[0], transform.position, transform.localRotation * Quaternion.Euler(new Vector3(0, -70, 0)));
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 4;

        yield return null;
    }
    private IEnumerator Proyectil5()
    {
        ProyectilActual = Instantiate(ProyectilF[0], transform.position, transform.localRotation);
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 5;

        yield return null;
    }
    private IEnumerator Zones()
    {
        for(int i = 0; i <5; i++)
        {
            Vector3 pos = new Vector3(Random.Range(min[0], max[0]), -1, Random.Range(min[1], max[1]));
            ProyectilActual = Instantiate(ProyectilF[1], pos, transform.localRotation);
            yield return new WaitForSeconds(1);
        }
        
        yield return null;
    }
    private IEnumerator Proyectil7()
    {
        ProyectilActual = Instantiate(ProyectilF[0], transform.position, transform.localRotation);
        ProyectilActual.gameObject.GetComponent<ProyectilF>().tipo = 6;
        yield return null;
    }
}
