using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Reference")]

    
    public Rigidbody[] rbPlatform;
    public Transform[] platformPoints;
    public Transform[] platformPoints2;
    public Transform[] platformPoints3;
    public GameObject player;


    [Header("PlatformInfo")]

    public float pSpeed;
    public float pDestroy;
    private int pActualPos=0;
    private int pNextPos=1;
    public bool moveNext=true;
    public float waitTime;
    private void Update()
    {
        MovePlatform();
    }

    public void MovePlatform()
    {
        if (moveNext)
        {
            StopCoroutine(PlatformWaitTime());
            for (int i = 0; i < rbPlatform.Length; i++)
            {
                rbPlatform[i].MovePosition(Vector3.MoveTowards(rbPlatform[i].transform.position, platformPoints[pNextPos].position, pSpeed * Time.deltaTime));
            }
          
            

           
        }
       
        if (Vector3.Distance(rbPlatform[0].position,platformPoints[pNextPos].position)<=0)
        {
            StartCoroutine(PlatformWaitTime());
            pActualPos = pNextPos;
            pNextPos++;
            if (pNextPos>platformPoints.Length-1)
            {
                pNextPos = 0;
            }
        }
        

    }
    public void StopMove()
    {

    }
    public IEnumerator DestroyPlatform()
    {
        Debug.Log("DestroyPlatfomr");
        player.transform.parent = null;
        yield return new WaitForSeconds(pDestroy);
        Destroy(this.gameObject);
    }
    public IEnumerator PlatformWaitTime()
    {
        moveNext =false;
        yield return new WaitForSeconds(waitTime);
        moveNext = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (transform.tag == "DestroyPlatform")
        {
            StartCoroutine(DestroyPlatform());
        }
    }



}
