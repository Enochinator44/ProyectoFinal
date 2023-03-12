using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player, cam;
    void Start()
    {
        StartCoroutine("IntroAnim");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator IntroAnim()
    {
        yield return new WaitForSeconds(7.35f);

        player.SetActive(true);
        cam.SetActive(true);
        Destroy(gameObject);
    }
}
