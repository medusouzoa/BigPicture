using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.MainHud
{
  public class MainHudView : EventView
  {
    public TextMeshProUGUI valueLabel;

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

    public void UpdateValue(int amount)
    {
      valueLabel.text = amount.ToString();
    }
  }
}