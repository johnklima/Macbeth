using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animState;
    
    [SerializeField] Transform target;
    [SerializeField] float speed;
   

    public float stopDistance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        

        agent = GetComponent<NavMeshAgent>();
        animState = GetComponent<Animator>();

        agent.SetDestination(target.position);

    }

    // Update is called once per frame
    void Update()
    {


        agent.SetDestination(target.position);

        //if (agent.remainingDistance < stopDistance)
        //    agent.isStopped = true;


        //find the speed
        Vector3 velo = new Vector3(agent.velocity.x, 0, agent.velocity.z);
        speed = velo.magnitude;

        if (speed > 0.00001f)
        {
            animState.SetBool("isWalking", true);
            animState.SetBool("isIdle", false);
        }
        else
        {
            animState.SetBool("isWalking", false);
            animState.SetBool("isIdle", true);

        }

        
    }

    public void setTarget(Transform newTarget)
    {
        //check on stuff before setting new target
        target = newTarget;
    }

    public NavMeshAgent getAgent()
    {
        return agent;
    }
}
