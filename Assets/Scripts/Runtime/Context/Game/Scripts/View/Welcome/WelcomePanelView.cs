using Runtime.Context.Game.Scripts.Models.InventoryObject;
using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.Welcome
{
  public class WelcomePanelView : EventView
  {
    public InventoryObject inventory;

    public void OnCheckAllDone()
    {
      dispatcher.Dispatch(WelcomePanelEvent.Start);
    }
  }
}