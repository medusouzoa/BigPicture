using Runtime.Context.Game.Scripts.Models.InventoryObject;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.Player
{
  public class PlayerView : EventView
  {
    public InventoryObject inventory;

    private void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        Click();
      }
    }

    public void Click()
    {
      dispatcher.Dispatch(PlayerGridEvent.Click);
    }
  }
}