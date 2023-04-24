using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueStarter : MonoBehaviour
{

    [SerializeField] Transform[] characters;
    

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < characters.Length;i++)
        {
            //this causes all timelines for the characters in the act/scene to start
            characters[i].GetComponent<ScriptUsageTimeline>().startTimeline();
        }


        //if I also have a ScriptUsageTimeline start me too
        ScriptUsageTimeline sut = transform.GetComponent<ScriptUsageTimeline>();
        if (sut)
            sut.startTimeline();


    }
}
