using System;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Vo
{
  public class ZombieStats : MonoBehaviour
  {
    public int health;
    public int maxHealth;
    public bool isDead;

    [SerializeField]
    private int damage;

    public float attackSpeed;

    // [SerializeField]
    // private bool canAttack;
    private void Update()
    {
     
    }

    private void Start()
    {
      InitVariables();
    }

    public void DealDamage(PlayerStats playerStats)
    {
      playerStats.TakeDamage(damage);
    }

    public virtual void CheckHealth()
    {
      if (health <= 0)
      {
        health = 0;
        Die();
      }

      if (health >= maxHealth)
      {
        health = maxHealth;
      }
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
    }

    public void Heal(int heal)
    {
      int healthAfterHeal = health + heal;
      SetHealth(healthAfterHeal);
    }


    private void InitVariables()
    {
      maxHealth = 25;
      SetHealth(maxHealth);
      isDead = false;
      damage = 10;
      attackSpeed = 1.5f;
     // canAttack = true;
    }
  }
}