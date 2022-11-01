using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GridSystem
{
    public class Coordinate
    {
        Vector2Int coordinate;
        bool traversal;

        public Coordinate(Vector2Int coordinate)
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
    }
}