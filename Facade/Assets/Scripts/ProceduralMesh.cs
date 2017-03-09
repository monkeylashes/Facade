﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProceduralMesh : MonoBehaviour {

    public Vector3[] vertices;
    public int[] triangles;
    Mesh mesh;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;                  
    }

	// Use this for initialization
	void Start () {
        MakeMeshData();
        CreateMesh();
	}
	
	void MakeMeshData()
    {
        vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(1, 0, 1)};
        triangles = new int[] {0,1,2,2,1,3};
    }

    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
