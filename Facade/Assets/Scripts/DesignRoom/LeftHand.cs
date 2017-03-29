using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class LeftHand : LeftHandBehavior {

    private GameObject player;

    private GameObject leftController;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("[CameraRig]");
        leftController = player.transform.FindChild("Controller (left)").gameObject;
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
        transform.position = leftController.transform.position;
        transform.rotation = leftController.transform.rotation;

        // update network
        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
    }
}
