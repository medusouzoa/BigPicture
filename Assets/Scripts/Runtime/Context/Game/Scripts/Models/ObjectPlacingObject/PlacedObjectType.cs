﻿using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Vo;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.ObjectPlacingObject
{
  [CreateAssetMenu]
  public class PlacedObjectType : ScriptableObject
  {
    public string nameStr;
    public GameObject prefab;
    public int width;
    public int height;
    public List<PlaceBuildObjectVo> placeBuildObjectVos;
    public int price;

    public int GetRotationAngle(Dir dir)
    {
      switch (dir)
      {
        default:
        case Dir.Down: return 0;
        case Dir.Left: return 90;
        case Dir.Up: return 180;
        case Dir.Right: return 270;
      }
    }

    public Vector2Int GetRotationOffset(Dir dir)
    {
      switch (dir)
      {
        default:
        case Dir.Down: return new Vector2Int(0, 0);
        case Dir.Left: return new Vector2Int(0, width);
        case Dir.Up: return new Vector2Int(width, height);
        case Dir.Right: return new Vector2Int(height, 0);
      }
    }

    public static Dir GetNextDir(Dir dir)
    {
      switch (dir)
      {
        default:
        case Dir.Down: return Dir.Left;
        case Dir.Left: return Dir.Up;
        case Dir.Up: return Dir.Down;
        case Dir.Right: return Dir.Down;
      }
    }

    public enum Dir
    {
      None,
      Down,
      Up,
      Left,
      Right,
    }

    public List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir)
    {
      List<Vector2Int> gridPositionList = new List<Vector2Int>();
      switch (dir)
      {
        case Dir.Down:
        case Dir.Up:
          for (int x = 0; x < width; x++)
          {
            for (int y = 0; y < height; y++)
            {
              gridPositionList.Add(offset + new Vector2Int(x, y));
            }
          }

          break;
        case Dir.Left:
        case Dir.Right:
          for (int x = 0; x < height; x++)
          {
            for (int y = 0; y < width; y++)
            {
              gridPositionList.Add(offset + new Vector2Int(x, y));
            }
          }

          break;
      }

      return gridPositionList;
    }
  }
}