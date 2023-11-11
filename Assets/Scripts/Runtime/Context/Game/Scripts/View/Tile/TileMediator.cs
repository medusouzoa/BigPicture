using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.Tile
{
  public enum TileItemEvent
  {
    TileSelected
  }

  public class TileMediator : EventMediator
  {
    [Inject]
    private TileView view { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(TileItemEvent.TileSelected, OnGridSelected);
    }

    private void OnGridSelected()
    {
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(TileItemEvent.TileSelected, OnGridSelected);
    }
  }
}