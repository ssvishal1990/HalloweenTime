using Assets.Scripts.GridSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SIngle ton class
/// This class will be responsible for
///  1) World Position to coordinate conversion
///  2) Coordinate Position to world Position Conversion
///  3) Checking the distance between 2 specified Coordinates
///  4) This will contain the matrix which will be used to decide if a particle block is traverasable or not
///  
/// </summary>
/// 

namespace Assets.Scripts.GridSystem
{
    public class GridSystem : MonoBehaviour
    {
        public static GridSystem Instance { get; private set; }


        [SerializeField] private int snapXEditorSetting;
        [SerializeField] private int snapYEditorSetting;
        [SerializeField] private int mapSizeX;
        [SerializeField] private int mapSizeY;



        Node[,] wayPoints;

        Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
        public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than 1 unit action system  " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
            //InitializeWayPoints();
        }

        private void InitializeWayPoints()
        {
            wayPoints = new Node[mapSizeX + 1, mapSizeY + 1];
            for (int x = 0; x <= mapSizeX; x++)
            {
                for (int y = 0; y <= mapSizeY; y++)
                {
                    wayPoints[x, y] = new Node(new Vector2Int(x, y));
                }
            }
        }

        public Vector2Int worldPositionToCoorDinatePosition(Vector3 worldPosition)
        {
            Vector2Int coorDinatePosition = new Vector2Int();
            coorDinatePosition.x = (int)Mathf.Floor(worldPosition.x / snapXEditorSetting);
            coorDinatePosition.y = (int)Mathf.Floor(worldPosition.z / snapXEditorSetting);
            return coorDinatePosition;
        }

        public Vector3 CoorDinatePositionToWorldPosition(Vector2Int coordinates)
        {
            Vector3 worldPosition = new Vector3();
            worldPosition.x = coordinates.x * snapXEditorSetting;
            worldPosition.z = coordinates.y * snapYEditorSetting;
            return worldPosition;
        }

        public bool isOnDestination(Vector2Int playerCurrentPosition,
                                    Vector2Int playerDestinationPosition)
        {
            if (playerCurrentPosition == playerDestinationPosition)
                return true;
            return false;
        }

        public void SetCoordinateTraversable(int x, int y)
        {
            wayPoints[x, y].setNodeTraverSable();
        }

        public bool getNodeTraversableStatus(Vector2Int coordinate)
        {
            return wayPoints[coordinate.x, coordinate.y].getTraversalStatus();
        }

        public void AddNodeIntoDictionary(Vector2Int coorDinate)
        {
            if (!grid.ContainsKey(coorDinate))
            {
                grid.Add(coorDinate, new Node(coorDinate, true));
            }

        }

        public void ResetNodes()
        {
            foreach (KeyValuePair<Vector2Int, Node> entry in grid)
            {
                entry.Value.connectedTo = null;
                entry.Value.isExplored = false;
                entry.Value.isPath = false;

            }
        }


    }

}
