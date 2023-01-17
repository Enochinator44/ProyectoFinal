using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaYdañoJugador : MonoBehaviour
{
    // Start is called before the first frame update
    public int vida = 100;
    private bool invencible = false;
    public float tiempoFrenado = 0.2f;
    private Animator Anim;
    public int vidMax = 100;
    public AudioClip otherClip;

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
            Anim.Play("muerte");
            
            StartCoroutine(Espera());
           
        }
    }
    public void RestarVida(int Cantidad)
    {
        if (!invencible && vida >0)
        {
            vida -= Cantidad;
            Anim.Play("Daño");
            StartCoroutine(Invulnerabilidad());
            StartCoroutine(FrenarVelocidad());
        }
        
        
    }
    public void RestarVidaEnemigo(int CantidadEnemigo)
    {
        vida -= CantidadEnemigo;
        Anim.Play("Daño2");
        StartCoroutine(Invulnerabilidad());
        StartCoroutine(FrenarVelocidad());
    }

    IEnumerator Invulnerabilidad()
    {
        invencible = true;
        yield return new WaitForSeconds(1.5f);
        invencible = false;
    }
    IEnumerator FrenarVelocidad()
    {
        var velocidadActual = GetComponent<PlayerController>().playerSpeed;
        GetComponent<PlayerController>().playerSpeed = 0;
        yield return new WaitForSeconds(tiempoFrenado);
        GetComponent<PlayerController>().playerSpeed = velocidadActual;

    }
    IEnumerator Espera()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(4);
    }
}
