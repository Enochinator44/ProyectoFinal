  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ProyectilF, ProyectilActual, Player;
    public float speedEnem, timing;
    public GameObject[] movEnem;
    public NavMeshAgent agente;
    Coroutine cProyectil1;

    
    public int VelocidadL, VelocidadR;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform);
        for (int i = 0; i < movEnem.Length; i++)
        {
            agente.SetDestination(movEnem[i].transform.position);
           
        }

        if (timing < 5)
        {
            StartCoroutine("Proyectil1");
            timing = 0;
        }

        timing += Time.deltaTime;
       

       
    }
    private IEnumerator Proyectil1()
    {
        ProyectilActual = Instantiate(ProyectilF, transform);
        ProyectilActual.transform.Translate(Vector3.forward*VelocidadL*Time.deltaTime, Space.Self);

        yield return null;
    }
}
