using Runtime.Context.Game.Scripts.Models.GameModel;
using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.Game
{
  public class GamePanelMediator : EventMediator
  {
    [Inject]
    public GamePanelView view { get; set; }
    
    [Inject]
    public IGameModel gameModel { get; set; }

    public override void OnRegister()
    {
      gameModel.Setup(view.n);
    }

    public override void OnRemove()
    {
    }
  }
}