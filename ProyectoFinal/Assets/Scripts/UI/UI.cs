using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update

    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Inicio");
    }
    public void ResumeGame()
    {
        SceneManager.LoadScene("Fight");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
