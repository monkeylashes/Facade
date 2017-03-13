using System;
using System.Collections.Generic;
using UnityEngine;

public class UserAreaAttributes : MonoBehaviour {

    public Guid id;

    public GameObject box;

    public Vector3 Center;

    public List<string> Tags;

    public Transform FrontAnchor;

    public Vector3 Size {
        get
        {
            return box.transform.localScale;
        }
    }

    public Vector3 Position {
        get
        {
            return box.transform.position;
        }
    }    


}
