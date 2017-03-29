using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;

public class GameInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        NetworkManager.Instance.InstantiateHeadNetworkObject();
        NetworkManager.Instance.InstantiateLeftHandNetworkObject();
        NetworkManager.Instance.InstantiateRightHandNetworkObject();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
