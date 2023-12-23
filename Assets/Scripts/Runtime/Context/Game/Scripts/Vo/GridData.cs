using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Vo
{
  public class GridData
  {
    private Dictionary<Vector3Int, PlacementData> placeObj = new();

    public void AddObjectAt(Vector3Int gridPosition,
      Vector2Int objectSize,
      int ID,
      int placedObjectIndex)
    {
      List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
      PlacementData data = new PlacementData(positionToOccupy, ID, placedObjectIndex);
      foreach (Vector3Int pos in positionToOccupy)
      {
        if (placeObj.ContainsKey(pos))
          throw new Exception($"Dictionary already contains this cell position {pos}");
        placeObj[pos] = data;
      }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
      List<Vector3Int> returnVal = new();
      for (int x = 0; x < objectSize.x; x++)
      {
        for (int y = 0; y < objectSize.y; y++)
        {
          returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
        }
      }

      return returnVal;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
      List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
      foreach (Vector3Int pos in positionToOccupy)
      {
        if (placeObj.ContainsKey(pos))
          return false;
      }

      return true;
    }

    public class PlacementData
    {
      public List<Vector3Int> occupiedPositions;
      public int ID { get; private set; }
      public int placedObjectIndex { get; private set; }

      public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placedObjectIndex)
      {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
        this.placedObjectIndex = placedObjectIndex;
      }
    }
  }
}