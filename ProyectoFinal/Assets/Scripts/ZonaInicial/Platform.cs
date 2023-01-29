using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Reference")]

    public GameObject player;
    public Rigidbody[] rbPlatform;
    public Transform[] platformPoints;
    public Transform[] platformPoints2;
    public Transform[] platformPoints3;


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
            rbPlatform[0].MovePosition(Vector3.MoveTowards(rbPlatform[0].transform.position, platformPoints[pNextPos].position, pSpeed * Time.deltaTime));

            rbPlatform[1].MovePosition(Vector3.MoveTowards(rbPlatform[1].transform.position, platformPoints2[pNextPos].position, pSpeed * Time.deltaTime));

            rbPlatform[2].MovePosition(Vector3.MoveTowards(rbPlatform[2].transform.position, platformPoints3[pNextPos].position, pSpeed * Time.deltaTime));
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
        yield return new WaitForSeconds(pDestroy);
        Destroy(rbPlatform[2]);
    }
    public IEnumerator PlatformWaitTime()
    {
        moveNext =false;
        yield return new WaitForSeconds(waitTime);
        moveNext = true;
    }

    

}
