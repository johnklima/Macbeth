using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class TransitionAgentDecision : MonoBehaviour
{

    [SerializeField] TransitionAgent agent;
    [SerializeField] string text;
    [SerializeField] Text textToShow;
    [SerializeField] Image imageToShow;
    [SerializeField] GameObject buttonToShow;  //buton base object is actually a gameobject, not a button per se

    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.name + " Start");
        
    }

    void ButtonClicked()
    {
        Debug.Log(transform.name + " ButtonClicked");
        //assign this transform to the agent, and kick him off
        agent.KickOff(transform);


        if (imageToShow)
        {
            //this IMAGE is the main container for 2d interaction
            imageToShow.enabled = false;
            
        }

        if (buttonToShow)
        {
            //hide ALL buttons too.
        
            RectTransform buttons = (RectTransform) buttonToShow.transform;
            foreach(GameObject child in buttons)
            {
                child.SetActive(false);
            }

            //will be reusing the button, so get it
            Button button = buttonToShow.GetComponent<Button>();
            
            //get the event
            Button.ButtonClickedEvent bevent = button.onClick;
            
            //remove all listeners
            bevent.RemoveAllListeners();

        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {

        //this happens when a parent has enabled the location
        Debug.Log(transform.name + " OnEnable");

        if (buttonToShow)
        {
            buttonToShow.SetActive(true);
            textToShow.text = text;

            //will be reusing the button, so get it
            Button button = buttonToShow.GetComponent<Button>();
            //get the event
            Button.ButtonClickedEvent bevent = button.onClick;

            //add me
            bevent.AddListener(ButtonClicked);

        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "MobileTransitionObject")
        {
            Debug.Log("Agent Hit Decision");

            if (textToShow)
            {
                textToShow.enabled = true;
                textToShow.text = text;
            }
            if(imageToShow)
            {
                imageToShow.enabled = true;                
            }
            if(buttonToShow)
            {
                buttonToShow.SetActive(true);
            }

            if (transform.childCount > 0)
            {
                for(int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);  
                }

            }
        }
    }
}
