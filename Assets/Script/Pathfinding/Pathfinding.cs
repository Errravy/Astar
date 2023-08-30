// Author   : Muhamad Ravyanto

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A* Algorythm.
/// </summary>
public class Pathfinding
{
    //==============================================================================
    // Functions
    //==============================================================================
    #region JenangManis methods
    /// <summary>
    /// searching path for npc.
    /// </summary>
    /// <param name="start">Start Node.</param>
    /// <param name="end">End Node.</param>
    /// <returns>Path for npc to walk</returns>
    public List<Node> FindingPath(Node start, Node end)
    {
        List<Node> search = new List<Node>() { start };
        List<Node> passed = new List<Node>();
        while (search.Count > 0)
        {
            Node currentNode = search[0];
            foreach (Node n in search)
                if (n.F < currentNode.F || n.F == currentNode.F && n.H < currentNode.H) currentNode = n;
            passed.Add(currentNode);
            search.Remove(currentNode);
            if (currentNode == end)
            {
                return GetFinishedPath(start, end);
            }
            List<Node> neighbourNode = Neighbouring(currentNode);
            foreach (Node n in neighbourNode)
            {
                if (n.Blocked || passed.Contains(n))
                {
                    n.WalkNode();
                    continue;
                }
                n.G = Distance(n, start);
                n.H = Distance(n, end);
                n.previous = currentNode;
                if (!search.Contains(n))
                {
                    search.Add(n);
                }
            }
        }
        return new List<Node>();
    }



    /// <summary>
    /// Get Finished Path from end node to start then return reversed path.
    /// </summary>
    /// <param name="start"> Starting Node. </param>
    /// <param name="end"> End Node. </param>
    /// <returns></returns>
    public List<Node> GetFinishedPath(Node start, Node end)
    {
        List<Node> finishedPath = new List<Node>();
        Node current = end;
        while (current != start)
        {
            finishedPath.Add(current);
            current = current.previous;
        }
        finishedPath.Reverse();
        return finishedPath;
    }



    /// <summary>
    /// Distance between 2 Node, current Node and target Node.
    /// </summary>
    /// <param name="currNode"> current Node. </param>
    /// <param name="targetNode"> Target Node. </param>
    /// <returns></returns>
    private int Distance(Node currNode, Node targetNode)
    {
        return Mathf.RoundToInt(Vector2.Distance(currNode.transform.position, targetNode.transform.position) * 10);
    }



    /// <summary>
    /// Search neighbour from current node at Horizontal and Vertical direction.
    /// </summary>
    /// <param name="current"> Current Node. </param>
    /// <returns></returns>
    private List<Node> Neighbouring(Node current)
    {
        List<Node> neigbor = new List<Node>();
        Dictionary<Vector2Int, Node> nodes = NodeGenerator.nodes;
        foreach (Node n in nodes.Values)
        {
            int neighbourDistance = Mathf.RoundToInt(Vector2.Distance(current.transform.position, n.transform.position) * 10);
            if (neigbor.Count >= 4)
            {
                break;
            }
            if (neighbourDistance == 10 && !n.Blocked)
            {
                neigbor.Add(n);
            }

        }
        return neigbor;
    }
    #endregion
}
