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
            teleport = true;
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
