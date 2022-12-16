using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wilsons : Maze
{
    
    List<MapLocation> notUsed = new List<MapLocation>();
    public override void Generate()
    {
        //create a starting cell
        int x = Random.Range(2, width - 1);
        int z = Random.Range(2, depth - 1);
        map[x, z] = 2;
        while(GetAvailableCells() > 1)
        RandomWalk();
    }

    int CountSquareMazeNeighbours(int x, int z)
    {
        int count = 0;
        for(int direction = 0; direction < directions.Count; direction++)
        {
            int nextX = x + directions[direction].x;
            int nextZ = z + directions[direction].z;
            if(map[nextX,nextZ] == 2)
            {
                count++;
            }
        }
        return count;
    }

    int GetAvailableCells()
    {
        notUsed.Clear();
        for(int z = 1; z<depth -1;z++)
            for (int x = 1; x < depth - 1; x++)
            {
                if(CountSquareMazeNeighbours(x,z) == 0)
                {
                    notUsed.Add(new MapLocation(x, z));
                }
            }
        return notUsed.Count;
    }
    void RandomWalk()
    {
        List<MapLocation> inWalk = new List<MapLocation>();
        int currentX;
        int currentZ;

        int randomStartIndex = Random.Range(0, notUsed.Count);
        currentX = notUsed[randomStartIndex].x;
        currentZ = notUsed[randomStartIndex].z;
        inWalk.Add(new MapLocation(currentX, currentZ));
        int loop = 0;
        bool validPath = false;
        while(currentX > 0 && currentX < width -1 && currentZ > 0 && currentZ < depth - 1 && loop < 5000 && !validPath)
        {
            map[currentX ,currentZ] = 0;
            /*if (CountSquareMazeNeighbours(currentX, currentZ) > 1)
            {
                break;
            }*/

            int randomDirection = Random.Range(0, directions.Count);
            int nextX = currentX + directions[randomDirection].x;
            int nextZ = currentZ + directions[randomDirection].z;
            if(CountSquareNeigbours(nextX,nextZ) < 2)
            {
                currentX = nextX;
                currentZ = nextZ;
                inWalk.Add(new MapLocation(currentX, currentZ));
            }
            validPath = CountSquareMazeNeighbours(currentX, currentZ) == 1;
            loop++; // this is for making sure that while loop doesn't accidentally end up being endless loop
        }

        if (validPath)
        {
            map[currentX, currentZ] = 0;
            Debug.Log("PathFound");

            foreach(MapLocation m in inWalk)
            {
                map[m.x, m.z] = 2;
            }
            inWalk.Clear();
        }
        else
        {
            foreach (MapLocation m in inWalk) map[m.x, m.z] = 1;
            inWalk.Clear();
        }
    }
}
