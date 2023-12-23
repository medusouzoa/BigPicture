using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.ObjectPlacingObject
{
  public enum Dir
  {
    None,
    Down,
    Up,
    Left,
    Right,
  }

  [CreateAssetMenu]
  public class PlacedObjectType : ScriptableObject
  {
    public string nameStr;
    public Transform prefab;
    public int width;
    public int height;

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