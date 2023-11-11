using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Vo
{
  [Serializable]
  public class NodeVo
  {
    public int x;

    public int y;

    public int z;

    public int hCost;

    public int gCost;

    public int fCost => gCost + hCost;
    public Vector3 nodePosition => new(x, y, z);

    public Vector3 centerWorldPoint;

    public Vector3 worldPosition;

    public bool isEmpty;

    public string value;

    private bool _status;
    public bool tempStatus { get; set; }

    public NodeVo parentNodeVo;

    public List<NodeVo> neighbours;

    public NodeVo cameFromNode;

    /**
     *  Temporary value always false at the end
     */
    public bool interactive { get; set; }

    public bool check;

    public NodeVo()
    {
    }

    public NodeVo(bool tempStatus)
    {
      this.tempStatus = tempStatus;
      _status = tempStatus;
    }

    public NodeVo(int xValue, int yValue) // height disabled
    {
      x = xValue;
      z = yValue;
    }

    // For easy check
    public override string ToString()
    {
      return "x: " + x + " y: " + y + " z: " + z;
    }

    public bool isWalkable
    {
      get { return _status; }
    }
    public void ResetStatus()
    {
      interactive = false;
      tempStatus = _status;
    }


    /**
      *  Permanent process of making node not walkable
      */
    public void Disable()
    {
      tempStatus = false;
      _status = false;
    }

    public void Enable()
    {
      tempStatus = true;
      _status = true;
    }

    public void CopyFrom(NodeVo nodeVo)
    {
      x = nodeVo.x;
      y = nodeVo.y;
      z = nodeVo.z;
      centerWorldPoint = nodeVo.centerWorldPoint;
      worldPosition = nodeVo.worldPosition;
      isEmpty = nodeVo.isEmpty;
      value = nodeVo.value;
    }
  }
}