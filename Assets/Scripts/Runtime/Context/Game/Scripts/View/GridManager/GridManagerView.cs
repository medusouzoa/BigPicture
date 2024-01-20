using System;
using System.Collections.Generic;
using RSG;
using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.GridManager
{
  public class GridManagerView: EventView
  {
    public void CreateGrids(List<List<TileGridVo>> gridData, BundleFacade bundleFacade)
    {
      List<Func<IPromise>> promises = new();

    
      for (int i = 0; i < gridData.Count; i++)
      {
        List<TileGridVo> gridVos = gridData[i];
        for (int j = 0; j < gridVos.Count; j++)
        {
          TileGridVo gridVo = gridVos[j];
          promises.Add(() => InstantiateGrid(bundleFacade, gridVo.x, gridVo.y));
        }
      }


      Promise.Sequence(promises)
        .Then(() => Debug.Log("All grids created"))
        .Catch(exception => Debug.LogError(exception));
    }

    private IPromise InstantiateGrid(BundleFacade bundleFacade, int i, int j)
    {
      return bundleFacade.InstantiateAndReturn("Tile", transform)
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
