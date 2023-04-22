//--------------------------------------------------------------------
//
// This is a Unity behaviour script that demonstrates how to use
// timeline markers in your game code. 
//
// Timeline markers can be implicit - such as beats and bars. Or they 
// can be explicity placed by sound designers, in which case they have 
// a sound designer specified name attached to them.
//
// Timeline markers can be useful for syncing game events to sound
// events.
//
// The script starts a piece of music and then displays on the screen
// the current bar and the last marker encountered.
//
// This document assumes familiarity with Unity scripting. See
// https://unity3d.com/learn/tutorials/topics/scripting for resources
// on learning Unity scripting. 
//
// For information on using FMOD example code in your own programs, visit
// https://www.fmod.com/legal
//
//--------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using UnityEngine;

class ScriptUsageTimeline : MonoBehaviour
{
    class TimelineInfo
    {
        public int CurrentMusicBar = 0;
        public FMOD.StringWrapper LastMarker = new FMOD.StringWrapper();
        public String objName;
        public Transform owner;       
        public AudioTimelineMarkerHandler handler;
    }

    TimelineInfo timelineInfo;
    GCHandle timelineHandle;

    public FMODUnity.EventReference EventName;

    
    FMOD.Studio.EVENT_CALLBACK markerCallback;
    FMOD.Studio.EventInstance eventInstance;


    void Start()
    {
        timelineInfo = new TimelineInfo();

        timelineInfo.owner = transform;
        timelineInfo.objName = transform.name;
        timelineInfo.handler = transform.GetComponent<AudioTimelineMarkerHandler>();

        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        markerCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

        eventInstance = FMODUnity.RuntimeManager.CreateInstance(EventName);

        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo);
        // Pass the object through the userdata of the instance
        eventInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        eventInstance.setCallback(markerCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        eventInstance.start();

        
        
    }

    void OnDestroy()
    {
        eventInstance.setUserData(IntPtr.Zero);
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        eventInstance.release();
        timelineHandle.Free();
    }

    /*void OnGUI()
    {
        GUILayout.Box(String.Format("Current Bar = {0}, Last Marker = {1}", timelineInfo.CurrentMusicBar, (string)timelineInfo.LastMarker));
    }
    */

    /// <summary>
    /// this is a static function so we use a class pointer to get user data from
    /// our timelineInfo object we created for this instance
    /// </summary>
    /// <param name="type"></param>
    /// <param name="instancePtr"></param>
    /// <param name="parameterPtr"></param>
    /// <returns></returns>
    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        // Retrieve the user data
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.CurrentMusicBar = parameter.bar;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.LastMarker = parameter.name;
                        
                        Debug.Log("MARKER " + parameter.name);

                        //ALL objects will be getting the FMOD callback if it has this script
                        //to make this easy for copy/paste in FMOD, markers we use a String.Contains()

                        String marker = parameter.name;

                        if ( marker.Contains(timelineInfo.objName) )
                        {
                            //if this marker is pointing at me, handle me.
                            timelineInfo.handler.HandleIt(true);
                        }
                        else 
                        {
                            //otherwise, handle me with a false condition, it's not me.
                            timelineInfo.handler.HandleIt(false);
                        }
                        

                    }
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }
}