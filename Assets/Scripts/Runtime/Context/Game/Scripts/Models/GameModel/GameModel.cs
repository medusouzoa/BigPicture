﻿using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Enum;
using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Vo;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.GameModel
{
  public class GameModel : IGameModel
  {
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    [SerializeField]
    private UI_Inventory _uiInventory;

    public List<List<TileGridVo>> grids { get; private set; }
    public Inventory inventory { get; set; }
    public int col { get; set; }

    [PostConstruct]
    public void OnPostConstruct()
    {
      grids = new List<List<TileGridVo>>();
      inventory = new Inventory();
      //   _uiInventory.SetInventory(inventory);
    }

    public void Setup(int n)
    {
      col = n;
      dispatcher.Dispatch(GameEvent.CreateGrid);
      FillGridData();
    }

    private void FillGridData()
    {
      for (int i = 0; i < col; i++)
      {
        grids.Add(new List<TileGridVo>());
        for (int j = 0; j < col; j++)
        {
          List<TileGridVo> gridVos = grids[i];
          gridVos.Add(new TileGridVo
          {
            x = i,
            y = j
          });
        }
      }
    }

    public void StartGame()
    {
      dispatcher.Dispatch(GameEvent.GameReady);
    }
  }
}