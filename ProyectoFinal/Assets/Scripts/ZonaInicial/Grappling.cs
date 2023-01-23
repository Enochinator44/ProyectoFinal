using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grappling : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Referencias")]
    private Controllador2 pm;
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]

    public float maxGrappleDistance;
    public float grappleDelayedTime;
    public float oveshootYAxis;

    private Vector3 grapplePoint;

    [Header("CDs")]
    public float grapplingCd;
    private float grapplingCdTimmer;

    [Header("Inputs")]

    public KeyCode grappleKey = KeyCode.Mouse0;
    public bool grappling;

    [Header("UI")]
    public Image mira;
    public Image miraInterior;
    public GameObject prueba;

    private void Start()
    {
        pm = GetComponent<Controllador2>();
        prueba.SetActive(false);
        
    }

    private void Update()
    {
        CheckGrapple();

        if (Input.GetKeyDown(grappleKey))
        {
            StartGrapple(); 
        }
        if (grapplingCdTimmer>0)
        {
            grapplingCdTimmer -= Time.deltaTime;
        }

    }

    private void LateUpdate()
    {
        if (grappling)
        {
            lr.SetPosition(0, gunTip.position);
        }
    }

    private void StartGrapple()
    {
        if (grapplingCdTimmer>0)
        {
            return;
        }

        grappling = true;

        //pm.freeze = true;
       
        RaycastHit hit;

        if (Physics.Raycast(cam.position,cam.forward,out hit, maxGrappleDistance,whatIsGrappleable)) //mirar a ver si necesito un punto en el medio de la pantalla desde el que lanzar el grapple
        {
            grapplePoint = hit.point;

           
            
            

            Invoke(nameof(ExecuteGrapple), grappleDelayedTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayedTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {
        pm.state = Controllador2.MovementState.grappling;
        pm.jumpDown=0;
        prueba.SetActive(false);


        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + oveshootYAxis;

        if (grapplePointRelativeYPos<0)
        {
            highestPointOnArc = oveshootYAxis;
        }

        pm.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple),1f );
    }

    public void StopGrapple()
    {
        prueba.SetActive(false);
        pm.jumpDown = 3;
        pm.state = Controllador2.MovementState.wallrunning;
        grapplingCdTimmer = grapplingCd;
        lr.enabled = false;
    }

    private void CheckGrapple()
    {
        
        Physics.Raycast(cam.transform.position, cam.transform.forward, 25f, whatIsGrappleable);

        
        //prueba.SetActive(true);
      
    }


}
