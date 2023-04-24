using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionAgentRoot : MonoBehaviour
{
    [SerializeField] string text;                   // the question asked text
    [SerializeField] Text textToShow;               // the question prompt textbox
    [SerializeField] RectTransform GUIcontainer;    // the signpost to display the question

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "MobileTransitionObject") // a boat in most cases
        {
            Debug.Log("Agent Hit Decision Root");             

            if (transform.childCount > 0)
            {
                //this handles the activation of the child objects in this decision tree
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }

            if (GUIcontainer)
            {
                GUIcontainer.gameObject.SetActive(true);
            }

            if (textToShow)
                textToShow.text = text;

        }
    }
}
