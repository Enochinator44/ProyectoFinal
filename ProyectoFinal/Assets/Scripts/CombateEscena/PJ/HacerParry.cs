using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerParry : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ParryOk")
        {
            Debug.Log("def");
            player.GetComponent<Animator>().SetTrigger("Parry");
            other.GetComponent<Rigidbody>().velocity = -other.GetComponent<Rigidbody>().velocity*2;
        }

    }
}
