using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pChild : MonoBehaviour
{
    public GameObject player;
    
    

    public void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")){
            player.transform.parent = transform;
            
        }
        
        //if (transform.tag=="DestroyPlatform")
        //{
        //    StartCoroutine( plt.DestroyPlatform());
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.parent = null;

        }
    }
}
