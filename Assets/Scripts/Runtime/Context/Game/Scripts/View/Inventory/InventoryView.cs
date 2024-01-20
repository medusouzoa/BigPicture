using Runtime.Context.Game.Scripts.Models.InventoryModel;
using Runtime.Context.Game.Scripts.Models.InventoryObject;
using Runtime.Context.Game.Scripts.Models.ItemObjects;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.Inventory
{
  public class InventoryView : EventView
  {
    [Inject]
    public IInventoryModel inventoryModel { get; set; }

    public int xStart;
    public int yStart;
    public InventoryObject inventory;
    public int numberOfColumn;
    public int xSpaceBetween;
    public int ySpaceBetween;
    public ItemObject[] items;
    public ItemObject item;

    public Vector3 GetPosition(int i)
    {
      return new Vector3(xStart + (xSpaceBetween * (i % numberOfColumn)), yStart + (-ySpaceBetween * (i / numberOfColumn)), 0f);
    }

    public void OnClose()
    {
      dispatcher.Dispatch(InventoryEvent.Close);
    }
  }
}