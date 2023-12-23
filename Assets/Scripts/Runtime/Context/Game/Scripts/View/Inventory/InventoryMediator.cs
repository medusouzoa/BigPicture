using System;
using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Models.InventoryModel;
using Runtime.Context.Game.Scripts.Models.ItemObjects;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.Inventory
{
  public enum InventoryEvent
  {
    None,
    Close
  }

  public class InventoryMediator : EventMediator
  {
    [Inject]
    public InventoryView view { get; set; }

    [Inject]
    public IInventoryModel inventoryModel { get; set; }

    public Action<string> createItemsCallback;
    public Dictionary<ItemObject, int> itemsFromDb;
    public bool isLoadInventory;

    public override void OnRegister()
    {
      view.dispatcher.AddListener(InventoryEvent.Close, OnClose);
      isLoadInventory = false;
      itemsFromDb = new Dictionary<ItemObject, int>();
      CreateDisplay();
    }


    public void CreateDisplay()
    {
      for (int i = 0; i < view.inventory.container.Count; i++)
      {
        GameObject obj = Instantiate(view.inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = view.GetPosition(i);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = view.inventory.container[i].amount.ToString("n0");
      }
    }

    private void OnClose()
    {
      Destroy(gameObject);
      Debug.Log("Close action called");
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(InventoryEvent.Close, OnClose);
    }
  }
}