using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pChild : MonoBehaviour
{
    public GameObject player;
    private  Platform plt;
    public GameObject movPlat;

    public void Start()
    {
        plt = movPlat.GetComponent<Platform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.transform.parent = transform;
        //if (transform.tag=="DestroyPlatform")
        //{
        //    StartCoroutine( plt.DestroyPlatform());
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        player.transform.parent = null;
    }
}
