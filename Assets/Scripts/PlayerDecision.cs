using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDecision : MonoBehaviour
{

    [SerializeField] TransitionAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MobileTransitionObject")
        {

            if (transform.childCount > 0)
            {

                int which = Random.Range(0, transform.childCount);

                Transform goal = transform.GetChild(which);

                goal.gameObject.SetActive(true);

                agent.KickOff(goal);

            }
        }
    }
}
