using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Decision : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        Debug.Log(transform.name + " enabled");   
    }

    private void OnDisable()
    {
        Debug.Log(transform.name + " disabled");
    }

    private void OnTriggerEnter(Collider other)
    {
        NavMeshAgent agent = null;

        if(other.tag == "NPC" )
        {

            NPC npc = other.transform.GetComponent<NPC>();

            agent = npc.getAgent();
            agent.isStopped = true;

            

            if(transform.childCount > 0)
            {
                
                int which = Random.Range(0, transform.childCount);
                
                Debug.Log(which);
                
                Transform goal = transform.GetChild(which);

                goal.gameObject.SetActive(true);
                npc.setTarget(goal);
                
                Debug.Log(other.name + " goal " + goal.name );

            }

        }
    }
}
