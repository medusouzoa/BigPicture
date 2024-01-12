using Runtime.Context.Game.Scripts.Models.DamageModel;
using Runtime.Context.Game.Scripts.Models.ItemObjects;
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
      Debug.Log("Damage is assigned: " + fistDamage);
      newButton.onClick.AddListener(() => OnFistSelected(fistDamage));

      float xPos = -600f + currentColumn * (lastWeaponButtonWidth + view.buttonSpacing);
      float yPos = 0 - currentRow * (lastWeaponButtonHeight + rowSpacing + view.buttonSpacing);

      RectTransform buttonRect = fistButton.GetComponent<RectTransform>();
      buttonRect.anchoredPosition = new Vector2(xPos, yPos);

      for (int i = 0; i < view.inventory.container.Count; i++)
      {
        if (view.inventory.container[i].item.isWeapon)
        {
          Debug.Log("the item is weapon: " + view.inventory.container[i].item.itemName);
          GameObject itemButton = Instantiate(view.inventory.container[i].item.weaponButton, Vector3.zero, Quaternion.identity, view.buttonContainer);
          Button aButton = itemButton.GetComponent<Button>();
          weaponDamage = view.inventory.container[i].item.damage;
          itemObject = view.inventory.container[i].item;
          Debug.Log("Damage is assigned: " + weaponDamage);
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
      CloseWeaponPanel();
    }

    private void OnWeaponSelected(int i, ItemObject item)
    {
      damageModel.zombie.TakeDamage(i);
      CloseWeaponPanel();
      view.inventory.RemoveItem(item, i);
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