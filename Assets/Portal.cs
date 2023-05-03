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

            if(dot > 0)  //player forward is in the same direction
                teleport = true;
            else         //player forward is opposite direction
                teleport = false;

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
