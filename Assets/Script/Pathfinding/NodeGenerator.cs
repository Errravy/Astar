// Author   : Muhamad Ravyanto

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Generating node to tilemap.
/// </summary>
public class NodeGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject node;

    [SerializeField]
    private GameObject nodeParent;
    private int keyIndex;
    public static Dictionary<Vector2Int, Node> nodes;
    public static Dictionary<int, Vector2Int> key;
    void Awake()
    {
        GenerateNode();
    }

    /// <summary>
    /// Generate Node from highest ground to low ground, 
    /// and block node if have obstacle or building at that node.
    /// </summary>
    private void GenerateNode()
    {
        Tilemap[] tileMaps = GetComponentsInChildren<Tilemap>();
        Tilemap walkable = tileMaps[0];
        // Tilemap unwalkable = tileMaps[1];
        nodes = new Dictionary<Vector2Int, Node>();
        key = new Dictionary<int, Vector2Int>();

        BoundsInt bounds = walkable.cellBounds;

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int tileLocation = new Vector3Int(x, y, 0);
                Vector2Int tileKey = new Vector2Int(x, y);
                if (walkable.HasTile(tileLocation))
                {
                    GameObject nodeObject = Instantiate(this.node, nodeParent.transform);
                    Node node = nodeObject.GetComponent<Node>();
                    Vector3 cellWorldPosition = walkable.GetCellCenterWorld(tileLocation);

                    nodeObject.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);

                    nodeObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    nodes.Add(tileKey, node);
                    key.Add(keyIndex, tileKey);

                    keyIndex++;
                    node.tileKey = tileKey;

                    node.SetNodeName(tileKey.ToString());

                }
            }
        }
    }
}
