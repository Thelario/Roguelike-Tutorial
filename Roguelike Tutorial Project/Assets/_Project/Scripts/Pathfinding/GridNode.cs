using UnityEngine;
using System.Collections.Generic;

namespace Game.Pathfinding
{
    public class GridNode : MonoBehaviour
    {
        private bool _displayGridGizmos;
        public LayerMask _unwalkableMask;
        public Vector2 _gridWorldSize;
        public float _nodeRadius;
        private float _nodeDiameter;
        private int _gridSizeX, _gridSizeY;

        public Node[,] grid;
        public Transform roomCenter;

        public int MaxSize { get => _gridSizeX * _gridSizeY; }

        private void Start()
        {
            InitializeGrid(true, _unwalkableMask, _gridWorldSize, _nodeRadius, roomCenter);
        }

        public void InitializeGrid(bool displayGridGizmos, LayerMask unwalkableMask, Vector2 gridWorldSize, float nodeRadius, Transform roomCenter)
        {
            _displayGridGizmos = displayGridGizmos;
            _unwalkableMask = unwalkableMask;
            _gridWorldSize = gridWorldSize;
            _nodeRadius = nodeRadius;
            this.roomCenter = roomCenter;

            _nodeDiameter = _nodeRadius * 2f;
            _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
            _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);

            CreateGrid();
        }

        public void CreateGrid()
        {
            grid = new Node[_gridSizeX, _gridSizeY];

            Vector3 worldBottomLeft = roomCenter.position - Vector3.right * _gridWorldSize.x / 2 - Vector3.up * _gridWorldSize.y / 2;

            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + _nodeRadius) + Vector3.up * (y * _nodeDiameter + _nodeRadius);
                    bool walkable = !(Physics2D.CircleCast(worldPoint, _nodeRadius, Vector2.zero, 0f, _unwalkableMask));
                    grid[x, y] = new Node(walkable, worldPoint, x, y);
                }
            }
        }

        public List<Node> GetNeighbours(Node node)
        {
            return null;
        }

        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            return null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(roomCenter.position, new Vector3(_gridWorldSize.x, _gridWorldSize.y, 1));

            if (grid != null && _displayGridGizmos)
            {
                foreach (Node node in grid)
                {
                    Gizmos.color = (node.walkable) ? Color.white : Color.red;
                    Gizmos.DrawCube(node.worldPosition, Vector3.one * (_nodeDiameter - .1f));
                    Gizmos.color = Color.green;
                }
            }
        }
    }
}
