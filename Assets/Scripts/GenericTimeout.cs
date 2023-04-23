using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTimeout : MonoBehaviour
{

    float delay = 3.0f;
    float timer = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - timer > delay)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
