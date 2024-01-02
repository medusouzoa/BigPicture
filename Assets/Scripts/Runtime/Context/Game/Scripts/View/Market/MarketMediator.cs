using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Models.CameraModel;
using Runtime.Context.Game.Scripts.Models.Grid;
using Runtime.Context.Game.Scripts.ObjectPlacingObject;
using Runtime.Context.Game.Scripts.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Context.Game.Scripts.View.Market
{
  public enum MarketEvent
  {
    None,
    Close
  }

  public class MarketMediator : EventMediator
  {
    [Inject]
    public MarketView view { get; set; }

    private List<Button> _itemButtons;
    public PlayerData player;

    private PlacedObjectType currentSelectedType;

    [Inject]
    public IGridModel gridModel { get; set; }

    [Inject]
    public ICameraModel cameraModel { get; set; }

    private PlacedObjectType.Dir _dir = PlacedObjectType.Dir.Down;

    public Camera sceneCamera;

    public override void OnRegister()
    {
      player = new PlayerData();
      player.AddMoney(200f);
      view.dispatcher.AddListener(MarketEvent.Close, OnClose);
      _itemButtons = new List<Button>();
      PopulateMarketButtons();
      sceneCamera = cameraModel.GetCameraByKey("1");
    }

    public void PopulateMarketButtons()
    {
      int buttonsPerRow = 3;
      int currentColumn = 0;
      int currentRow = 0;
      float rowSpacing = -200f;

      foreach (MarketData marketData in view.marketDatabase.marketData)
      {
        Button itemButton = Instantiate(view.itemButtonPrefab, Vector3.zero, Quaternion.identity, view.buttonContainer);
        _itemButtons.Add(itemButton);

        float xPos = -500f + currentColumn * (view.itemButtonPrefab.GetComponent<RectTransform>().rect.width + view.buttonSpacing);
        float yPos = 0 - currentRow * (view.itemButtonPrefab.GetComponent<RectTransform>().rect.height + rowSpacing + view.buttonSpacing);

        RectTransform buttonRect = itemButton.GetComponent<RectTransform>();
        buttonRect.anchoredPosition = new Vector2(xPos, yPos);

        currentColumn++;

        if (currentColumn >= buttonsPerRow)
        {
          currentColumn = 0;
          currentRow++;
        }

        itemButton.onClick.AddListener(() => OnItemButtonClick(marketData));
      }
    }

    public void Update()
    {
      OnBuild();
    }

    public void OnItemButtonClick(MarketData marketData)
    {
      currentSelectedType = marketData.placedObjectType;
      Debug.Log($"Selected PlacedObjectType: {currentSelectedType}");
      Debug.Log("Item clicked: " + marketData.Name);
    }

    public void OnBuild()
    {
      if (Input.GetMouseButtonDown(0))
      {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPoint = sceneCamera.ScreenToWorldPoint(mousePosition);
        NodeVo nodeVo = gridModel.GetNodeByWorldPosition(worldPoint);
        if (nodeVo != null)
        {
          List<Vector2Int> gridPositionList = currentSelectedType.GetGridPositionList(new Vector2Int(nodeVo.x, nodeVo.z), PlacedObjectType.Dir.Down);
          bool canBuild = true;
          foreach (Vector2Int gridPosition in gridPositionList)
          {
            if (!gridModel.GetNodeByWorldPosition(new Vector3(gridPosition.x, 0, gridPosition.y)).CanBuild())
            {
              canBuild = false;
              break;
            }
          }

          if (canBuild)
          {
            Vector2Int rotationOffset = currentSelectedType.GetRotationOffset(_dir);
            Vector3 placedObjWorldPosition = new Vector3(nodeVo.x * 10, 0, nodeVo.z * 10) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * 10f;

            PlacedObject placedObject = PlacedObject.Create(placedObjWorldPosition, new Vector2Int(nodeVo.x, nodeVo.z), _dir, currentSelectedType, player);

            foreach (Vector2Int gridPosition in gridPositionList)
            {
              gridModel.GetNodeByWorldPosition(new Vector3(gridPosition.x, 0, gridPosition.y)).SetPlacedObject(placedObject);
            }

            nodeVo.SetPlacedObject(placedObject);
          }
          else
          {
            Debug.Log("Cannot build here! ");
          }

          if (Input.GetKeyDown(KeyCode.R))
          {
            _dir = PlacedObjectType.GetNextDir(_dir);
            Debug.Log("working");
          }
        }
      }
    }

    private void OnClose()
    {
      Destroy(gameObject);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(MarketEvent.Close, OnClose);
    }
  }
}