using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GridGenerator : MonoBehaviour
{
    public static GridGenerator Instance;

    public event EventHandler OnGridGenerated;

    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float padding;//Distance between Tiles
    [SerializeField] private Vector2 gridVector; //The row and column number
    [SerializeField] private GameObject startingPosition; //The grid starts here
    [SerializeField] private Transform tilesPlaceholder;

    private List<Node> tilesData = new List<Node>();

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GenerateGrid();
    }
    private void GenerateGrid()
    {
        float sizeOfCell = padding;

        int globalIndex = 0;
        for (int i = 0; i < gridVector.x; i++)
        {
            //Run a loop to add tiles to the instantiateed row
            for (int j = 0; j < gridVector.y; j++)
            {

                int index = globalIndex++;
                //Caluclating the spawn Position based on the value of i and j
                Vector3 spawnPosition = startingPosition.transform.position + new Vector3(-i * sizeOfCell, 0, j * sizeOfCell);

                Node newNode = new Node(new Vector2(i, j), index);

                tilesData.Add(newNode);

            }
        }
        Debug.Log(tilesData[tilesData.Count - 1].coordinates);
        //Spawning a transofrm at the end of grid generation
        Transform lastTransform = Instantiate(tilesPlaceholder, tilesData[tilesData.Count - 1].coordinates, Quaternion.identity);
        lastTransform.parent = transform;


        AddObstacles();

        foreach (Node node in tilesData)
        {
            //Go to each tile and Call the add neighbor function
            //tileWithCordinate.tileObject.SetNeighbors(CalculateNeighbors(tileWithCordinate.cordinate, tileWithCordinate.index));
            node.neighborNodes = CalculateNeighbors(node.coordinates, node.index);
        }

        OnGridGenerated?.Invoke(this, EventArgs.Empty);
    }
    public void SetObjectOnTile(int index, bool isOn)
    {
        tilesData[index].isWalkable = !isOn;
        //Debug.Log($"{index} and {tilesData[index].node.isWalkable}");
    }
    private void AddObstacles()
    {
        //Cycle through each tile and if the coordinate matches with obstacle coordinate add obstacle
        foreach (Node node in tilesData)
        {
            //Check if the node overlaps with out object 2d

            Collider2D[] overlapingColliders = Physics2D.OverlapCircleAll(node.coordinates, 0.1f);
            foreach(Collider2D collider2D in overlapingColliders)
            {
                if(collider2D.gameObject.layer == obstacleMask)
                {
                    node.isWalkable = false;
                    break;
                }
            }
        }
    }
    private List<Node> CalculateNeighbors(Vector2 coordinates, int index)
    {
        //(0, +1);(0, -1); (+5, 0) ; (-5, 0) are the neighbours

        List<Vector2> neighbors = new List<Vector2>();
        List<int> neighborsIndex = new List<int>();
        List<Node> nodes = new List<Node>();
        bool isValidCoordinate(Vector2 toCheckCordinate)
        {
            int maxIndex = (int)gridVector.x * (int)gridVector.y;
            bool isInvalid = toCheckCordinate.x < 0 || toCheckCordinate.y < 0 || toCheckCordinate.x >= gridVector.x || toCheckCordinate.y >= gridVector.y;
            return !isInvalid;
        }

        int[] dx = { 0, 0, +1, -1 };
        int[] dy = { +1, -1, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            Vector2 checkingVector = new Vector2(coordinates.x + dx[i], coordinates.y + dy[i]);


            if (isValidCoordinate(checkingVector))
            {
                neighbors.Add(checkingVector);

                int indexToAdd = (dx[i] == 0) ? dy[i] : dx[i] * (int)gridVector.x;
                neighborsIndex.Add(indexToAdd + index);
                nodes.Add(tilesData[indexToAdd + index]);
            }
        }

        return nodes;
    }
    public int GetMaxIndex() { return (int)gridVector.x * (int)gridVector.y; }
    public List<Node> GetTilesData() { return tilesData; }
}