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
    public int startX;
    public int startY;
    private bool _isMoving;
    private Transform _target;

    public override void OnRegister()
    {
      view.dispatcher.AddListener(PlayerGridEvent.Click, OnClick);
      _isMoving = false;
      GridSettingsVo settingsVo = new()
      {
        height = 20,
        width = 20,
        cellSize = 10f,
        originPosition = Vector3.zero
      };
      startX = 0;
      startY = 0;
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
        if (_isMoving)
        {
          StopAllCoroutines();
          _isMoving = false;
        }

        startX = nodeVo.x;
        startY = nodeVo.z;

        List<NodeVo> path = pathfindingService.FindPath(startX, startY, nodeVo.x, nodeVo.z);

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
      _isMoving = true;

      foreach (NodeVo node in path)
      {
        Vector3 targetPosition = new Vector3(node.x * 10f + 5f, 0, node.z * 10f + 5f);

        while (Vector3.Distance(view.transform.position, targetPosition) > 0.1f)
        {
          view.transform.position = Vector3.MoveTowards(view.transform.position, targetPosition, Time.deltaTime * moveSpeed);
          yield return null;
        }
      }

      _isMoving = false;
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(PlayerGridEvent.Click, OnClick);
    }

    private void OnApplicationQuit()
    {
      view.inventory.container.Clear();
    }
  }
}