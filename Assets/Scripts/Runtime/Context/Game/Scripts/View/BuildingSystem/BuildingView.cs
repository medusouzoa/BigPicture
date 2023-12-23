using System;
using Runtime.Context.Game.Scripts.Models.Grid;
using Runtime.Context.Game.Scripts.ObjectPlacingObject;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.BuildingSystem
{
  public class BuildingView : EventView
  {
    [SerializeField]
    public PlacedObjectType placedObjectType;

    [SerializeField]
    public Camera sceneCamera;
  }
}