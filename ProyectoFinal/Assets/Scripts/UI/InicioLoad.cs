using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class InicioLoad : MonoBehaviour
{
    // Start is called before the first frame update
    public Image cortinilla;
    public TextMeshProUGUI carga, simbolotxt;
    void Start()
    {
        cortinilla.CrossFadeAlpha(0, 0, false);
        carga.CrossFadeAlpha(0, 0, false);
        simbolotxt.CrossFadeAlpha(0, 0, false);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CargasEscena()
    {
        StartCoroutine(CargarEscenaC());
    }
    public IEnumerator CargarEscenaC()
    {
        AsyncOperation a = SceneManager.LoadSceneAsync(3);
        a.allowSceneActivation = false;
        while (a.progress <= 0.9f)
        {
            simbolotxt.CrossFadeAlpha(1,1.3f, false);
            carga.text = "Loading... " /*+ Mathf.FloorToInt(a.progress) + a.progress + "%"*/;
            Debug.Log(a.progress);

            if (a.progress >= 0.9f)
            {

                carga.CrossFadeAlpha(0, 1.5f, false);
                simbolotxt.CrossFadeAlpha(0, 0, false);

                yield return new WaitForSeconds(1f);
                cortinilla.CrossFadeAlpha(1, 2, false);
                
                yield return new WaitForSeconds(2f);

                a.allowSceneActivation = true;


            }
            yield return null;
        }


        yield return new WaitForSeconds(1);
        cortinilla.CrossFadeAlpha(0, 2, false);
        yield return null;
    }
}
