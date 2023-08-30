using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathClick : MonoBehaviour
{
    public static PathClick instance;
    private Node startNode;
    private Node endNode;
    private List<Node> path;
    private Pathfinding pathfinding;

    private void Awake()
    {
        instance = this;
        path = new List<Node>();
        pathfinding = new();
    }

    public void SetNode(Node node)
    {
        if (startNode == null)
        {
            startNode = node;
        }
        else if (endNode == null)
        {
            endNode = node;
            path = pathfinding.FindingPath(startNode, endNode);
            Debug.Log(path.Count);
            foreach (Node nodess in path)
            {
                nodess.WalkNode();
            }
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            path = null;
            startNode = null;
            endNode = null;
        }
    }
}
