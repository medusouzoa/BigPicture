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
using Runtime.Context.Game.Scripts.Models.PlayerModel;
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

    [Inject]
    public IPlayerModel playerModel { get; set; }

    public Action<string> createItemsCallback;
    public Action<string> createMoneyCallback;
    public Dictionary<ItemObject, int> itemsFromDb;
    public Transform player;
    public PlayerStats pStats;
    public EnemySpawner eSpawner;
    public Transform house;
    public HouseStats hStats;

    public override void OnRegister()
    {
      StartMoneyCallback();
      StartCallback();
      itemsFromDb = new Dictionary<ItemObject, int>();
      view.dispatcher.AddListener(WelcomePanelEvent.Start, OnCheckAllDone);
      Vector3 spawnPosition = new Vector3(88, 0, 80);
      Vector3 spawnHousePosition = new Vector3(100, 0, 114);
      GameObject playerInstance = Instantiate(view.player, spawnPosition, Quaternion.identity);
      GameObject houseInstance = Instantiate(view.house, spawnHousePosition, Quaternion.identity);
      HouseStats houseStats = houseInstance.GetComponent<HouseStats>();
      hStats = houseStats;
      house = houseInstance.transform;
      PlayerStats playerStats = playerInstance.GetComponent<PlayerStats>();
      pStats = playerStats;
      player = playerInstance.transform;
    }


    public void StartCallback()
    {
      createItemsCallback = (jsonArrayString) =>
      {
        StartCoroutine(inventoryModel.CreateItemsRoutine(jsonArrayString));
        for (int i = 0; i < inventoryModel.itemsArray.Count; i++)
        {
          JSONClass itemCount = inventoryModel.itemsArray[i].AsObject;
          string itemName = itemCount["name"];
          int itemAmount = itemCount["amount"].AsInt;

          ItemObject item = inventoryModel.GetItemByName(itemName);

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
          if (itemsFromDb.Values.ToList()[i] != 0)
          {
            view.inventory.AddItem(itemsFromDb.Keys.ToList()[i], itemsFromDb.Values.ToList()[i]);
          }
        }
      };
      StartCoroutine(web.GetUserItems(createItemsCallback));
    }

    public void StartMoneyCallback()
    {
      createMoneyCallback = (arrayString) =>
      {
        string[] parts = arrayString.Split(':');

        if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int amount))
        {
          playerModel.AddMoney(amount);
          Debug.Log("money amount: " + amount);
        }
      };
      StartCoroutine(web.GetMoneyAmountCoroutine(createMoneyCallback));
    }

    private void OnCheckAllDone()
    {
      Transform parent = layerModel.GetLayer(Layers.InGameLayer);
      Destroy(gameObject);

      parent = layerModel.GetLayer(Layers.Hud);
      panelModel.LoadPanel(GamePanels.InGameHud, parent)
        .Then(() => { gameModel.StartGame(); });

      GameObject spawner = Instantiate(view.spawner, Vector3.zero, Quaternion.identity);
      EnemySpawner enemySpawner = spawner.GetComponent<EnemySpawner>();
      eSpawner = enemySpawner;
      enemySpawner.SetTargetSpawner(player);
      enemySpawner.SetHouseTargetSpawner(house);
      pStats.SetSpawner(eSpawner);
      hStats.SetSpawner(eSpawner);
    }
  }
}