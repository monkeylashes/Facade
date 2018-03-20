using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class Head : HeadBehavior {

    private GameObject player;

    private GameObject head;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("[CameraRig]");
        head = player.transform.FindChild("Camera (eye)").gameObject;
    }
	
	// Update is called once per frame
	void Update () {        
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;

            return;
        }

        // if we own this object then hook up to our head transforms
        transform.position = head.transform.position;
        transform.rotation = head.transform.rotation;

        // update network
        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
    }
}
