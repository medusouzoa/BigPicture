using System;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.Bundle
{
  public class UI_Inventory : MonoBehaviour
  {
    private Inventory _inventory;
    private Transform _itemSlotContainer;
    private Transform _itemSlotTemplate;

    private void Awake()
    {
      _itemSlotContainer = transform.Find("itemSlotContainer");
      Debug.Log(_itemSlotContainer);
      _itemSlotTemplate = transform.Find("itemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
      this._inventory = inventory;
      RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
      int x = 0;
      int y = 0;
      float itemSlotCellSize = 30f;
      foreach (Item item in _inventory.GetItemList())
      {
        RectTransform itemSlotRectTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
        itemSlotRectTransform.gameObject.SetActive(true);
        itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
        x++;
        if (x > 4)
        {
          x = 0;
          y++;
        }
      }
    }
  }
}