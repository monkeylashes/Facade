using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VoxelRenderer : MonoBehaviour {

    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;
    List<Color> colors;

    public float scale = 1f;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Use this for initialization
    void Start () {
        GenerateVoxelMesh(new VoxelData());
        UpdateMesh(); ;
	}
	
    void GenerateVoxelMesh(VoxelData data)
    {
        data.Generate();
        vertices = new List<Vector3>();
        triangles = new List<int>();
        colors = new List<Color>();

        for (int x = 0; x < data.Width; x++)
        {
            for(int y= 0; y < data.Height; y++)
            {
                for (int z = 0; z < data.Depth; z++)
                {
                    //Debug.Log(string.Format("{0}/{1}, {2}/{3}, {4}/{5}", x,data.Width, y, data.Height, z, data.Depth));
                    if (data.data[x, y, z] == 0)
                    {
                        continue;
                    }

                    MakeCube(scale, new Vector3((float)x * scale, (float)y * scale, (float)z * scale));                    
                }
            }            
        }
    }


    void MakeCube(float cubeScale, Vector3 cubePosition)
    {      
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
        mesh.colors = colors.ToArray();
        mesh.RecalculateNormals();
    }

}
