using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Models.Grid;
using Runtime.Context.Game.Scripts.ObjectPlacingObject;
using Runtime.Context.Game.Scripts.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.BuildingSystem
{
  public class BuildingMediator : EventMediator
  {
    [Inject]
    public IGridModel gridModel { get; set; }

    [Inject]
    public BuildingView view { get; set; }

    private void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPoint = view.sceneCamera.ScreenToWorldPoint(mousePosition);
        NodeVo nodeVo = gridModel.GetNodeByWorldPosition(worldPoint);
        List<Vector2Int> gridPositionList = view.placedObjectType.GetGridPositionList(new Vector2Int(nodeVo.x, nodeVo.z), Dir.Down);
        if (nodeVo.CanBuild())
        {
          Transform buildTransform = Instantiate(view.placedObjectType.prefab, new Vector3(nodeVo.x * 10, 0, nodeVo.z * 10), Quaternion.identity);
          foreach (Vector2Int gridPosition in gridPositionList)
          {
            gridModel.GetNodeByWorldPosition(new Vector3(gridPosition.x, 0, gridPosition.y)).SetTransform(buildTransform);
          }

          nodeVo.SetTransform(buildTransform);
        }
        else
        {
          Debug.Log("Cannot build here! ");
        }
      }
    }
  }
}