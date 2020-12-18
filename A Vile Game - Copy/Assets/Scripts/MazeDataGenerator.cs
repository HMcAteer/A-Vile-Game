using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDataGenerator
{
    public float spaceThresh;//used to determine if there is empty space

    public MazeDataGenerator()
    {
        spaceThresh = .1f;
    }

    public int[,] FromDimensions(int sizeRows, int sizeCols)
    {
        int[,] maze = new int[sizeRows, sizeCols];
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                //checks to see if cell is on the outside of the grid and assigns a 1 for the wall
                if (i == 0 || j == 0 || i == rMax || j == cMax)
                {
                    maze[i, j] = 1;
                }

                //checks to make sure coordinates are evenly divisible by 2, so that it can do every other cell
                else if (i % 2 == 0 && j % 2 == 0)
                {
                    if (Random.value > spaceThresh)
                    {
                        
                        maze[i, j] = 1;
                        //randomly coose adjacent cells
                        int a = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);
                        maze[i + a, j + b] = 1;
                    }
                }
            }
        }

        return maze;
    }
    
}
