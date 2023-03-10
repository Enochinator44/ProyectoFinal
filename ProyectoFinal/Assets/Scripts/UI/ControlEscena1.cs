using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlEscena1 : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public Image continuar,fondoContinuar,titulo;
    public Image cortinilla;
    public TextMeshProUGUI carga;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        continuar.CrossFadeAlpha(1f, 2f, false);
        fondoContinuar.CrossFadeAlpha(1, 2f, false);
        titulo.CrossFadeAlpha(1, 2f, false);
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        cortinilla.CrossFadeAlpha(0, 0, false);
        continuar.CrossFadeAlpha(0, 0, false);
        fondoContinuar.CrossFadeAlpha(0, 0, false);
        titulo.CrossFadeAlpha(0, 0, false);
        carga.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name=="Menu")
        {
            if (Input.anyKeyDown)
            {
                CargasEscena();
                continuar.CrossFadeAlpha(0f, 1f, false);
                fondoContinuar.CrossFadeAlpha(0, 1f, false);
            }
        }
      
        
    }

    public void CargasEscena()
    {
        StartCoroutine(CargarEscenaC(1));
    }
    public IEnumerator CargarEscenaC(int scene)
    {
        AsyncOperation a = SceneManager.LoadSceneAsync(scene);
        a.allowSceneActivation = false;

        while (a.progress<=0.9f)
        {
            
            carga.text = "Loading: " +/* Mathf.FloorToInt(a.progress) +*/a.progress+ "%";
            Debug.Log(a.progress);

            if (a.progress>=0.9f)
            {
                
                carga.text = "";
                cam.GetComponent<P_cam_BlackHole>().begin();
                yield return new WaitForSeconds(1f);
                cortinilla.CrossFadeAlpha(1, 4, false);
                titulo.CrossFadeAlpha(0, 2.5f, false);
                yield return new WaitForSeconds(4f);

                a.allowSceneActivation = true;
              
                
            }
            yield return null;
        }
        
        
        yield return new WaitForSeconds(1);
        cortinilla.CrossFadeAlpha(0, 2, false);
        yield return null;
    }
   
}
