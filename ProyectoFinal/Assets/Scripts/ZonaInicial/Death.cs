using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject player;
    public GameObject Inicio;

    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = Inicio.transform.position;
    }

}
