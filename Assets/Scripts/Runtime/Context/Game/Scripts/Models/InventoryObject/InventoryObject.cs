using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Models.ItemObjects;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.InventoryObject
{
  [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
  public class InventoryObject : ScriptableObject
  {
    public List<InventorySlot> container = new List<InventorySlot>();

    public void AddItem(ItemObject item, int amount)
    {
      bool hasItem = false;
      for (int i = 0; i < container.Count; i++)
      {
        if (container[i].item == item)
        {
          container[i].AddAmount(amount);
          hasItem = true;
          break;
        }
      }

      if (!hasItem)
      {
        container.Add(new InventorySlot(item, amount));
      }
    }

    public int GetAmountByName(string itemName)
    {
      foreach (InventorySlot slot in container)
      {
        if (slot.item != null && slot.item.itemName == itemName)
        {
          return slot.amount;
        }
      }

      return 0; 
    }

    public bool ContainsItem(ItemObject item)
    {
      for (int i = 0; i < container.Count; i++)
      {
        if (container[i].item == item)
        {
          return true;
        }
      }

      return false;
    }

    public bool ContainsEnoughItem(ItemObject item, int amount)
    {
      for (int i = 0; i < container.Count; i++)
      {
        if (container[i].item == item && container[i].amount >= amount)
        {
          return true;
        }
      }

      return false;
    }

    public void RemoveItem(ItemObject item, int amount)
    {
      for (int i = 0; i < container.Count; i++)
      {
        if (container[i].item == item)
        {
          container[i].RemoveAmount(amount);

          if (container[i].amount <= 0)
          {
            container.RemoveAt(i);
          }

          break;
        }
      }
    }
  }

  [System.Serializable]
  public class InventorySlot
  {
    public ItemObject item;
    public int amount;

    public InventorySlot(ItemObject item, int amount)
    {
      this.item = item;
      this.amount = amount;
    }

    public void AddAmount(int value)
    {
      amount += value;
    }

    public void RemoveAmount(int value)
    {
      amount -= value;
    }
  }
}