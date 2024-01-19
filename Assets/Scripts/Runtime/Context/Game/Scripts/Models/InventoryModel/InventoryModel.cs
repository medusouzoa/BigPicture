using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Models.ItemObjects;
using Runtime.Context.Game.Scripts.Vo.SimpleJSON;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.InventoryModel
{
  public class InventoryModel : IInventoryModel
  {
    private static Dictionary<string, ItemObject> itemDictionary;
    public JSONArray itemsArray { get; set; }
    public JSONArray moneyArray { get; set; }
    public Dictionary<ItemObject, int> items { get; set; }

    [PostConstruct]
    public void OnPostConstruct()
    {
      items = new Dictionary<ItemObject, int>();
    }

    public IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
      itemsArray = JSON.Parse(jsonArrayString) as JSONArray;

      for (int i = 0; i < itemsArray.Count; i++)
      {
        JSONClass itemCount = itemsArray[i].AsObject;

        string itemName = itemCount["name"];
        int itemAmount = itemCount["amount"].AsInt;
      }


      yield return null;
    }

    public IEnumerator GetMoneyRoutine(string jsonArrayString)
    {
      moneyArray = JSON.Parse(jsonArrayString) as JSONArray;
      yield return null;
    }


    public void InitializeDatabase()
    {
      itemDictionary = new Dictionary<string, ItemObject>();
      StickObject stickObject = Resources.Load<StickObject>("Stick");
      StoneObject stoneObject = Resources.Load<StoneObject>("Stone");
      AxeObject axeObject = Resources.Load<AxeObject>("Axe");
      HammerObject hammerObject = Resources.Load<HammerObject>("Hammer");
      SwordObject swordObject = Resources.Load<SwordObject>("Sword");
      AddItemToDatabase(stickObject);
      AddItemToDatabase(stoneObject);
      AddItemToDatabase(axeObject);
      AddItemToDatabase(hammerObject);
      AddItemToDatabase(swordObject);
    }

    // Add an item to the database
    public void AddItemToDatabase(ItemObject item)
    {
      if (!itemDictionary.ContainsKey(item.itemName))
      {
        itemDictionary.Add(item.itemName, item);
      }
      else
      {
        Debug.LogWarning("Item with name " + item.itemName + " already exists in the database.");
      }
    }

    // Find an item by itemName
    public ItemObject GetItemByName(string itemName)
    {
      if (itemDictionary.TryGetValue(itemName, out ItemObject item))
      {
        return item;
      }
      else
      {
        Debug.LogWarning("Item with name " + itemName + " not found in the database.");
        return null;
      }
    }

    public void AddItemToInventory(string itemName)
    {
      InventoryObject.InventoryObject inventory = Resources.Load<InventoryObject.InventoryObject>("New Inventory");
      ItemObject item = GetItemByName(itemName);

      if (item != null)
      {
        inventory.AddItem(item, 2);
      }
      else
      {
        Debug.LogWarning("Item with name " + itemName + " not found.");
      }
    }
  }
}