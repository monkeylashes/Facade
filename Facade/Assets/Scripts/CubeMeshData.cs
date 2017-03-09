using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubeMeshData {
    // first 4 are North face, and the second 4 are the South face of the cube.
    public static Vector3[] vertices =
    {
        new Vector3(0,0,0),
        new Vector3(0,1,0),
        new Vector3(1,1,0),
        new Vector3(1,0,0),
        new Vector3(0,0,1),
        new Vector3(0,1,1),
        new Vector3(1,1,1),
        new Vector3(1,0,1)
    };

    public static int[][] faceTriangles =
    {
        new int[] { 0, 1, 2, 3},
        new int[] { 4, 7, 6, 5},
        new int[] { 0, 4, 5, 1},
        new int[] { 3, 2, 6, 7},
        new int[] { 1, 5, 6, 2},
        new int[] { 0, 3, 7, 4}
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir"> direction of the face we want vertices of. {South:0,N,W,E,U,D}</param>
    /// <returns></returns>
    public static Vector3[] FaceVertices(int dir, float scale, Vector3 position)
    {
        // the four vertices making up a face
        Vector3[] fv = new Vector3[4];

        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = vertices[faceTriangles[dir][i]] * scale + position;
        }

        return fv;        
    }
}
