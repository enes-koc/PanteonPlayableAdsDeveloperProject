using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphPenalty : MonoBehaviour
{
    Pathfinding.GraphNode currentNode;
    Pathfinding.GraphNode prevNode;
    List<Pathfinding.GraphNode> PointNodeList = new List<Pathfinding.GraphNode>();

    private void Start()
    {
        prevNode = AstarPath.active.GetNearest(transform.position).node;
    }

    private void FixedUpdate()
    {
        currentNode = AstarPath.active.GetNearest(transform.position).node;

        if (currentNode.NodeIndex != prevNode.NodeIndex)
        {
            foreach (var node in PointNodeList)
            {
                node.Penalty = 0;
            }
            PointNodeList.Clear();

            PointNodeList.Add(currentNode);
            currentNode.GetConnections(PointNodeList.Add);
            foreach (var node in PointNodeList)
            {
                node.Penalty = 13000;
            }
        }

        prevNode = currentNode;
    }

    private void OnDisable()
    {
        foreach (var node in PointNodeList)
        {
            node.Penalty = 0;
        }
    }
}
