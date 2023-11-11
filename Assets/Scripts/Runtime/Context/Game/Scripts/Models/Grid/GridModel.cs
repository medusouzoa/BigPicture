using System;
using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Enum;
using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Vo;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Context.Game.Scripts.Models.Grid
{
  public class GridModel : IGridModel
  {
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    private GridSettingsVo _currentSettings;

    private Vector2Int _invalidGridPoint;

    private NodeVo[,] _gridArray;

    [PostConstruct]
    public void OnPostConstruct()
    {
      _invalidGridPoint = Vector2Int.down * 99999;
    }

    public void Create(GridSettingsVo vo)
    {
      _currentSettings = vo;

      int width = _currentSettings.width;
      int height = _currentSettings.height;

      _gridArray = new NodeVo[width, height];

      FillGrid();
      GridDebug(width, height, _currentSettings.cellSize);

      // Debug.Log("GridModel.Create> DataCreated");
      dispatcher.Dispatch(GridEvent.DataCreated);
    }

    private void FillGrid()
    {
      for (int x = 0; x < _gridArray.GetLength(0); x++)
      {
        for (int y = 0; y < _gridArray.GetLength(1); y++)
        {
          NodeVo nodeVo = CreateEmptyNodeVo(x, y);
          _gridArray[x, y] = nodeVo;
        }
      }
    }
    public NodeVo GetGridObject(int x, int y)
    {
      if (x >= 0 && y >= 0 && x < _currentSettings.width && y < _currentSettings.height)
      {
        return _gridArray[x, y];
      }
      else
      {
        return default(NodeVo);
      }
    }
    private NodeVo CreateEmptyNodeVo(int x, int y)
    {
      NodeVo nodeVo = new(x, y)
      {
        worldPosition = GetWorldPosition(x, y),
        centerWorldPoint = GetWorldToCenterPoint(GetWorldPosition(x, y)),
        isEmpty = true,
        value = x + ", " + y,
      };
      return nodeVo;
    }

    private Vector3 GetWorldToCenterPoint(Vector3 worldPoint)
    {
      return worldPoint + new Vector3(GetCellSize(), 0, GetCellSize()) * .5f;
    }

    private Vector3 GetCenterToWorldPoint(Vector3 centerPosition)
    {
      return centerPosition - new Vector3(GetCellSize(), 0, GetCellSize()) * .5f;
    }

    private void FillGridWithData(List<NodeVo> gridList)
    {
      for (int x = 0; x < _gridArray.GetLength(0); x++)
      {
        for (int y = 0; y < _gridArray.GetLength(1); y++)
        {
          NodeVo nodeVo = gridList.Find(vo => vo.x == x && vo.z == y);
          if (nodeVo == null)
          {
            nodeVo = CreateEmptyNodeVo(x, y);
          }

          _gridArray[x, y] = nodeVo;
        }
      }

      dispatcher.Dispatch(GridEvent.DataCreated);
    }

    private void GridDebug(int width, int height, float cellSize)
    {
      for (int x = 0; x < _gridArray.GetLength(0); x++)
      {
        for (int y = 0; y < _gridArray.GetLength(1); y++)
        {
          NodeVo dot1NodeVo = _gridArray[x, y];
    
          Debug.DrawLine(dot1NodeVo.worldPosition, GetWorldPosition(x, y + 1), Color.white, 999f);
          Debug.DrawLine(dot1NodeVo.worldPosition, GetWorldPosition(x + 1, y), Color.white, 999f);
        }
      }
    
      Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 999f);
      Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 999f);
    }

    public int GetWidth()
    {
      return _currentSettings.width;
    }

    public int GetHeight()
    {
      return _currentSettings.height;
    }

    public float GetCellSize()
    {
      return _currentSettings.cellSize;
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
      return new Vector3(x, 0, y) * _currentSettings.cellSize + _currentSettings.originPosition;
    }

    public NodeVo GetNodeByCenterPosition(Vector3 centerPosition)
    {
      Vector3 originPosition = _currentSettings.originPosition;
      float cellSize = _currentSettings.cellSize;

      Vector3 worldPosition = GetCenterToWorldPoint(centerPosition);

      int x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
      int y = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);

      return HasGrid(x, y) ? _gridArray[x, y] : null;
    }
    
    public NodeVo GetNodeByWorldPosition(Vector3 worldPosition)
    {
      Vector3 originPosition = _currentSettings.originPosition;
      float cellSize = _currentSettings.cellSize;

      int x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
      int y = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);

      return HasGrid(x, y) ? _gridArray[x, y] : null;
    }

    public void SetValue(NodeVo nodeVo, bool isEmpty)
    {
      Vector3 nodePosition = nodeVo.nodePosition;
      int intX = (int)nodePosition.x;
      int intY = (int)nodePosition.z;

      if (!HasGrid(intX, intY))
      {
        return;
      }

      NodeVo node = _gridArray[intX, intY];
      node.isEmpty = isEmpty;
    }

    public void SetValue(Vector3 worldPosition, bool isEmpty)
    {
      NodeVo nodeVo = GetNodeByCenterPosition(worldPosition);
      SetValue(nodeVo, isEmpty);
    }

    public bool HasGrid(int x, int y)
    {
      return x >= 0 && y >= 0 && x < _currentSettings.width && y < _currentSettings.height;
    }

    public bool IsEmpty(int nodeX, int nodeY)
    {
      return HasGrid(nodeX, nodeY) && _gridArray[nodeX, nodeY].isEmpty;
    }

    public List<NodeVo> GetAllNodes()
    {
      List<NodeVo> resultList = new();

      for (int x = 0; x < _gridArray.GetLength(0); x++)
      {
        for (int y = 0; y < _gridArray.GetLength(1); y++)
        {
          NodeVo nodeVo = _gridArray[x, y];
          resultList.Add(nodeVo);
        }
      }

      return resultList;
    }

    public void AddHeight()
    {
      List<NodeVo> tempList = GetAllNodes();

      _currentSettings.height += 1;
      _gridArray = new NodeVo[_currentSettings.width, _currentSettings.height];

      FillGridWithData(tempList);

      dispatcher.Dispatch(GridEvent.AddHeightSuccess);
    }

    public bool HasEmptyGrid()
    {
      NodeVo firstEmptyNodeVo = GetFirstEmptyNode();
      return firstEmptyNodeVo != null;
    }

    // TODO: refactor this because it's very complex
    public NodeVo GetRandomEmptyPoint()
    {
      NodeVo randomNodeVo = null;

      int runCount = 0;
      const int maxRunCount = 100; // TODO: make it configurable

      do
      {
        if (runCount > maxRunCount)
        {
          break;
        }

        runCount++;
        int randomX = Random.Range(0, _currentSettings.width);
        int randomY = Random.Range(0, _currentSettings.height);

        if (!HasGrid(randomX, randomY))
        {
          continue;
        }

        if (!IsEmpty(randomX, randomY))
        {
          continue;
        }

        randomNodeVo = _gridArray[randomX, randomY];

        break;
      } while (true);

      if (randomNodeVo == null)
      {
        randomNodeVo = GetFirstEmptyNode();
      }

      return randomNodeVo;
    }

    private NodeVo GetFirstEmptyNode()
    {
      for (int x = 0; x < _gridArray.GetLength(0); x++)
      {
        for (int y = 0; y < _gridArray.GetLength(1); y++)
        {
          if (IsEmpty(x, y))
          {
            return _gridArray[x, y];
          }
        }
      }

      // Debug.LogWarning("GetFirstEmptyNode> Could not find first empty node");
      return null;
    }
  }
}