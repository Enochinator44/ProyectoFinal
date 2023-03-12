using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interfase : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camM, cam1, cam2, player, plat, force,enem;
    void Start()
    {
        camM.SetActive(false);
        force.SetActive(false);
        player.SetActive(false);
        
        enem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("interFase");
    }
    IEnumerator interFase()
    {
        yield return new WaitForSeconds(2f);
        cam1.SetActive(false);
        cam2.SetActive(true);
        yield return new WaitForSeconds(7.30f);
        SceneManager.LoadScene("Fight2");

    }
}
