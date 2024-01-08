using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Models.Database;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.CraftRow
{
  public enum CraftRowEvent
  {
    None,
    FirstCraft,
    SecondCraft,
    ThirdCraft,
  }

  public class CraftRowMediator : EventMediator
  {
    [Inject]
    public CraftRowView view { get; set; }

    [Inject]
    public IDatabaseModel dbModel { get; set; }

    public override void OnRegister()
    {
      view.OnCraftItem1Action = () => OnCraftItem(0);
      view.OnCraftItem2Action = () => OnCraftItem(1);
      view.OnCraftItem3Action = () => OnCraftItem(2);
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
        // StartCoroutine(web.SaveItem(view.craftBook.recipes[index].recipeName, 1));
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
    }

    public override void OnRemove()
    {
    }
  }
}