using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarController : MonoBehaviour
{

    [SerializeField] int nodeWidth;
    [SerializeField] int nodeHeight;
    [SerializeField] float modeDiameter;
    [SerializeField] Vector3 centerPos;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            aStarPositioner(nodeWidth, nodeHeight, modeDiameter, centerPos);
        }
    }

    public void aStarPositioner(int nodeWidth, int nodeHeight, float modeDiameter, Vector3 centerPos)
    {
        AstarPath aStar = AstarPath.active;
        //Pathfinding.AstarData data = aStar.astarData;
        Pathfinding.GridGraph gridGraph = aStar.data.gridGraph;//data.gridGraph;
        gridGraph.center = centerPos;
        gridGraph.SetDimensions(nodeWidth, nodeHeight, modeDiameter);
        AstarPath.active.Scan();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centerPos, new Vector3(nodeWidth, 0, nodeHeight));
    }
}
