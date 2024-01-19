using System.Collections;
using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Models.ItemObjects;
using Runtime.Context.Game.Scripts.Vo.SimpleJSON;

namespace Runtime.Context.Game.Scripts.Models.InventoryModel
{
  public interface IInventoryModel
  {
    ItemObject GetItemByName(string itemName);
    void AddItemToDatabase(ItemObject item);

    IEnumerator CreateItemsRoutine(string jsonArrayString);
    void AddItemToInventory(string itemName);
    JSONArray itemsArray { get; set; }
    Dictionary<ItemObject, int> items { get; set; }
    JSONArray moneyArray { get; set; }
    void InitializeDatabase();
    IEnumerator GetMoneyRoutine(string jsonArrayString);
  }
}