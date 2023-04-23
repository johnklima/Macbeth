using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextMarkerHandler : AudioTimelineMarkerHandler
{

    
    [SerializeField] Text textToShow;
    [SerializeField] Image imageToShow;
    [SerializeField] string text;
    [SerializeField] DecisionSignTimeout signTimeout;

    public override void HandleIt(bool onoff)
    {
        Debug.Log(transform.name + " HandledIt!!");

        if(onoff == true)
        {
            imageToShow.enabled = true;
            textToShow.enabled = true;
            textToShow.text = text;
            signTimeout.KickOff();
        }

    }

    public void SetText(string _text)
    {
        text = _text;
    }
}
