using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target, jugador,enemigo;
    public float distJugadorAenemigo;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(target.transform);

        distJugadorAenemigo = Vector3.Magnitude(jugador.transform.position - enemigo.transform.position);
       
        transform.position = new Vector3(transform.position.x, 1 + (1f*distJugadorAenemigo), 1 + (-2f * distJugadorAenemigo));
      

    }

     public IEnumerator Shake(float duracion , float magnitude)
    {
        Vector3 posOriginal = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed< duracion)
        {
            float x = Random.Range(-1f, 1f)*magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, posOriginal.z);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        transform.localPosition = posOriginal;
    }
}
