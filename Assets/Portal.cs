using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform ToLocation;

    bool teleport = false;
    Transform player;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " portal");
        if(other.tag == "Player")
        {
                        
            player = other.transform;
            //dot product must use unit vectors
            float dot = Vector3.Dot(player.forward, transform.forward);
            Debug.Log("the dot is " + dot);

            float deg = Mathf.Rad2Deg * Mathf.Acos(dot);
            float uni = Vector3.Angle(player.forward, transform.forward);
            
            Debug.Log("unity says " + uni + " mathf says " + deg);

            if(dot > 0)  //player forward is in the same direction
                teleport = true;
            else         //player forward is opposite direction
                teleport = false;

        }
        if (other.tag == "Gun")
        {

            Debug.Log(other.name + " Move the gun");
            Transform gun = other.transform;

            gun.parent = null;

            Vector3 pos = ToLocation.position + ToLocation.forward * 3;
            gun.SetPositionAndRotation(pos, ToLocation.rotation);

        }


    }

    private void LateUpdate()
    {
        if(teleport)
        {
            player.transform.SetPositionAndRotation(ToLocation.position, ToLocation.rotation);
            Camera.main.GetComponent<CameraContoller>().CameraReset(ToLocation.position, ToLocation.rotation);

            teleport = false;

        }
    }
}
