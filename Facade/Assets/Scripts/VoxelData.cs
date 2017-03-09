using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelData {

    public int[,,] data = new int[,,]
    {
        {
          { 0, 1, 1},
          { 0, 1, 1},
          { 0, 1, 1}
        },
        {
          { 0, 1, 1},
          { 0, 1, 1},
          { 1, 1, 1}
        },
        {
          { 0, 1, 1},
          { 1, 1, 1},
          { 1, 1, 1}
        },
        {
          { 1, 1, 1},
          { 1, 1, 1},
          { 1, 1, 1}
        },
    };

    public int Width
    {
        get
        {
            return data.GetLength(0);
        }
    }

    public int Height
    {
        get
        {
            return data.GetLength(1);
        }
    }

    public int Depth
    {
        get
        {
            return data.GetLength(2);
        }
    }    

    public void Generate()
    {
        int xDimension = (int)Random.Range(1, 50);
        int yDimension = (int)Random.Range(1, 5);
        int zDimension = (int)Random.Range(1, 50);

        data = new int[xDimension,yDimension,zDimension];

        for (int i = 0; i < xDimension; i++)
        {
            for (int j = 0; j < yDimension; j++)
            {
                for (int k = 0; k < zDimension; k++)
                {
                    if(Random.Range(0,100) > 50f)
                    {
                        data[i, j, k] = 1;
                    }
                    else
                    {
                        data[i, j, k] = 0;
                    }
                }
            }
        }
    }
}
