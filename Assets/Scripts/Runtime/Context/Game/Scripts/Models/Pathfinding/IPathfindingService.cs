using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Vo;

namespace Runtime.Context.Game.Scripts.Models.Pathfinding
{
  public interface IPathfindingService
  {
    List<NodeVo> FindPath(int startX, int startY, int endX, int endY);
  }
}