using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.Tile
{
  public class TileView : EventView
  {
    public int x;

    public int y;
    
    public void OnTileSelected()
    {
      dispatcher.Dispatch(TileItemEvent.TileSelected);
    }
  }
  
}