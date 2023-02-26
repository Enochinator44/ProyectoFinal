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
        speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
    }

    public void begin()
    {
        StartCoroutine(cBegin());
        
    }
    IEnumerator cBegin()
    {
        speed = 0.25f;
        yield return new WaitForSeconds(4);
        //SceneManager.LoadScene("Inicio");
    }
}
