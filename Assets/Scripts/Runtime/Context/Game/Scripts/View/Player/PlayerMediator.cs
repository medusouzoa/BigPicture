using System.Collections;
using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Models.Grid;
using Runtime.Context.Game.Scripts.Models.Pathfinding;
using Runtime.Context.Game.Scripts.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.Player
{
  public enum PlayerGridEvent
  {
    Click
  }

  public class PlayerMediator : EventMediator
  {
    [Inject]
    public PlayerView view { get; set; }

    [Inject]
    public IGridModel gridModel { get; set; }

    [Inject]
    public IPathfindingService pathfindingService { get; set; }

    public float moveSpeed = 20f;

    public override void OnRegister()
    {
      view.dispatcher.AddListener(PlayerGridEvent.Click, OnClick);

      GridSettingsVo settingsVo = new()
      {
        height = 20,
        width = 20,
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
          StartCoroutine(MovePlayerAlongPath(path));
          Debug.Log("PathCount: " + path.Count);
          for (int i = 0; i < path.Count - 1; i++)
          {
            Debug.DrawLine(new Vector3(path[i].x, 0, path[i].z) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, 0, path[i + 1].z) * 10f + Vector3.one * 5f, Color.green, 1f);
          }
        }
      }
    }

    private IEnumerator MovePlayerAlongPath(List<NodeVo> path)
    {
      foreach (NodeVo node in path)
      {
        Vector3 targetPosition = new Vector3(node.x * 10f + 5f, 0, node.z * 10f + 5f); // Adjust based on your grid settings

        while (Vector3.Distance(view.transform.position, targetPosition) > 0.1f)
        {
          view.transform.position = Vector3.MoveTowards(view.transform.position, targetPosition, Time.deltaTime * moveSpeed);
          yield return null;
        }
      }
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(PlayerGridEvent.Click, OnClick);
    }
  }
}