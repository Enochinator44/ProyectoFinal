using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player, cam, music;
    void Start()
    {
        StartCoroutine("IntroAnim");
        StartCoroutine("SE");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SE()
    {
        yield return new WaitForSeconds(1.5f);
        music.SetActive(true);
    }
    IEnumerator IntroAnim()
    {
        yield return new WaitForSeconds(7.35f);

        player.SetActive(true);
        cam.SetActive(true);
        Destroy(gameObject);
    }
}
