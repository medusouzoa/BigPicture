using Runtime.Context.Game.Scripts.Models.CameraModel;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.SceneCameras
{
  public class CameraMediator : Mediator
  {
    [Inject]
    public CameraView view { get; set; }

    [Inject]
    public ICameraModel cameraModel { get; set; }

    public override void OnRegister()
    {
      string key = view.key;
      Camera sceneCamera = view.sceneCamera;

      cameraModel.AddCamera(key, sceneCamera);
    }

    public override void OnRemove()
    {
    }
  }
}