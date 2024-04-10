using System.Collections.Generic;
using System.Diagnostics;
using System;
using UnityEngine;


public class PathFinder : MonoBehaviour
{
    public static PathFinder Instance;

    private static List<Node> tilesData;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GridGenerator.Instance.OnGridGenerated += GridGenerator_OnGridGenerated;

    }

    private void GridGenerator_OnGridGenerated(object sender, EventArgs e)
    {
        UnityEngine.Debug.Log("Heya");
        SetTilesData(GridGenerator.Instance.GetTilesData());

    }
    public void SetTilesData(List<Node> data) { tilesData = data; }
    
    public static List<Node> StartPathFindingAStar(int start, int end)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        bool pathFound = false;

        int maxIndex = GridGenerator.Instance.GetMaxIndex();

        List<int> openTiles = new List<int>(maxIndex);
        openTiles.Add(start);
        List<int> closedTiles = new List<int>(maxIndex);

        int[] prev = new int[maxIndex];//For keeping record of which tiles is reached by which index

        while (openTiles.Count > 0)
        {
            int current = openTiles[0];//Change this to lowest f cost

            for (int i = 0; i < openTiles.Count; i++)
            {

                if ((tilesData[openTiles[i]].fCost < tilesData[current].fCost) || (tilesData[openTiles[i]].fCost == tilesData[current].fCost && tilesData[openTiles[i]].hCost < tilesData[current].hCost))
                {
                    current = openTiles[i];
                }
            }

            openTiles.Remove(current);
            closedTiles.Add(current);

            if (current == end)
            {
                pathFound = true;
                break;
            }

            foreach (Node neighborNode in tilesData[current].neighborNodes)
            {
                int neighbor = neighborNode.index;
                if (!tilesData[neighbor].isWalkable || closedTiles.Contains(neighbor))
                {
                    continue;
                }

                //If the new path to neighbour is shorter or if the neighbour is not in open
                //Set a new f cost
                //Set parent of neighbour to current
                //if neighbour is not in oopen add it 

                int newMovementCostToNeighbor = tilesData[current].gCost + GetDistance(current, neighbor);

                if (newMovementCostToNeighbor < tilesData[neighbor].gCost || !openTiles.Contains(neighbor))
                {
                    int newHCost = GetDistance(neighbor, end);
                    tilesData[neighbor].hCost = newHCost;
                    tilesData[neighbor].gCost = newMovementCostToNeighbor;

                    //tilesData[neighbor].tileObject.SetPathFinderData(newMovementCostToNeighbor, newHCost);
                    prev[neighbor] = current;

                    //Debug.Log($"{neighbor} : fCost - {tilesData[neighbor].tileObject.GetGridData().fCost}");

                    if (!openTiles.Contains(neighbor))
                    {
                        openTiles.Add(neighbor);
                    }
                }
            }

        }

        int GetDistance(int startIndex, int endIndex)
        {
            Vector2 startCoordinate = tilesData[startIndex].coordinates;
            Vector2 endCoordinate = tilesData[endIndex].coordinates;

            int distX = (int)Mathf.Abs(endCoordinate.x - startCoordinate.x);
            int distY = (int)Mathf.Abs(endCoordinate.y - startCoordinate.y);

            return distX + distY;
        }

        //If no path is found simply return an empty list
        if (!pathFound)
        {
            UnityEngine.Debug.Log("Path not found");
            return new List<Node>();
        }

        //Run a reverse function on prev to get path
        List<int> path = new List<int>();
        for (int i = end; i != start; i = prev[i])
        {
            if (path.Count > maxIndex)
            {
                UnityEngine.Debug.Log($"{start} to {end}");
                break;
            }
            path.Add(i);
        }
        //path.Add(start);
        path.Reverse();

        List<Node> pathTiles = new List<Node>();
        path.ForEach(i =>
        {
            pathTiles.Add(tilesData[i]);
        });

        return pathTiles;
    }

}