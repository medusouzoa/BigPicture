using System;
using System.Collections.Generic;
using RSG;
using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.View.Tile;
using Runtime.Context.Game.Scripts.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.TileManager
{
  public class TileManagerView : EventView
  {
    public void CreateGrids(List<List<TileGridVo>> gridData, BundleFacade bundleFacade)
    {
      List<Func<IPromise>> promises = new();

      // Fill promises with functions that instantiate grids

      for (int i = 0; i < gridData.Count; i++)
      {
        List<TileGridVo> gridVos = gridData[i];
        for (int j = 0; j < gridVos.Count; j++)
        {
          TileGridVo gridVo = gridVos[j];
          promises.Add(() => InstantiateGrid(bundleFacade, gridVo.x, gridVo.y));
        }
      }

      // Debug.LogWarning("PromiseAllBefore>");

      Promise.Sequence(promises)
        .Then(() => Debug.Log("All grids created"))
        .Catch(exception => Debug.LogError(exception));
    }

    private IPromise InstantiateGrid(BundleFacade bundleFacade, int i, int j)
    {
      return bundleFacade.InstantiateAndReturn("Grid", transform)
        .Then(result =>
        {
          result.transform.name = $"grid_{i}_{j}";

          TileView gridView = result.GetComponent<TileView>();
          gridView.x = i;
          gridView.y = j;
        });
    }
  }
}