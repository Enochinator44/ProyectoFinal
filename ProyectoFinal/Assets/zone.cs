using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone : MonoBehaviour
{
    float  timing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timing += Time.deltaTime;
        if (timing >2)
        {
            Destroy(gameObject);
        }
    }
}
