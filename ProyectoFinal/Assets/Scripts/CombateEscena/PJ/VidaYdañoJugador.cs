using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaYdañoJugador : MonoBehaviour
{
    // Start is called before the first frame update
    public float vida = 100;
    private bool invencible = false;
    public float tiempoFrenado = 0.2f;
    private Animator Anim;
    public int vidMax = 100;
    public AudioClip otherClip;
    public Image vidaImg;

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }
    public void Update()
    {

        if (vida>vidMax)
        {
            vida = vidMax;
        }
        if (vida<=0)
        {
            GetComponent<ContT>().playerSpeed = 0;
            GetComponent<ContT>().dashSpeed = 0;
            GetComponent<ContT>().parry.SetActive(false);
            GetComponent<ContT>().escudo.SetActive(false);
            GetComponent<ContT>().bAtaqueCargado=false;
            Anim.Play("Muerte");
            
            StartCoroutine(Espera());

            vidaImg.fillAmount = vida / 100;
            
        }
    }
    public void RestarVida(float Cantidad)
    {
        if (!invencible && vida >0)
        {
            vida -= Cantidad;
            Anim.Play("Daño");
            vidaImg.fillAmount = vida / 100;
            StartCoroutine(Invulnerabilidad());
            //StartCoroutine(FrenarVelocidad());
        }
        
        
    }
    public void RestarVidaEnemigo(int CantidadEnemigo)
    {
        vida -= CantidadEnemigo;
        Anim.Play("Daño2");
        
    }

    IEnumerator Invulnerabilidad()
    {
        invencible = true;
        yield return new WaitForSeconds(1.5f);
        invencible = false;
    }
    //IEnumerator FrenarVelocidad()
    //{
    //    var velocidadActual = GetComponent<PlayerController>().playerSpeed;
    //    GetComponent<PlayerController>().playerSpeed = 0;
    //    yield return new WaitForSeconds(tiempoFrenado);
    //    GetComponent<PlayerController>().playerSpeed = velocidadActual;

    //}
    IEnumerator Espera()
    {
        gameObject.GetComponent<ContT>().enabled = false;
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<ContT>().enabled = true;
        SceneManager.LoadScene("Menu");
    }
}
