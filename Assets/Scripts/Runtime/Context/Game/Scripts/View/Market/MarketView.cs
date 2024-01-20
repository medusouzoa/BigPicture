using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Models.ObjectPlacingObject;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Context.Game.Scripts.View.Market
{
  public class MarketView : EventView
  {
    [SerializeField]
    public Button itemButtonPrefab;

    [SerializeField]
    public Transform buttonContainer;

    public MarketDatabase marketDatabase;

    public float buttonSpacing;

    [SerializeField]
    public List<PlacedObjectType> placedObjectTypeList;


    public void OnClose()
    {
      dispatcher.Dispatch(MarketEvent.Close);
    }
  }
}