using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextMarkerHandler : AudioTimelineMarkerHandler
{

    
    [SerializeField] Text textShow;
    [SerializeField] Image imageToShow;
    [SerializeField] string textToShow;
    [SerializeField] DecisionSignTimeout signTimeout;

    public override void HandleIt(bool onoff)
    {
        Debug.Log(transform.name + " HandledIt!!");

        if(onoff == true)
        {
            imageToShow.enabled = true;
            textShow.enabled = true;
            textShow.text = textToShow;
            signTimeout.KickOff();
        }

    }

    public void SetText(string _textToShow)
    {
        textToShow = _textToShow;
    }
}
