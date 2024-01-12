using Runtime.Context.Game.Scripts.Enum;
using Runtime.Context.Game.Scripts.Models.CameraModel;
using Runtime.Context.Game.Scripts.Models.DamageModel;
using Runtime.Context.Game.Scripts.Models.GameModel;
using Runtime.Context.Game.Scripts.Models.LayerModel;
using Runtime.Context.Game.Scripts.Models.Panel;
using Runtime.Context.Game.Scripts.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.Chromozombie
{
  public enum ZombieEvent
  {
    None,
    Menu
  }

  public class ChromozombieMediator : EventMediator
  {
    [Inject]
    public ChromozombieView view { get; set; }

    [Inject]
    public ICameraModel cameraModel { get; set; }

    [Inject]
    public ILayerModel layerModel { get; set; }

    [Inject]
    public IPanelModel panelModel { get; set; }

    [Inject]
    public IDamageModel damageModel { get; set; }
    [Inject]
    public IGameModel gameModel { get; set; }


    public Camera cam;

    public override void OnRegister()
    {
      cam = cameraModel.GetCameraByKey("1");
      view.dispatcher.AddListener(ZombieEvent.Menu, OnMenuOpen);
      Debug.Log("EnemySpawn"+gameModel.enemySpawn);
    }


    private void OnMenuOpen()
    {
      Transform parent = layerModel.GetLayer(Layers.Hud);
      panelModel.LoadPanel(GamePanels.WeaponPanel, parent);
    }

    void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, view.zombieLayer))
        {
          ZombieStats zombie = hit.collider.GetComponent<ZombieStats>();

          if (zombie != null)
          {
            Debug.Log(zombie.health);
            Debug.Log("zombie clicked");
            damageModel.zombie = zombie;
            OnMenuOpen();
          }
        }
      }
    }
  }
}