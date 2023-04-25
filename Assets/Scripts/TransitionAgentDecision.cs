using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class TransitionAgentDecision : MonoBehaviour
{

    [SerializeField] TransitionAgent agent;         // a boat in most cases
    [SerializeField] string text;                   // the question asked text, or the button text
    [SerializeField] Text textToShow;               // the question prompt textbox, or button text
    [SerializeField] RectTransform GUIcontainer;    // the signpost to display the question



    [SerializeField] RectTransform[] buttonsToHide;  //all the buttons that might be visible

    [SerializeField] RectTransform thisButton;      //the button choice that this decision point represents


    [SerializeField] bool isAQuestion = false;
    [SerializeField] bool isADestination = false;
    [SerializeField] DialogueCamera dialogueCamera;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.name + " Start");        
    }

    void myButtonClicked()
    {
        Debug.Log(transform.name + " ButtonClicked");
        
        //assign this transform to the agent, and kick him off
        agent.KickOff(transform);

        //hide all the GUI elements
        for(int i = 0; i < buttonsToHide.Length; i++)
        {
            buttonsToHide[i].gameObject.SetActive(false);

            Button button = buttonsToHide[i].GetComponent<Button>();

            //get the event from it
            Button.ButtonClickedEvent bevent = button.onClick;

            //remove all listeners this decision node may have added
            bevent.RemoveAllListeners();

        }

        if (GUIcontainer)
            GUIcontainer.gameObject.SetActive(false);
        

    }

    private void OnEnable()    
    {

        //this happens when a parent has enabled the location/decision

        Debug.Log(transform.name + " OnEnable");

        //any GUI associated with this decision node?
        if (thisButton)
        {
            thisButton.gameObject.SetActive(true);            

            //will be reusing the button, so get the actual button component
            Button button = thisButton.GetComponent<Button>();
            
            
            //get the event from it
            Button.ButtonClickedEvent bevent = button.onClick;

            //add me as a listener to the click
            bevent.AddListener(myButtonClicked);

            //assign text to button
            textToShow.text = text;

        }

        if (GUIcontainer)
        {
            GUIcontainer.gameObject.SetActive(true);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "MobileTransitionObject") // a boat in most cases
        {
            Debug.Log("Agent Hit Decision");

            //if it is a question being asked, we stuff the GUI
            if( isAQuestion )
            {
                if (thisButton)
                {
                    thisButton.gameObject.SetActive(true);

                }

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

            }
            else if (isADestination)
            {
                //release the dialogue camera
                dialogueCamera.DisableDialogueCamera();
            }

            
        }
    }
}
