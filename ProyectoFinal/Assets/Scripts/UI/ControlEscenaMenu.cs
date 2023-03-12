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
    public Animator anim,playerAnim;
    public Image cortinillaFalsa;
    public GameObject player;
    public int impulsoSalto;
    private AsyncOperation asyncOperation, asyncOperationb;
    public TextMeshProUGUI textCarga;


    private void Start()
    {
        cortinillaFalsa.CrossFadeAlpha(0, 0, false);
        textCarga.CrossFadeAlpha(0, 0, false);
        StartCoroutine(CorrutinaFalsaCo());
        asyncOperation  = SceneManager.LoadSceneAsync(2);
        asyncOperation.allowSceneActivation = false;
        asyncOperationb = SceneManager.LoadSceneAsync(3);
        asyncOperationb.allowSceneActivation = false;

    }
    private void Update()
    {
        if (asyncOperation.progress >= 0.9)
        {
            textCarga.CrossFadeAlpha(0, 1.5f, false);
            Debug.Log("Hola");
        }
        if (asyncOperation.progress<0.9)
        {
            textCarga.CrossFadeAlpha(1, 1.5f, false);
            textCarga.text = "Loading...";
            
        }
       

        Debug.Log(asyncOperation.progress);
        Debug.Log(asyncOperationb.progress);
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
        StartCoroutine(AnimacionSaltoNG());
    }
    public void LoadGame()
    {
        StartCoroutine(AnimacionSaltoLG());
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
    public IEnumerator AnimacionSaltoNG()
    {
        if (asyncOperation.progress==0.9)
        {
            playerAnim.SetTrigger("Jump");
            yield return new WaitForSeconds(0.3f);
            player.GetComponent<Rigidbody>().AddForce(0, 0, impulsoSalto, ForceMode.Impulse);
            yield return new WaitForSeconds(.7f);
            asyncOperation.allowSceneActivation = true;
        }
       
        


    }
    public IEnumerator AnimacionSaltoLG()
    {
        if (asyncOperationb.progress==0.9)
        {
            playerAnim.SetTrigger("Jump");
            yield return new WaitForSeconds(0.3f);
            player.GetComponent<Rigidbody>().AddForce(0, 0, impulsoSalto, ForceMode.Impulse);
            yield return new WaitForSeconds(.7f);
            asyncOperationb.allowSceneActivation = true;
        }
        


    }
}
