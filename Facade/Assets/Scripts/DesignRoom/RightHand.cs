using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
public class RightHand : RightHandBehavior {

    private GameObject player;

    private GameObject rightController;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("[CameraRig]");
        rightController = player.transform.FindChild("Controller (right)").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;

            return;
        }

        // if we own this object then hook up to our head transforms
        transform.position = rightController.transform.position;
        transform.rotation = rightController.transform.rotation;

        // update network
        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
    }
}
