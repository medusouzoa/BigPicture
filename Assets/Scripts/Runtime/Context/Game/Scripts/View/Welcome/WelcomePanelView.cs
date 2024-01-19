using Runtime.Context.Game.Scripts.Models.InventoryObject;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.Welcome
{
  public class WelcomePanelView : EventView
  {
    public InventoryObject inventory;
    public GameObject player;
    public GameObject spawner;

    [SerializeField]
    public GameObject house;

    public void OnCheckAllDone()
    {
      dispatcher.Dispatch(WelcomePanelEvent.Start);
    }
  }
}