using Assets.Scripts.GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Pathfinder
{
    public class PathFinder : MonoBehaviour
    {

        [SerializeField] Vector2Int startCoorDinates;
        public Vector2Int StartCoorDinates {  get { return startCoorDinates; } }

        [SerializeField] Vector2Int endCoorDinates;
        public Vector2Int EndCoorDinates { get { return endCoorDinates; } }

        [SerializeField] List<Node> path;



        Queue<Node> frontier = new Queue<Node>();
        Dictionary<Vector2Int, Node> reachedDictionary = new Dictionary<Vector2Int, Node>();

        Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
        Node startNode;
        Node endNode;
        Node currentSearchNode;

        Dictionary<Vector2Int, Node> grid;


        void Start()
        {
            grid = GridSystem.GridSystem.Instance.Grid;

            InitializeStartAndEndNodes();
            BreadthFirstSearch(startCoorDinates);
            buildPath();

        }

        private void InitializeStartAndEndNodes()
        {
            if (grid.ContainsKey(startCoorDinates))
            {
                Debug.Log("Grid Contained start Node");
                startNode = grid[startCoorDinates];
            }
            if (grid.ContainsKey(endCoorDinates))
            {
                Debug.Log("Grid Contained End Node");
                endNode = grid[endCoorDinates];
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }


        void BreadthFirstSearch(Vector2Int coordinates)
        {
            startNode.isWalkable = true;
            endNode.isWalkable = true;

            frontier.Clear();
            reachedDictionary.Clear();


            bool isRunning = true;
            frontier.Enqueue(grid[coordinates]);
            reachedDictionary.Add(coordinates, grid[coordinates]);

            while (frontier.Count > 0 && isRunning)
            {
                currentSearchNode = frontier.Dequeue();
                currentSearchNode.isExplored = true;
                ExploreNeighbors();
                if (currentSearchNode.coordinate == endCoorDinates)
                {
                    isRunning = false;
                }
            }
        }

        private void ExploreNeighbors()
        {
            List<Node> neighbors  = new List<Node>();

            foreach (Vector2Int direction in directions)
            {
                Vector2Int potentialNeighborNode = currentSearchNode.coordinate + direction;

                if (grid.ContainsKey(potentialNeighborNode))
                {
                    neighbors.Add(grid[potentialNeighborNode]);
                }
            }

            foreach (Node neighbor in neighbors)
            {
                if (!reachedDictionary.ContainsKey(neighbor.coordinate) && neighbor.isWalkable)
                {
                    neighbor.connectedTo = currentSearchNode;
                    reachedDictionary.Add(neighbor.coordinate, neighbor);
                    frontier.Enqueue(neighbor);
                }
            }
        }


        List<Node> buildPath()
        {
            path = new List<Node>();
            Node currentNode = endNode;

            path.Add(currentNode);
            currentNode.isPath = true;
            while (currentNode.connectedTo != null)
            {
                currentNode = currentNode.connectedTo;
                path.Add(currentNode);
                currentNode.isPath = true;
            }
            path.Reverse();
            return path;
        }

        public bool willBlockPath(Vector2Int coordinates)
        {
            if (grid.ContainsKey(coordinates))
            {
                bool previousState = grid[coordinates].isWalkable;
                grid[coordinates].isWalkable = false;
                List<Node> newPath = GetNewPath();
                grid[coordinates].isWalkable = previousState;

                if (newPath.Count <= 1)
                {
                    GetNewPath();
                    return true;
                }
            }
            return false;
        }

        public List<Node> GetNewPath()
        {
            GridSystem.GridSystem.Instance.ResetNodes();
            BreadthFirstSearch(startCoorDinates);
            return buildPath();
        }

        public List<Node> GetNewPath(Vector2Int Coordinates)
        {
            GridSystem.GridSystem.Instance.ResetNodes();
            BreadthFirstSearch(Coordinates);
            return buildPath();
        }

        public void NotifyReceivers()
        {
            BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
        }

        public void setStartCoordinate(Vector2Int startCoorDinates)
        {
            this.startCoorDinates = startCoorDinates;
            startNode = grid[startCoorDinates];
        }
        public void setEndCoordinate(Vector2Int endCoorDinates)
        {
            this.endCoorDinates = endCoorDinates;
            endNode = grid[endCoorDinates];
        }
    }
}