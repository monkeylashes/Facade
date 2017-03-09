using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class ProceduralCube : MonoBehaviour {

    public float scale = 1f;
    public int posX, posY, posZ;

    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;


    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    void Start()
    {
        float halfScale = scale * 0.5f;
        MakeCube(scale, new Vector3(posX * scale, posY * scale, posZ * scale));
        UpdateMesh();
    }

    void MakeCube(float cubeScale, Vector3 cubePosition)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < 6; i++)
        {
            MakeFace(i, cubeScale, cubePosition);
        }        
    }

    private void MakeFace(int dir, float faceScale, Vector3 facePosition)
    {        
        vertices.AddRange(CubeMeshData.FaceVertices(dir, faceScale, facePosition));
        int vCount = vertices.Count;

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 3);
        triangles.Add(vCount - 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 2);
        triangles.Add(vCount - 1);
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
}
