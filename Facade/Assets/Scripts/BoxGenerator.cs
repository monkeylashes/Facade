using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateCube(Vector3 position, Vector3 size)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = position;
        cube.transform.localScale = size;       

        cube.AddComponent<Attributes>();
    }

    public void SetAttributes(string name, string[] tags, Vector3 direction, GameObject cube)
    {
        Attributes at = cube.GetComponent<Attributes>();
        at.Name = name;
        at.Tags = tags;
        at.direction = direction;
    }
}
