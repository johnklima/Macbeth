using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionSignTimeout : MonoBehaviour
{

    [SerializeField] Text textToHide;
    [SerializeField] Image imageToHide;

    [SerializeField] float delay;
    private float timer = -1;



    // Update is called once per frame
    void Update()
    {
        if(timer > 0 && Time.time - timer > delay)
        {
            textToHide.enabled = false;
            imageToHide.enabled = false;
            timer = -1;
        }        
    }

    public void KickOff()
    {
        timer = Time.time;
    }
}
