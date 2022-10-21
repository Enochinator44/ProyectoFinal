using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{

    public GameObject filo;
    public GameObject ObjectToPickUp;
    public float TiempoAtaque, TiempoAtaque2, TiempoAtaque3;
    bool Ataque2 = false;
    bool Ataque3 = false;
    public bool Fa1;
    public bool Fa2;
    private bool AtaqueMelee, AtaqueRango;

    public GameObject Filo;
    public GameObject Pistola;

    // ATAQUE DISPARO
    public float Municion = 5;
    public float gravity = 20.0F;
    public GameObject bala;
    public GameObject salida;
    public bool AtaqueDisparo;
    private float tiempoAcumulado;
    private float tiempodeataque = 1;
    public bool sinarmaMele;
    public bool sinarmaRango;
    public AudioClip otherClip;

    void Start()
    {

        sinarmaMele = true;
        sinarmaRango = true;
        filo.SetActive(false);
        Pistola.SetActive(false);

        AtaqueDisparo = false;

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Alpha1) && sinarmaMele == false)
        {
            Filo.SetActive(true);
            Pistola.SetActive(false);
            AtaqueMelee = true;
            AtaqueRango = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && sinarmaRango == false)
        {
            Pistola.SetActive(true);
            Filo.SetActive(false);
            AtaqueMelee = false;
            AtaqueRango = true;
        }

        if (AtaqueDisparo == false && Input.GetKey(KeyCode.Mouse0) && Municion > 0 && AtaqueRango == true && Pistola.activeSelf == true)
        {
            
            gameObject.GetComponent<Animator>().Play("Disparo");
            GameObject.Instantiate(bala, salida.transform.position, transform.GetChild(0).rotation);
            AtaqueDisparo = true;
            Municion -= 1;
        }
        if (AtaqueDisparo == true)
        {
            tiempoAcumulado += Time.deltaTime;
            if (tiempoAcumulado > tiempodeataque)
            {
                AtaqueDisparo = false;
                tiempoAcumulado = 0;
            }
        }


        if (Input.GetMouseButtonDown(0) && Ataque2 == false && Ataque3 == false && AtaqueMelee == true && filo.activeSelf == true)
        {
            StartCoroutine(SonidoAtaque());
            filo.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.GetComponent<Animator>().SetBool("Ataque1", true);

            StartCoroutine(Finataque());








        }
        if (Input.GetMouseButtonDown(0) && Ataque2 == true && Fa1 == true && AtaqueMelee == true && filo.activeSelf == true)
        {
            StartCoroutine(SonidoAtaque());
            filo.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.GetComponent<Animator>().SetBool("Ataque2", true);

            StartCoroutine(Finataque2());



        }
        if (Input.GetMouseButtonDown(0) && Ataque3 == true && Fa2 == true && AtaqueMelee == true && filo.activeSelf == true)
        {
            StartCoroutine(SonidoAtaque());
            filo.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.GetComponent<Animator>().SetBool("Ataque3", true);

            StartCoroutine(Finataque3());

        }


    }


    IEnumerator Finataque()
    {
        yield return new WaitForSeconds(0.6f);
        Ataque2 = true;
        Fa1 = true;
        filo.GetComponent<BoxCollider>().isTrigger = false;
        gameObject.GetComponent<Animator>().SetBool("Ataque1", false);
        Debug.Log("FA1");
        yield return new WaitForSeconds(2);
        Fa1 = false;

        Ataque2 = false;



    }
    IEnumerator Finataque2()
    {
        yield return new WaitForSeconds(0.6f);
        Ataque2 = false;
        filo.GetComponent<BoxCollider>().isTrigger = false;
        gameObject.GetComponent<Animator>().SetBool("Ataque2", false);
        Debug.Log("FA2");
        Ataque3 = true;
        Fa2 = true;

    }
    IEnumerator Finataque3()
    {
        yield return new WaitForSeconds(1);
        Ataque3 = false;
        filo.GetComponent<BoxCollider>().isTrigger = false;
        gameObject.GetComponent<Animator>().SetBool("Ataque3", false);
        Debug.Log("FA3");
    }
    IEnumerator SonidoAtaque()
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = otherClip;
        audio.Play();
    }
}