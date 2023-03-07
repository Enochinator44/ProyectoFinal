using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ControlEscenaMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject goJugar,goOpciones,goExit;
    public Animator anim;
    public Image cortinillaFalsa;


    private void Start()
    {
        cortinillaFalsa.CrossFadeAlpha(0, 0, false);
        StartCoroutine(CorrutinaFalsaCo());

    }
    private void Update()
    {
        
    }
    public void PlayButton()
    {
        
        if (anim.GetBool("Opciones")==true)
        {
            anim.SetBool("Opciones", false);
            anim.SetBool("Jugar", true);
        }
        if (anim.GetBool("Salir") == true)
        {
            anim.SetBool("Salir", false);
            anim.SetBool("Jugar", true);
        }
       
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Inicio");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Fight");
    }
    public void OptionsButton()
    {
        if (anim.GetBool("Jugar") == true)
        {
            anim.SetBool("Jugar", false);
            anim.SetBool("Opciones", true);
        }
        if (anim.GetBool("Salir") == true)
        {
            anim.SetBool("Salir", false);
            anim.SetBool("Opciones", true);
        }
    }
    public void ExitButton()
    {

        if (anim.GetBool("Jugar") == true)
        {
            anim.SetBool("Jugar", false);
            anim.SetBool("Salir", true);
        }
        if (anim.GetBool("Opciones") == true)
        {
            anim.SetBool("Opciones", false);
            anim.SetBool("Salir", true);
        }

    }
    public void ExitConfirmationYes()
    {
        Application.Quit();
        Debug.Log("Yes");
    }
    public void ExitConfirmationNo()
    {
        Debug.Log("no");
        anim.SetBool("Jugar", true);

        anim.SetBool("Salir", false);
    }
    public IEnumerator CorrutinaFalsaCo()
    {
        cortinillaFalsa.CrossFadeAlpha(1, 0, false);
        yield return new WaitForEndOfFrame();
        cortinillaFalsa.CrossFadeAlpha(0, 0.5f, false);
    }
}
