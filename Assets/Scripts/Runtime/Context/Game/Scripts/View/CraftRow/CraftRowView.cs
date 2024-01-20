using System;
using Runtime.Context.Game.Scripts.Models.InventoryObject;
using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.CraftRow
{
  public class CraftRowView : EventView
  {
    public InventoryObject inventory;
    public Models.CraftBookObject.CraftBookObject craftBook;

    public Action OnCraftItem1Action;
    public Action OnCraftItem2Action;
    public Action OnCraftItem3Action;

    public void OnCraftItem1()
    {
      OnCraftItem1Action?.Invoke();
    }

    public void OnCraftItem2()
    {
      OnCraftItem2Action?.Invoke();
    }

    public void OnCraftItem3()
    {
      OnCraftItem3Action?.Invoke();
    }
  }
}