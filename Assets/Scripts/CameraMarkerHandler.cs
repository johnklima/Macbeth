using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMarkerHandler : AudioTimelineMarkerHandler
{

    public override void HandleIt(bool onoff)
    {
        //in the case of the dialog camera, we release upon true marker
        
        if( onoff == true)
        {
            Debug.Log(transform.name + " Handled It!!");
            transform.GetComponent<DialogueCamera>().DisableDialogueCamera();
        }
        
    }
}
