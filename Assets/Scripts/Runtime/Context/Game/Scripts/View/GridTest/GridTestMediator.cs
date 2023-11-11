using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Models.Grid;
using Runtime.Context.Game.Scripts.Models.Pathfinding;
using Runtime.Context.Game.Scripts.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.GridTest
{
  public enum GridTestEvent
  {
    Click
  }

  public class GridTestMediator : EventMediator
  {
    [Inject]
    public GridTestView view { get; set; }

    [Inject]
    public IGridModel gridModel { get; set; }

    [Inject]
    public IPathfindingService pathfindingService { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(GridTestEvent.Click, OnClick);

      GridSettingsVo settingsVo = new()
      {
        height = 10,
        width = 10,
        cellSize = 10f,
        originPosition = Vector3.zero
      };

      gridModel.Create(settingsVo);
    }

    private void OnClick()
    {
      Vector3 mousePosition = Input.mousePosition;
      Vector3 worldPoint = view.cam.ScreenToWorldPoint(mousePosition);
      NodeVo nodeVo = gridModel.GetNodeByWorldPosition(worldPoint);
      Debug.LogError(nodeVo == null ? "Node is null" : nodeVo.ToString());

      if (nodeVo != null)
      {
        List<NodeVo> path = pathfindingService.FindPath(0, 0, nodeVo.x, nodeVo.z);

        if (path != null)
        {
          Debug.Log("PathCount: " + path.Count);
          for (int i = 0; i < path.Count - 1; i++)
          {
            Debug.DrawLine(new Vector3(path[i].x, 0, path[i].z) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, 0, path[i + 1].z) * 10f + Vector3.one * 5f, Color.green, 1f);
          }
        }
      }
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(GridTestEvent.Click, OnClick);
    }
  }
}