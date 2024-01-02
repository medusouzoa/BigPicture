using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.CameraModel
{
  public interface ICameraModel
  {
    void AddCamera(string key, Camera cam);
    Camera GetCameraByKey(string key);
  }
}