using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TransitionAgent : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animState;
    private bool go = false;

    [SerializeField] Transform target;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animState = GetComponent<Animator>();
    }
    public void KickOff( Transform _target)    
    {
        go = true;
        target = _target;
        agent.isStopped = false;
    }
    // Update is called once per frame
    void Update()
    {
        //refresh the destination
        if (target && go)
            agent.SetDestination(target.position);
        

    }
}
