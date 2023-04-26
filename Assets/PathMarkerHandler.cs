using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMarkerHandler : AudioTimelineMarkerHandler
{
    [SerializeField] NPC[] npcs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void HandleIt(bool onoff)
    {
        Debug.Log(transform.name + " Handled It!!");

        if(onoff == true)
        {
            for (int i = 0; i < npcs.Length; i++)
            {
                npcs[i].enabled = true;
            }

        }
        
    }
}
