using System;
using System.Collections.Generic;
using RSG;
using Runtime.Context.Game.Scripts.Models.Bundle;
//using Runtime.Context.Game.Scripts.View.Grid;
using Runtime.Context.Game.Scripts.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.GridManager
{
  public class GridManagerView: EventView
  {
    public void CreateGrids(List<List<GridVo>> gridData, BundleFacade bundleFacade)
    {
      List<Func<IPromise>> promises = new();

      // Fill promises with functions that instantiate grids
    
      for (int i = 0; i < gridData.Count; i++)
      {
        List<GridVo> gridVos = gridData[i];
        for (int j = 0; j < gridVos.Count; j++)
        {
          GridVo gridVo = gridVos[j];
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
      return bundleFacade.InstantiateAndReturn("ItemSlot", transform)
        .Then(result =>
        {
          result.transform.name = $"grid_{i}_{j}";
          
          /*GridView gridView = result.GetComponent<GridView>();
          gridView.x = i;
          gridView.y = j;*/
        });
    }
  }
  }
