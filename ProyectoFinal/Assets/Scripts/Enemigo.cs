  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ProyectilF, ProyectilActual, Player;
    public float speedEnem;
    public GameObject[] movEnem;
    public NavMeshAgent agente;
    
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
       

       
    }
    private IEnumerator Proyectil1()
    {
        ProyectilActual = Instantiate(ProyectilF, transform);
        ProyectilActual.transform.Translate(Vector3.forward*VelocidadL, Space.Self);

        yield return null;
    }
}
