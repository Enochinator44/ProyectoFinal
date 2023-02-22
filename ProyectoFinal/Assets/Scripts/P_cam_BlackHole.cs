using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P_cam_BlackHole : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime*speed, Space.World);
    }

    public void begin()
    {
        //float tiempo = 0; 
        //while (tiempo < 5)
        //{
        //    tiempo += Time.deltaTime;
        //    transform.Translate(-Vector3.right * Time.deltaTime * speed*2, Space.World);
        //}
        SceneManager.LoadScene("Inicio");
    }
}
