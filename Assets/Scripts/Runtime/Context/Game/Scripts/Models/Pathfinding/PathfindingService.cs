using System;
using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Models.Grid;
using Runtime.Context.Game.Scripts.Vo;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.Pathfinding
{
  public class PathfindingService : IPathfindingService
  {
    private Action<List<NodeVo>> _completeCallback;

    private NodeVo _endPosition;

    private List<NodeVo> _foundPath;

    private MonoBehaviour _root;

    private NodeVo _startPosition;

    [Inject]
    public IGridModel gridModel { get; set; }

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private List<NodeVo> _openList;
    private List<NodeVo> _closedList;

    public List<NodeVo> FindPath(int startX, int startY, int endX, int endY)
    {
      NodeVo startNode = gridModel.GetGridObject(startX, startY);
      NodeVo endNode = gridModel.GetGridObject(endX, endY);
      _openList = new List<NodeVo> {startNode};
      _closedList = new List<NodeVo>();
      for (int x = 0; x < gridModel.GetWidth(); x++)
      {
        for (int y = 0; y < gridModel.GetHeight(); y++)
        {
          NodeVo pathNode = gridModel.GetGridObject(x, y);
          pathNode.gCost = int.MaxValue;
          pathNode.cameFromNode = null;
        }
      }

      startNode.gCost = 0;
      startNode.hCost = CalculateDistanceCost(startNode, endNode);

      while (_openList.Count > 0)
      {
        NodeVo currentNode = _openList[0];
        if (currentNode == endNode)
        {
          return CalculatePath(endNode);
        }

        _openList.Remove(currentNode);
        _closedList.Add(currentNode);
        foreach (NodeVo neighbourNode in GetNeighbourList(currentNode))
        {
          if (_closedList.Contains(neighbourNode)) continue;
          int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
          if (tentativeGCost < neighbourNode.gCost)
          {
            neighbourNode.cameFromNode = currentNode;
            neighbourNode.gCost = tentativeGCost;
            neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);


            if (!_openList.Contains(neighbourNode))
            {
              _openList.Add(neighbourNode);
            }
          }
        }
      }

      return null;
    }

    private NodeVo GetLowestFCostNode(List<NodeVo> pathNodeList)
    {
      NodeVo lowestFCostNode = pathNodeList[0];
      for (int i = 1; i < pathNodeList.Count; i++)
      {
        if (pathNodeList[i].fCost < lowestFCostNode.fCost)
        {
          lowestFCostNode = pathNodeList[i];
        }
      }

      return lowestFCostNode;
    }

    private List<NodeVo> CalculatePath(NodeVo endNode)
    {
      List<NodeVo> path = new List<NodeVo>();
      path.Add(endNode);
      NodeVo currentNode = endNode;
      while (currentNode.cameFromNode != null)
      {
        path.Add(currentNode.cameFromNode);
        currentNode = currentNode.cameFromNode;
      }

      path.Reverse();
      return path;
    }

    private int CalculateDistanceCost(NodeVo a, NodeVo b)
    {
      int xDistance = Mathf.Abs(a.x - b.x);
      int yDistance = Mathf.Abs(a.z - b.z);
      int remaining = Mathf.Abs(xDistance - yDistance);
      return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private NodeVo GetNode(int x, int y)
    {
      return gridModel.GetGridObject(x, y);
    }

    private List<NodeVo> GetNeighbourList(NodeVo currentNode)
    {
      List<NodeVo> neighbourList = new List<NodeVo>();


      if (currentNode.x - 1 >= 0)
      {
        neighbourList.Add(GetNode(currentNode.x - 1, currentNode.z));
        if (currentNode.z - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.z - 1));
        if (currentNode.z + 1 < gridModel.GetHeight()) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.z + 1));
      }

      if (currentNode.x + 1 < gridModel.GetWidth())
      {
        neighbourList.Add(GetNode(currentNode.x + 1, currentNode.z));
        if (currentNode.z - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.z - 1));
        if (currentNode.z + 1 < gridModel.GetHeight()) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.z + 1));
      }

      if (currentNode.z - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.z - 1));
      if (currentNode.z + 1 < gridModel.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.z + 1));

      return neighbourList;
    }
  }
}