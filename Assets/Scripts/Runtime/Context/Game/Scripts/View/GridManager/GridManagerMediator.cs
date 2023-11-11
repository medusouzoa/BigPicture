using Runtime.Context.Game.Scripts.Enum;
using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Models.GameModel;
using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.GridManager
{
  public class GridManagerMediator : EventMediator
  {
    [Inject]
    public GridManagerView view { get; set; }

    [Inject]
    public IGameModel gameModel { get; set; }

    [Inject]
    public BundleFacade bundleFacade { get; set; }

    public override void OnRegister()
    {
      dispatcher.AddListener(GameEvent.CreateGrid, OnCreateGrid);
    }

    private void OnCreateGrid()
    {
      view.CreateGrids(gameModel.grids, bundleFacade);
    }

    public override void OnRemove()
    {
      dispatcher.RemoveListener(GameEvent.CreateGrid, OnCreateGrid);
    }
  }
}