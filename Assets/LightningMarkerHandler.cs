using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningMarkerHandler : AudioTimelineMarkerHandler
{
    [SerializeField] Light theLight;

    float timer = -1;
    private Transform audioObj;
    private int curChild = 0;
    public override void HandleIt(bool onoff)
    {
        Debug.Log(transform.name + " Handled It!!");

        if (onoff == true)
        {
            timer = Time.time;
        }
       

    }

    
    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            Debug.Log("DO LIGHTNING!!!");
            //do something
            timer = -1;

            transform.GetChild(curChild).gameObject.SetActive(true);
            curChild++;

            if (curChild >= transform.childCount)
                curChild = 0;
        }
    }
}
