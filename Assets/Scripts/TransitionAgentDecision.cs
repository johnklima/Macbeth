using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionAgentDecision : MonoBehaviour
{

    [SerializeField] TransitionAgent agent;
    [SerializeField] string text;
    [SerializeField] Text textToShow;
    [SerializeField] Image imageToShow;


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
            Debug.Log("Agent Hit Decision");

            if (transform.childCount > 0)
            {
                //test first with random
                int which = Random.Range(0, transform.childCount);

                Transform goal = transform.GetChild(which);

                goal.gameObject.SetActive(true);

                agent.KickOff(goal);

            }
        }
    }
}
