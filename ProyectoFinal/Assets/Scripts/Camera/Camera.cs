using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target, jugador,enemigo,Aim;
    public float distJugadorAenemigo;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Transform aux = target.transform;

        //aux.position = new Vector3(aux.position.x, aux.position.y, aux.position.z);

        //transform.LookAt(aux);


        //transform.LookAt(target.transform, target.transform.up);

        transform.LookAt(target.transform);

        distJugadorAenemigo = Vector3.Magnitude(jugador.transform.position - enemigo.transform.position);

        float ajuste = Mathf.Clamp(distJugadorAenemigo, 5, 40);
        float ajusteZ = Mathf.Clamp(distJugadorAenemigo, 3, 60);
        Vector3 v = new Vector3(Aim.transform.position.x, 0, 0);
       
        transform.position = new Vector3(v.magnitude , ajuste,(-1.5f*ajusteZ));

        //transform.position = new Vector3(transform.position.x, 1 + (1f * distJugadorAenemigo), 1 + (-2f * distJugadorAenemigo));


    }

     public IEnumerator Shake(float duracion , float magnitude)
    {
        Vector3 posOriginal = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed< duracion)
        {
            float x = Random.Range(0f, 0.02f)*magnitude;
            float y = Random.Range(0f, 0.02f) * magnitude;

            transform.localPosition = new Vector3(x, y, posOriginal.z);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        transform.localPosition = posOriginal;
    }
}
