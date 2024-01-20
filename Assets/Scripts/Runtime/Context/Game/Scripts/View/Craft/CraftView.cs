using Runtime.Context.Game.Scripts.Models.InventoryObject;
using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.Craft
{
  public class CraftView : EventView
  {
    public InventoryObject inventory;
    public Models.CraftBookObject.CraftBookObject craftBook;

    public void OnClose()
    {
      dispatcher.Dispatch(CraftEvent.Close);
    }
  }
}