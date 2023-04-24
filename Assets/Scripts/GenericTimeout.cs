using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTimeout : MonoBehaviour
{

    [SerializeField] float delay = 3.0f;
    float timer = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0 && Time.time - timer > delay)
        {            
            timer = -1;
            this.gameObject.SetActive(false);
        }
    }

    public void KickOff()
    {
        timer = Time.time;
    }
}
