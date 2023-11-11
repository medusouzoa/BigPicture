using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.Bundle
{
  public class Inventory
  {
    private List<Item> _itemList;

    public Inventory()
    {
      _itemList = new List<Item>();
      AddItem(new Item {itemType = Item.ItemType.Wrench, amount = 1});
      AddItem(new Item {itemType = Item.ItemType.Stick, amount = 1});
      AddItem(new Item {itemType = Item.ItemType.Stone, amount = 1});
      Debug.Log(_itemList.Count);
    }

    public void AddItem(Item item)
    {
      _itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
      return _itemList;
    }
  }
}