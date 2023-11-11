using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.GridTest
{
  public class GridTestView : EventView
  {
    public Camera cam;

    private void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        Click();
      }
    }

    public void Click()
    {
      dispatcher.Dispatch(GridTestEvent.Click);
    }
  }
}