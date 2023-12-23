using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Context.Game.Scripts.Vo
{
  public class InputManager : MonoBehaviour
  {
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 _lastPosition;

    [SerializeField]
    private LayerMask placementLayermask;
      
    
    public bool IsPointerOverUI()
      => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetSelectedMapPosition()
    {
      Vector3 mousePos = Input.mousePosition;
      mousePos.z = sceneCamera.nearClipPlane;
      Ray ray = sceneCamera.ScreenPointToRay(mousePos);
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit, 100, placementLayermask))
      {
        _lastPosition = hit.point;
      }

      return _lastPosition;
    }
  }
}