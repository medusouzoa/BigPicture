using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Models.DamageModel;
using Runtime.Context.Game.Scripts.Models.ItemObjects;
using Runtime.Context.Game.Scripts.Models.PlayerModel;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Runtime.Context.Game.Scripts.View.WeaponPanel
{
  public class WeaponPanelMediator : EventMediator
  {
    [Inject]
    public WeaponPanelView view { get; set; }

    [Inject]
    public IDamageModel damageModel { get; set; }

    [Inject]
    public IPlayerModel playerModel { get; set; }

    [FormerlySerializedAs("damage")]
    public int fistDamage;

    public int weaponDamage;
    public ItemObject itemObject;

    public override void OnRegister()
    {
      InstantiateWeaponItems();
    }

    void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        if (!IsClickOverUIPanel())
        {
          CloseWeaponPanel();
        }
      }
    }

    private void InstantiateWeaponItems()
    {
      int buttonsPerRow = 4;
      int currentColumn = 0;
      int currentRow = 0;
      float rowSpacing = -200f;

      float lastWeaponButtonWidth = 0f;
      float lastWeaponButtonHeight = 0f;
      GameObject fistButton = Instantiate(view.fistPrefab, Vector3.zero, Quaternion.identity, view.buttonContainer);

      fistButton.GetComponentInChildren<TextMeshProUGUI>().text = "1";
      Button newButton = fistButton.GetComponent<Button>();
      damageModel.damage = 20;
      fistDamage = damageModel.damage;
      newButton.onClick.AddListener(() => OnFistSelected(fistDamage));

      float xPos = -600f + currentColumn * (lastWeaponButtonWidth + view.buttonSpacing);
      float yPos = 0 - currentRow * (lastWeaponButtonHeight + rowSpacing + view.buttonSpacing);

      RectTransform buttonRect = fistButton.GetComponent<RectTransform>();
      buttonRect.anchoredPosition = new Vector2(xPos, yPos);

      for (int i = 0; i < view.inventory.container.Count; i++)
      {
        if (view.inventory.container[i].item.isWeapon)
        {
          GameObject itemButton = Instantiate(view.inventory.container[i].item.weaponButton, Vector3.zero, Quaternion.identity, view.buttonContainer);
          Button aButton = itemButton.GetComponent<Button>();
          weaponDamage = view.inventory.container[i].item.damage;
          itemObject = view.inventory.container[i].item;
          aButton.onClick.AddListener(() => OnWeaponSelected(weaponDamage, itemObject));
          itemButton.GetComponentInChildren<TextMeshProUGUI>().text = view.inventory.container[i].amount.ToString("n0");

          xPos = -200f + currentColumn * (view.inventory.container[i].item.weaponButton.GetComponent<RectTransform>().rect.width + view.buttonSpacing);
          yPos = 0 - currentRow * (view.inventory.container[i].item.weaponButton.GetComponent<RectTransform>().rect.height + rowSpacing + view.buttonSpacing);

          buttonRect = itemButton.GetComponent<RectTransform>();
          buttonRect.anchoredPosition = new Vector2(xPos, yPos);

          currentColumn++;
          if (currentColumn >= buttonsPerRow)
          {
            currentColumn = 0;
            currentRow++;
          }
        }
      }
    }

    private void OnFistSelected(int i)
    {
      damageModel.zombie.TakeDamage(i);
      playerModel.money += 10;
      CloseWeaponPanel();
    }

    private void OnWeaponSelected(int i, ItemObject item)
    {
      damageModel.zombie.TakeDamage(i);
      if (i < 40)
      {
        playerModel.money += 10;
      }

      else if (i > 40 && i < 70)
      {
        playerModel.money += 20;
      }
      else
      {
        playerModel.money += 30;
      }

      CloseWeaponPanel();
      view.inventory.RemoveItem(item, 1);
      StartCoroutine(web.UpdateAmount(item.itemName, view.inventory.GetAmountByName(item.itemName)));
      StartCoroutine(web.UpdateMoneyAmount(0, playerModel.money));
    }

    private bool IsClickOverUIPanel()
    {
      return EventSystem.current.IsPointerOverGameObject();
    }

    private void CloseWeaponPanel()
    {
      Destroy(gameObject);
    }
  }
}