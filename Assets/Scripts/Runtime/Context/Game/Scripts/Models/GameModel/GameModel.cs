using System.Collections.Generic;
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
    public List<List<GridVo>> grids { get; private set; }
    public Inventory inventory { get; set; }

    [PostConstruct]
    public void OnPostConstruct()
    {
      grids = new List<List<GridVo>>();
      inventory = new Inventory();
      _uiInventory.SetInventory(inventory);
    }

    public void Setup()
    {
      dispatcher.Dispatch(GameEvent.CreateGrid);
    }

    public void StartGame()
    {
      dispatcher.Dispatch(GameEvent.GameReady);
    }
  }
}