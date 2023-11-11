using System;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Vo
{
  [Serializable]
  public class GridSettingsVo
  {
    public int width;
    
    public int height;
    
    public float cellSize;

    public Vector3 originPosition;
  }
}