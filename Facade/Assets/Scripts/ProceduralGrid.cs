using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour {

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public float cellSize = 1;
    public Vector3 gridOffset = Vector3.zero;
    public int gridSizeX = 10;
    public int gridSizeY = 10;

    void Awake () {
        mesh = GetComponent<MeshFilter>().mesh;
	}

    void Start()
    {
        MakeConnectedProceduralGrid();
        UpdateMesh();
    }

    void MakeDiscreteProceduralGrid()
    {
        vertices = new Vector3[gridSizeX*gridSizeY*4];
        triangles = new int[gridSizeX*gridSizeY*6];

        int v = 0;
        int t = 0;



        float vertexOffset = cellSize * 0.5f;

        for(int x=0; x<gridSizeX; x++)
        {
            for(int y=0; y<gridSizeY; y++)
            {
                Vector3 cellOffet = new Vector3(x*cellSize, 0, y*cellSize);

                vertices[v] = new Vector3(-vertexOffset, 0, -vertexOffset) + gridOffset + cellOffet;
                vertices[v+1] = new Vector3(-vertexOffset, 0, vertexOffset) + gridOffset + cellOffet;
                vertices[v+2] = new Vector3(vertexOffset, 0, -vertexOffset) + gridOffset + cellOffet;
                vertices[v+3] = new Vector3(vertexOffset, 0, vertexOffset) + gridOffset + cellOffet;

                triangles[t] = v;
                triangles[t+1] = v+1;
                triangles[t+2] = v+2;
                triangles[t+3] = v+2;
                triangles[t+4] = v+1;
                triangles[t+5] = v+3;

                v += 4;
                t += 6;
            }
        }
    }

    void MakeConnectedProceduralGrid()
    {
        vertices = new Vector3[(gridSizeX + 1) * (gridSizeY + 1)];
        triangles = new int[gridSizeX * gridSizeY * 6];

        int v = 0;
        int t = 0;
        
        float vertexOffset = cellSize * 0.5f;

        for (int x = 0; x <= gridSizeX; x++)
        {
            for (int y = 0; y <= gridSizeY; y++)
            {
                vertices[v] = new Vector3((x * cellSize) - vertexOffset, 0, (y * cellSize) - vertexOffset);
                v++;
            }
        }

        v = 0;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                triangles[t] = v;
                triangles[t + 1] = v + 1;
                triangles[t + 2] = v + (gridSizeX + 1);
                triangles[t + 3] = v + (gridSizeX + 1);
                triangles[t + 4] = v + 1;
                triangles[t + 5] = v + (gridSizeX + 1) + 3;

                v++;
                t += 6;
            }
            v++;
        }

    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

    }

	
}
