using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.MainHud
{
  public class MainHudView : EventView
  {
    public void OnMarketOpen()
    {
      dispatcher.Dispatch(MainHudEvent.Market);
    }

    public void OnInventoryOpen()
    {
      dispatcher.Dispatch(MainHudEvent.Inventory);
    }

    public void OnCraftOpen()
    {
      dispatcher.Dispatch(MainHudEvent.Craft);
    }

    public void OnSettingsOpen()
    {
      dispatcher.Dispatch(MainHudEvent.Settings);
    }
    public void OnClose()
    {
      dispatcher.Dispatch(MainHudEvent.Close);
    }
  }
}