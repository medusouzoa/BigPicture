using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Models.InventoryObject;
using Runtime.Context.Game.Scripts.Models.ItemObjects;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

namespace Runtime.Context.Game.Scripts.Vo
{
  public class ZombieStats : MonoBehaviour
  {
    public int health;
    public int maxHealth;
    public bool isDead;
    private NavMeshAgent _agent;
    private Animator _anim;
    private ZombieStats _stats;
    public bool hasReached = false;
    private Transform _target;
    public InventoryObject inventory;
    public float timeOfLastAttack = 0;
    private List<ItemObject> _items;

    [SerializeField]
    private float stoppingDistance;

    [SerializeField]
    private int damage;

    public float attackSpeed;


    private void Update()
    {
      MoveToTarget();
    }

    private void Start()
    {
      InitVariables();
      GetReferences();
    }

    public void DealDamage(PlayerStats playerStats)
    {
      playerStats.TakeDamage(damage);
    }

    public void DealHouseDamage(HouseStats houseStats)
    {
      houseStats.TakeDamage(damage);
    }


    public virtual void CheckHealth()
    {
      if (health <= 0)
      {
        health = 0;
        Die();
        AddItems();
      }

      if (health >= maxHealth)
      {
        health = maxHealth;
      }
    }

    private void AddItems()
    {
      StickObject stickObject = Resources.Load<StickObject>("Stick");
      StoneObject stoneObject = Resources.Load<StoneObject>("Stone");
      _items.Add(stickObject);
      _items.Add(stoneObject);
      int randomIndex = new Random().Next(_items.Count);
      int amount = UnityEngine.Random.Range(0, 9);
      int itemAmount = inventory.GetAmountByName(_items[randomIndex].itemName);
      inventory.AddItem(_items[randomIndex], amount);
      Debug.Log("Items added: " + _items[randomIndex].itemName + " amount: " + amount);
      int result = itemAmount + amount;
      StartCoroutine(web.UpdateAmount(_items[randomIndex].itemName, result));
    }

    private void Die()
    {
      isDead = true;
      Destroy(gameObject);
    }

    private void SetHealth(int newHealth)
    {
      health = newHealth;
      CheckHealth();
    }


    public void TakeDamage(int damage)
    {
      int healthAfterDamage = health - damage;
      SetHealth(healthAfterDamage);
      Debug.Log("Damage given by take damage: " + damage);
    }

    public void Heal(int heal)
    {
      int healthAfterHeal = health + heal;
      SetHealth(healthAfterHeal);
    }


    private void InitVariables()
    {
      maxHealth = 80;
      SetHealth(maxHealth);
      isDead = false;
      damage = 10;
      attackSpeed = 1.5f;
    }

    private void MoveToTarget()
    {
      _agent.SetDestination(_target.position);
      _anim.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);
      float distanceToTarget = Vector3.Distance(transform.position, _target.position);
      _anim.SetFloat("Speed", 0f);
      //Attack
      if (distanceToTarget <= _agent.stoppingDistance)
      {
        if (!hasReached)
        {
          hasReached = true;
          timeOfLastAttack = Time.time;
        }


        if (Time.time >= timeOfLastAttack + _stats.attackSpeed)
        {
          timeOfLastAttack = Time.time;
          PlayerStats playerStats = _target.GetComponent<PlayerStats>();
          if (playerStats != null)
          {
            AttackTarget(playerStats);
          }
          else
          {
            HouseStats houseStats = _target.GetComponent<HouseStats>();
            AttackHouseTarget(houseStats);
          }
        }
      }
      else
      {
        if (hasReached)
        {
          hasReached = false;
        }
      }
    }

    private void AttackTarget(PlayerStats playerStats)
    {
      _stats.DealDamage(playerStats);
    }

    private void AttackHouseTarget(HouseStats houseStats)
    {
      _stats.DealHouseDamage(houseStats);
    }

    public void SetTarget(Transform newTarget)
    {
      _target = newTarget;
    }

    private void GetReferences()
    {
      _agent = GetComponent<NavMeshAgent>();
      _anim = GetComponentInChildren<Animator>();
      _stats = GetComponent<ZombieStats>();
      _items = new List<ItemObject>();
    }
  }
}