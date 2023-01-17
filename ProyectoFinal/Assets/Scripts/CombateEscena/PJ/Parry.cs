using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="ParryOk")
        {
            other.GetComponent<ProyectilF>().parryDone();
            other.GetComponent<Animator>().SetBool("Parry", true);
            //if (Input.GetKeyDown(KeyCode.Mouse1))
            //{
            //    other.GetComponent<Rigidbody>().AddForce(other.transform.forward, ForceMode.Impulse);
            //}
            
            
            
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="ParryOk")
        {
            other.GetComponent<Animator>().SetBool("Parry", false);
        }
        
    }
}
