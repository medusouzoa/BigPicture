using Runtime.Context.Game.Scripts.Models.InventoryObject;
using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.CraftRow
{
  public class CraftRowView : EventView
  {
    public InventoryObject inventory;
    public CraftBookObject.CraftBookObject craftBook;

    public void OnCraftItem1()
    {
      dispatcher.Dispatch(CraftRowEvent.FirstCraft);
    }

    public void OnCraftItem2()
    {
      dispatcher.Dispatch(CraftRowEvent.SecondCraft);
    }

    public void OnCraftItem3()
    {
      dispatcher.Dispatch(CraftRowEvent.ThirdCraft);
    }

    public void OnCraftItem4()
    {
      dispatcher.Dispatch(CraftRowEvent.FourthCraft);
    }

    public void OnCraftItem5()
    {
      dispatcher.Dispatch(CraftRowEvent.FifthCraft);
    }
  }
}