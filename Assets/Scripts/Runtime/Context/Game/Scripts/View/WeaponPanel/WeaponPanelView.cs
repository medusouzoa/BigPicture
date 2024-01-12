using Runtime.Context.Game.Scripts.Models.InventoryObject;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Context.Game.Scripts.View.WeaponPanel
{
  public class WeaponPanelView: EventView
  {
    public InventoryObject inventory;
    public GameObject fistPrefab;

    [SerializeField]
    public Transform buttonContainer;
    public float buttonSpacing;
  }
}