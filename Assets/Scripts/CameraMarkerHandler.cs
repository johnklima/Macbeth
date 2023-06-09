using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraMarkerHandler : AudioTimelineMarkerHandler
{

    [SerializeField] RectTransform messageSign;
    [SerializeField] Text textToShow;
    [SerializeField] string text;

    private void Start()
    {
        
    }

    public override void HandleIt(bool onoff)
    {
        //in the case of the dialog camera, we release upon true marker
        
        if( onoff == true)
        {
            Debug.Log(transform.name + " Handled It!!");
            transform.GetComponent<DialogueCamera>().DisableDialogueCamera();
            if(messageSign)
            {
                messageSign.gameObject.SetActive(true);
                textToShow.text = text;

            }

        }
        
    }
}
