using UnityEngine;

namespace Assets.Scripts
{
    public class Node {
        public bool walkable;
        public Vector3 WorldPosition;
        public int gridX;
        public int gridY;

        public int GCost;
        public int HCost;
        public int FCost
        {
            get { return GCost + HCost; }
        }

        public Node Parent;

        public Node(bool _walkable, Vector3 _worldPosition, int _gridX, int _gridY)
        {
            walkable = _walkable;
            WorldPosition = _worldPosition;
            gridX = _gridX;
            gridY = _gridY;
        }

    }
}
