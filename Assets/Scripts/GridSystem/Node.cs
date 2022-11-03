using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GridSystem
{
    /// <summary>
    /// Internal class, again this class will not be serialized or anything.
    /// Think of this class like a node, this will keep the info as in 
    /// if the node is traversable and the neighbour nodes
    /// </summary>
    /// 
    [System.Serializable]
    public class Node
    {
        public Vector2Int coordinate;
        public bool isWalkable;
        public bool isExplored;
        public bool isPath;
        public Node connectedTo;
        bool traversal;

        public Node(Vector2Int coordinate)
        {
            this.coordinate = coordinate;
            traversal = false;
        }

        public void setNodeTraverSable()
        {
            traversal = true;
        }

        public bool getTraversalStatus()
        {
            return traversal;
        }

        public Node(Vector2Int coordinate, bool iswalkable)
        {
            this.coordinate = coordinate;
            this.isWalkable = iswalkable;
        }
    }
}