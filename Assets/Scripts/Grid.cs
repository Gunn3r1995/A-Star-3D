using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Grid : MonoBehaviour {
        public LayerMask UnwalkableMask;
        public Vector2 GridWorldSize;
        public float NodeRadius;
        public List<Node> Path;

        Node[,] grid;

        float nodeDiameter;
        int gridSizeX, gridSizeY;

        private void Awake()
        {
            nodeDiameter = NodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(GridWorldSize.x / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(GridWorldSize.y / nodeDiameter);
            CreateGrid();
        }

        private void CreateGrid()
        {
            grid = new Node[gridSizeX, gridSizeY];
            Vector3 worldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;

            for (int x = 0; x < gridSizeX; x++) {
                for (int y = 0; y < gridSizeY; y++){
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + NodeRadius) + Vector3.forward * (y * nodeDiameter + NodeRadius);
                    bool walkable = !(Physics.CheckSphere(worldPoint, NodeRadius, UnwalkableMask));

                    grid[x, y] = new Node(walkable, worldPoint, x, y);
                }
            }
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }
            return neighbours;
        }

        public Node GetNodeFromWorldPoint(Vector3 worldPosistion){
            float percentX = (worldPosistion.x + GridWorldSize.x/2) / GridWorldSize.x;
            float percentY = (worldPosistion.z + GridWorldSize.y/2) / GridWorldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

            return grid[x, y];
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x,1, GridWorldSize.y));

            if(grid != null){
                foreach(Node n in grid) {
                    Gizmos.color = (n.walkable) ? Color.white: Color.red;
                    if (Path != null)
                    {
                        if (Path.Contains(n))
                        {
                            Gizmos.color = Color.black;
                        }
                    }
                    Gizmos.DrawCube(n.WorldPosition, Vector3.one * (nodeDiameter - 0.1f));
                }
            }
        }
    }
}
