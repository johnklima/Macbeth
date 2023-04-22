using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatBot : MonoBehaviour
{

    [SerializeField] TransitionAgent transitionAgent;
    public Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //player is on the boat, so child him to the boat agent
            other.transform.parent = transitionAgent.transform;
            //also child the camera collider (this transform)
            transform.parent = transitionAgent.transform;
            //kick off the boat navigation
            transitionAgent.KickOff(target);

        }
            
    }
}
