using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cams : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cameraM, cam1, cam2, Fase, Player, Plat;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateAnim()
    {
        StartCoroutine("Anim");
    }
    IEnumerator Anim()
    {
        Fase.SetActive(true);
        Player.SetActive(false);
        Plat.SetActive(false);
        cameraM.SetActive(false);
        cam1.SetActive(true);
        yield return new WaitForSeconds(1.50f);
        cam1.SetActive(false);
        cam2.SetActive(true);
        yield return new WaitForSeconds(5.30f);
        SceneManager.LoadScene("Fight");
        
    }
}
