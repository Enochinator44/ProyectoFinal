using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingObject : MonoBehaviour
{
    private Renderer Renderer;
    public Camera cam;
    Vector3 screenPoint;
    void Start()
    {
        
        Renderer = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        screenPoint = cam.WorldToScreenPoint(transform.position);
        screenPoint.x = screenPoint.x / Screen.width;
        screenPoint.y = screenPoint.y / Screen.height;
        Renderer.material.SetVector("ObjScreenPos", screenPoint);
    }
}
