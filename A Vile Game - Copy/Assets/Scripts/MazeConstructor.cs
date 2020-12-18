using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;
    private MazeDataGenerator dataGenerator;//needed to generate the layout for the maze
    private MazeMeshGenerator meshGenerator;//needed to generate the textures of the maze
    [SerializeField] private FpsMovement player;
    [SerializeField] private Material mazeMat1;
    [SerializeField] private Material mazeMat2;
    [SerializeField] private Material endMat;
    [SerializeField] private Material startMat;


    public int[,] data
    {
        get; private set;
    }

    void Awake()
    {
        dataGenerator = new MazeDataGenerator();//used for layout
        meshGenerator = new MazeMeshGenerator();//used for textures

        //default to walls surrounding a empty cell resulting in a single room
        data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
    }

    private void DisplayMaze()
    {
        //creates games objects and tags them accordingly 
        GameObject go = new GameObject();
        go.transform.position = Vector3.zero;
        go.name = "Procedural Maze";
        go.tag = "Generated";
        //adds the correct associated materials
        MeshFilter mf = go.AddComponent<MeshFilter>();
        mf.mesh = meshGenerator.FromData(data);

        MeshCollider mc = go.AddComponent<MeshCollider>();
        mc.sharedMesh = mf.mesh;

        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        mr.materials = new Material[2] { mazeMat1, mazeMat2 };
    }


    
    public float hallWidth
    {
        get; private set;
    }
    public float hallHeight
    {
        get; private set;
    }

    public int startRow
    {
        get; private set;
    }
    public int startCol
    {
        get; private set;
    }

    public int goalRow
    {
        get; private set;
    }
    public int goalCol
    {
        get; private set;
    }
    public int middleRow
    {
        get; private set;
    }
    public int middleCol
    {
        get; private set;
    }

    public void DisposeOldMaze()
    {
        //checks to see if objects have "Generated" tag and then deletes them
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Generated");
        foreach (GameObject go in objects)
        {
            Destroy(go);
        }
    }

    private void FindStartPosition()
    {
        //cycles through the maze to find the start position
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    //if(i== rMax && j == cMax)
                    //{
                    middleRow = rMax/2;
                    middleCol = cMax/2;
                    //}
                    startRow = i;
                    startCol = j;
                    return;
                }
            }
        }
    }

    private void FindGoalPosition()
    {
        //cycles through the maze to find the end position
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        // loop top to bottom, right to left
        for (int i = rMax; i >= 0; i--)
        {
            for (int j = cMax; j >= 0; j--)
            {
                if (maze[i, j] == 0)
                {
                    goalRow = i;
                    goalCol = j;
                    return;
                }
            }
        }
    }
    private void PlaceStart(TriggerEvent callback)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = new Vector3(startCol * hallWidth, .5f, startRow * hallWidth);
        go.name = "Start";
        go.tag = "Generated";

        go.GetComponent<BoxCollider>().isTrigger = true;
        go.GetComponent<MeshRenderer>().sharedMaterial = startMat;

        TriggerEventRouter tc = go.AddComponent<TriggerEventRouter>();
        tc.callback = callback;
    }
    private void PlaceEnd(TriggerEvent callback)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = new Vector3(goalCol * hallWidth, .5f, goalRow * hallWidth);
        go.name = "End";
        go.tag = "Generated";

        go.GetComponent<BoxCollider>().isTrigger = true;
        go.GetComponent<MeshRenderer>().sharedMaterial = endMat;

        TriggerEventRouter te = go.AddComponent<TriggerEventRouter>();
        te.callback = callback;


    }

    public void GenerateNewMaze(int sizeRows, int sizeCols, TriggerEvent endCallback=null, TriggerEvent startCallback=null)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }

        DisposeOldMaze();//removes the old mazes so a new one is generated each time

        data = dataGenerator.FromDimensions(sizeRows, sizeCols);//generates the layout based on given dimensions

        FindStartPosition();
        FindGoalPosition();

        // store values used to generate this mesh
        hallWidth = meshGenerator.width;
        hallHeight = meshGenerator.height;

        DisplayMaze();//generates maze in the scene
        PlaceStart(startCallback);
        PlaceEnd(endCallback);
        
    }


}
