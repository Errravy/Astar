// Author   : Muhamad Ravyanto

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Isometric tile Node.
/// Using A* Algorithm.
/// </summary>
public class Node : MonoBehaviour
{
    //==============================================================================
    // Variables
    //==============================================================================
    public static List<Node> nodesInGame = new List<Node>();
    public static List<Node> path = new List<Node>();
    public static Node startNodeCheck;
    public static Node endNodeCheck;

    public int G; // distance from start node to this node
    public int H; // distance from end node to this node
    public int F => G + H; // read only.
    public Node previous; // Previous Node From current this node
    public bool Blocked; // Obstacle at top node
    public Vector2Int tileKey; // Nodes key in NodeGenerator Dictionary 
    private SpriteRenderer sr;
    private Pathfinding pathfinding;
    private bool canBlock;

    [SerializeField] Sprite empty;
    [SerializeField] Sprite walkNode;
    [SerializeField] Sprite startNode;
    [SerializeField] Sprite EndNode;
    [SerializeField] Sprite blockNode;

    public void SetNodeName(string name)
    {
        this.gameObject.name = name;
    }

    private void OnMouseEnter()
    {
        canBlock = true;
    }

    private void OnMouseDown()
    {
        if (startNodeCheck == null)
        {
            startNodeCheck = this;
            sr.sprite = startNode;
            PathClick.instance.SetNode(this);
        }
        else if (endNodeCheck == null)
        {
            endNodeCheck = this;
            sr.sprite = EndNode;
            PathClick.instance.SetNode(this);
        }
    }

    private void OnMouseExit()
    {
        canBlock = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Node node in nodesInGame)
            {
                node.Empty();
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (canBlock)
                Block();
        }
    }

    private void Awake()
    {
        nodesInGame.Add(this);
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = empty;
        pathfinding = new Pathfinding();
    }

    public void Block()
    {
        if (!Blocked)
        {
            sr.sprite = blockNode;
            Blocked = true;
        }
        else
        {
            sr.sprite = empty;
            Blocked = false;
        }
    }

    public void Empty()
    {
        sr.sprite = empty;
        Blocked = false;
        startNodeCheck = null;
        endNodeCheck = null;
    }

    public void WalkNode()
    {
        if (sr.sprite == empty)
            sr.sprite = walkNode;
    }
}

