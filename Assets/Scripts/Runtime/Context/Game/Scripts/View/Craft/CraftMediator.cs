using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Models.InventoryModel;
using Runtime.Context.Game.Scripts.Models.ItemObjects;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Context.Game.Scripts.View.Craft
{
  public enum CraftEvent
  {
    None,
    Close
  }

  public class CraftMediator : EventMediator
  {
    [Inject]
    public CraftView view { get; set; }

    [Inject]
    public IInventoryModel inventoryModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(CraftEvent.Close, OnClose);
      CreateDisplay();
    }


    public void CreateDisplay()
    {
      GameObject parent = GameObject.Find("Elements");

      if (parent == null)
      {
        Debug.LogError("Parent GameObject 'Elements' not found.");
        return;
      }

      for (int i = 0; i < view.craftBook.recipes.Count; i++)
      {
        GameObject obj = Instantiate(view.craftBook.recipes[i].prefab, parent.transform);
        TextMeshProUGUI headerText = obj.transform.Find("ItemName")?.GetComponent<TextMeshProUGUI>();
        headerText.text = view.craftBook.recipes[i].recipeName.ToString();
        int recipeIndex = i;
        Button craftButton = obj.GetComponentInChildren<Button>();
        craftButton.onClick.AddListener(() => OnCraftItem(recipeIndex));
        for (int j = 0; j < view.craftBook.recipes[i].ingredients.Count; j++)
        {
          string itemName = "Item" + (j + 1);
          Transform itemTransform = obj.transform.Find(itemName);

          if (itemTransform != null)
          {
            GameObject newItemPrefab = view.craftBook.recipes[i].ingredients[j].item.prefab;
            newItemPrefab.GetComponentInChildren<TextMeshProUGUI>().text = view.craftBook.recipes[i].ingredients[j].amount.ToString("n0");

            Instantiate(newItemPrefab, itemTransform.position, itemTransform.rotation, itemTransform);
          }
          else
          {
            Debug.LogError($"The '{itemName}' GameObject was not found in the instantiated prefab hierarchy.");
          }
        }

        string craftedName = "CraftedItem";
        Transform craftedTransform = obj.transform.Find(craftedName);
        if (craftedTransform != null)
        {
          GameObject newItemPrefab = view.craftBook.recipes[i].result.prefab;
          Instantiate(newItemPrefab, craftedTransform.position, craftedTransform.rotation, craftedTransform);
        }
        else
        {
          Debug.LogError($"The '{craftedName}' GameObject was not found in the instantiated prefab hierarchy.");
        }
      }
    }

    private void OnCraftItem(int index)
    {
      if (view.inventory.ContainsEnoughItem(view.craftBook.recipes[index].ingredients[0].item,
            view.craftBook.recipes[index].ingredients[0].amount)
          && view.inventory.ContainsEnoughItem(view.craftBook.recipes[index].ingredients[1].item,
            view.craftBook.recipes[index].ingredients[1].amount))
      {
        Debug.Log("There are items to craft");
        CraftingNewItem(index);
        Debug.Log(view.craftBook.recipes[index].recipeName);
        ItemObject newItem = inventoryModel.GetItemByName(view.craftBook.recipes[index].recipeName);
        int i = view.inventory.GetAmountByName(newItem.itemName) + 1;
        StartCoroutine(web.UpdateAmount(view.craftBook.recipes[index].recipeName, i));
        view.inventory.AddItem(newItem, 1);
        Debug.Log(view.inventory.GetAmountByName(newItem.itemName));
      }
      else
      {
        Debug.Log("There are missing objects to craft");
      }
    }

    public void CraftingNewItem(int i)
    {
      view.inventory.RemoveItem(view.craftBook.recipes[i].ingredients[0].item, view.craftBook.recipes[i].ingredients[0].amount);
      view.inventory.RemoveItem(view.craftBook.recipes[i].ingredients[1].item, view.craftBook.recipes[i].ingredients[1].amount);
      int item1 = view.inventory.GetAmountByName(view.craftBook.recipes[i].ingredients[0].item.itemName);
      int item2 = view.inventory.GetAmountByName(view.craftBook.recipes[i].ingredients[1].item.itemName);
      Debug.Log("new amount of item1 is: " + item1);
      StartCoroutine(web.UpdateAmount(view.craftBook.recipes[i].ingredients[0].item.itemName, item1));
      StartCoroutine(web.UpdateAmount(view.craftBook.recipes[i].ingredients[1].item.itemName, item2));
    }

    private void OnClose()
    {
      Destroy(gameObject);
      Debug.Log("Close action called");
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(CraftEvent.Close, OnClose);
    }
  }
}