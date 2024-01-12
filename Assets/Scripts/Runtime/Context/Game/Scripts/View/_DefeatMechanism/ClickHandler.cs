using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Context.Game.Scripts.View._DefeatMechanism
{
  public class ClickHandler
  {
    private static readonly PointerEventData EventDataCurrentPosition = new(EventSystem.current);
    private static List<RaycastResult> _results = new();

    // public static bool Check(Vector2 pointPosition)
    // {
    //   // Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
    //   // the ray cast appears to require only eventData.position.
    //   EventDataCurrentPosition.position = new Vector2(pointPosition.x, pointPosition.y);
    //   _results.Clear();
    //   EventSystem.current.RaycastAll(EventDataCurrentPosition, _results);
    //   _results = _results.Where(result => result.gameObject.layer != InteractionSettings.ClickableLayer).ToList();
    //
    //   return _results.Count > 0;
    // }
  }
}