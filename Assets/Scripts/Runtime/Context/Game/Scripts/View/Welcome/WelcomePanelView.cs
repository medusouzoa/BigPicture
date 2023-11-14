using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.Welcome
{
  public class WelcomePanelView : EventView
  {
    public void OnCheckAllDone()
    {
      dispatcher.Dispatch(WelcomePanelEvent.Start);
    }
  }
}