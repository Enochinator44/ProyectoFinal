using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cHolder : MonoBehaviour
{

    public Transform camPos;
   
    void Update()
    {
        transform.position = camPos.position;
    }
}
