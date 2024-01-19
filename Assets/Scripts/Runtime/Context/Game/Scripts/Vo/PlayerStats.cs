using System;
using Runtime.Context.Game.Scripts.Enum;
using Runtime.Context.Game.Scripts.Models.LayerModel;
using Runtime.Context.Game.Scripts.Models.Panel;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Context.Game.Scripts.Vo
{
  public class PlayerStats : MonoBehaviour
  {
    public int health;
    public int maxHealth;
    public bool isDead;
    public EnemySpawner enemySpawner;
    //public UnityEvent OnPlayerDeath;
    public GameManager gm;
  
    private void Start()
    {
      InitVariables();
      gm = FindObjectOfType<GameManager>();
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
      if (enemySpawner != null)
      {
        Destroy(enemySpawner.gameObject);
        enemySpawner = null;
        gm.OnPlayerDeath();
      }
    }

    public void SetSpawner(EnemySpawner spawner)
    {
      enemySpawner = spawner;
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
      maxHealth = 100;
      SetHealth(maxHealth);
      isDead = false;
    }
  }
}