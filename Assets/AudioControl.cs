using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        
    }


    FMOD.Studio.Bus music;
    FMOD.Studio.Bus master;
    FMOD.Studio.Bus sfx;

    // Start is called before the first frame update
    void Start()
    {
        
        master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        

    }


    public void setVolumeMaster()
    {
        master.setVolume(GetComponent<Slider>().value);

    }

}
