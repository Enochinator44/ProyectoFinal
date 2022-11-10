using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ProyectilF, ProyectilActual, Player;
    
    public int VelocidadL, VelocidadR;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform);
    }
    private IEnumerator Proyectil1()
    {
        ProyectilActual = Instantiate(ProyectilF, transform);
        ProyectilActual.transform.Translate(Vector3.forward*VelocidadL, Space.Self);

        yield return null;
    }
}
