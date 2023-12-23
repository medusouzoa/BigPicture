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
    FourthCraft,
    FifthCraft
  }

  public class CraftRowMediator : EventMediator
  {
    [Inject]
    public CraftRowView view { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(CraftRowEvent.FirstCraft, OnCraftItem1);
      view.dispatcher.AddListener(CraftRowEvent.SecondCraft, OnCraftItem2);
      view.dispatcher.AddListener(CraftRowEvent.ThirdCraft, OnCraftItem3);
      view.dispatcher.AddListener(CraftRowEvent.FourthCraft, OnCraftItem4);
      view.dispatcher.AddListener(CraftRowEvent.FifthCraft, OnCraftItem5);
    }


    public void OnCraftItem1()
    {
      if (view.inventory.ContainsEnoughItem(view.craftBook.recipes[0].ingredients[0].item,
            view.craftBook.recipes[0].ingredients[0].amount)
          && view.inventory.ContainsEnoughItem(view.craftBook.recipes[0].ingredients[1].item,
            view.craftBook.recipes[0].ingredients[1].amount))
      {
        Debug.Log("There are items to craft");
        CraftingNewItem(0);
      }
      else
      {
        Debug.Log("There are missing objects to craft");
      }
    }

    public void OnCraftItem2()
    {
      if (view.inventory.ContainsEnoughItem(view.craftBook.recipes[1].ingredients[0].item,
            view.craftBook.recipes[1].ingredients[0].amount)
          && view.inventory.ContainsEnoughItem(view.craftBook.recipes[1].ingredients[1].item,
            view.craftBook.recipes[1].ingredients[1].amount))
      {
        Debug.Log("There are items to craft");
        CraftingNewItem(1);
      }
      else
      {
        Debug.Log("There are missing objects to craft");
      }
    }

    public void OnCraftItem3()
    {
      if (view.inventory.ContainsEnoughItem(view.craftBook.recipes[2].ingredients[0].item,
            view.craftBook.recipes[2].ingredients[0].amount)
          && view.inventory.ContainsEnoughItem(view.craftBook.recipes[2].ingredients[1].item,
            view.craftBook.recipes[2].ingredients[1].amount))
      {
        Debug.Log("There are items to craft");
      }
      else
      {
        Debug.Log("There are missing objects to craft");
      }
    }

    public void OnCraftItem4()
    {
      if (view.inventory.ContainsEnoughItem(view.craftBook.recipes[3].ingredients[0].item,
            view.craftBook.recipes[3].ingredients[0].amount)
          && view.inventory.ContainsEnoughItem(view.craftBook.recipes[3].ingredients[1].item,
            view.craftBook.recipes[3].ingredients[1].amount))
      {
        Debug.Log("There are items to craft");
      }
      else
      {
        Debug.Log("There are missing objects to craft");
      }
    }

    public void OnCraftItem5()
    {
      if (view.inventory.ContainsEnoughItem(view.craftBook.recipes[4].ingredients[0].item,
            view.craftBook.recipes[4].ingredients[0].amount)
          && view.inventory.ContainsEnoughItem(view.craftBook.recipes[4].ingredients[1].item,
            view.craftBook.recipes[4].ingredients[1].amount))
      {
        Debug.Log("There are items to craft");
        view.inventory.RemoveItem(view.craftBook.recipes[4].ingredients[0].item, view.craftBook.recipes[4].ingredients[0].amount);
        view.inventory.RemoveItem(view.craftBook.recipes[4].ingredients[1].item, view.craftBook.recipes[4].ingredients[1].amount);
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
      view.dispatcher.RemoveListener(CraftRowEvent.FirstCraft, OnCraftItem1);
      view.dispatcher.RemoveListener(CraftRowEvent.SecondCraft, OnCraftItem2);
      view.dispatcher.RemoveListener(CraftRowEvent.ThirdCraft, OnCraftItem3);
      view.dispatcher.RemoveListener(CraftRowEvent.FourthCraft, OnCraftItem4);
      view.dispatcher.RemoveListener(CraftRowEvent.FifthCraft, OnCraftItem5);
    }
  }
}