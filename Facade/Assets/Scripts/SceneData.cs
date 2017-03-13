using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour {

    Dictionary<Guid, GameObject> sceneLayout;

    public void AddToScene(GameObject userAreaContainer)
    {
        // generate guid
        userAreaContainer.GetComponent<UserAreaAttributes>().id = Guid.NewGuid();
        sceneLayout.Add(userAreaContainer.GetComponent<UserAreaAttributes>().id, userAreaContainer);     
    }      
}
