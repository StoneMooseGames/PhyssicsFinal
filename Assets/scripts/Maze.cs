using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapLocation
{
    public int x;
    public int z;

    public MapLocation(int _x, int _z)
    {
        x = _x;
        z = _z;
    }
}
public class Maze : MonoBehaviour
{
    public List<MapLocation> directions = new List<MapLocation>() { new MapLocation(1,0),
                                                             new MapLocation(0,1),
                                                             new MapLocation(-1,0),
                                                             new MapLocation(0,-1)};
    public int width = 30;
    public int depth = 30;
    public byte[,] map;
    public int scale = 6;

    public GameObject wallGo;
    public GameObject playerGO;
    public GameObject exitGo;
    // Start is called before the first frame update
    void Start()
    {
        InitializeMap();
        Generate();
        DrawMap();
        PlacePlayer();
        GenerateExit();
    }
    void InitializeMap()
    {
        map = new byte[width, depth];
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                
                map[x, z] = 1;
            }
        }
    }

    public virtual void PlacePlayer()
    {
        int placeX = Random.Range(1, width);
        int placeZ = Random.Range(1, depth);

        if (map[placeX, placeZ] == 0)
        {
            playerGO.transform.position = new Vector3(placeX * scale, 1, placeZ * scale);
            return;
        }
        else
        {
            PlacePlayer();
        }
       
    }

    void GenerateExit()
    {
        int placeX = Random.Range(2, width - 2);
        int placeZ = Random.Range(2, depth - 2);

        if (map[placeX, placeZ] == 0)
        {
            Instantiate(exitGo);
            exitGo.transform.position = new Vector3(placeX * scale, 0, placeZ * scale);
            return;
        }
        else
        {
            GenerateExit();
        }
    }
    public virtual void Generate()
    {
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                map[x, z] = (byte)Random.Range(0, 2);
            }
        }
    }

    void DrawMap()
    {

        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if(map[x,z] == 1)
                {
                    Vector3 cellPosition = new Vector3(x*scale, 0, z*scale);
                    GameObject wall = Instantiate(wallGo);
                    wall.transform.localScale = new Vector3(scale, scale, scale);
                    wall.transform.position = cellPosition;
                }
                
               
            }
        }
    }

    public int CountSquareNeigbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if (map[x - 1, z] == 0) count++;
        if (map[x + 1, z] == 0) count++;
        if (map[x, z - 1] == 0) count++;
        if (map[x, z + 1] == 0) count++;
        return count;
    }

    public int CountDiagonalNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if (map[x - 1, z - 1] == 0) count++;
        if (map[x + 1, z - 1] == 0) count++;
        if (map[x - 1 , z + 1] == 0) count++;
        if (map[x + 1, z + 1] == 0) count++;
        return count;
    }

    public int CountAllNeighbours(int x, int z)
    {
        return CountSquareNeigbours(x, z) + CountDiagonalNeighbours(x, z);
    }
   
}
