using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Context.Game.Scripts.Enum;
using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Models.GameModel;
using Runtime.Context.Game.Scripts.Models.InventoryModel;
using Runtime.Context.Game.Scripts.Models.ItemObjects;
using Runtime.Context.Game.Scripts.Models.LayerModel;
using Runtime.Context.Game.Scripts.Models.Panel;
using Runtime.Context.Game.Scripts.Vo;
using Runtime.Context.Game.Scripts.Vo.SimpleJSON;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.Welcome
{
  public enum WelcomePanelEvent
  {
    Start,
  }

  public class WelcomePanelMediator : EventMediator
  {
    [Inject]
    public WelcomePanelView view { get; set; }

    [Inject]
    public ILayerModel layerModel { get; set; }

    [Inject]
    public IPanelModel panelModel { get; set; }

    [Inject]
    public IGameModel gameModel { get; set; }

    [Inject]
    public IInventoryModel inventoryModel { get; set; }

    public Action<string> createItemsCallback;
    public Dictionary<ItemObject, int> itemsFromDb;

    public override void OnRegister()
    {
      itemsFromDb = new Dictionary<ItemObject, int>();
      view.dispatcher.AddListener(WelcomePanelEvent.Start, OnCheckAllDone);
      StartCallback();
    }

    public void StartCallback()
    {
      createItemsCallback = (jsonArrayString) =>
      {
        StartCoroutine(inventoryModel.CreateItemsRoutine(jsonArrayString));
        Debug.Log("Mediator bilgisi: " + inventoryModel.itemsArray.Count);
        for (int i = 0; i < inventoryModel.itemsArray.Count; i++)
        {
          JSONClass itemCount = inventoryModel.itemsArray[i].AsObject;
          string itemName = itemCount["name"];
          Debug.Log("OnRegisterCallback, itemName: " + itemName);
          int itemAmount = itemCount["amount"].AsInt;

          ItemObject item = inventoryModel.GetItemByName(itemName);
          Debug.Log("Son eklenen yer çalıştı: " + item.itemName);

          if (!itemsFromDb.ContainsKey(item) || itemsFromDb == null)
          {
            itemsFromDb.Add(item, itemAmount);
          }
          else
          {
            itemsFromDb[item] += itemAmount;
          }
        }

        for (int i = 0; i < itemsFromDb.Count; i++)
        {
          view.inventory.AddItem(itemsFromDb.Keys.ToList()[i], itemsFromDb.Values.ToList()[i]);
        }
      };
      StartCoroutine(web.GetUserItems(createItemsCallback));
    }

    private void OnCheckAllDone()
    {
      Transform parent = layerModel.GetLayer(Layers.InGameLayer);
      Destroy(gameObject);

      parent = layerModel.GetLayer(Layers.Hud);
      panelModel.LoadPanel(GamePanels.InGameHud, parent)
        .Then(() => { gameModel.StartGame(); });
      Vector3 spawnPosition = new Vector3(88, 0, 80);

      // Instantiate the player at the specified position with the default rotation
      GameObject playerInstance = Instantiate(view.player, spawnPosition, Quaternion.identity);
      GameObject spawner = Instantiate(view.spawner, Vector3.zero, Quaternion.identity);
    }
  }
}