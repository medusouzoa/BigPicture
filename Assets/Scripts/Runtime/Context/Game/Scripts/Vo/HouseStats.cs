using Runtime.Context.Game.Scripts.Enum;
using Runtime.Context.Game.Scripts.Models.LayerModel;
using Runtime.Context.Game.Scripts.Models.Panel;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Vo
{
  public class HouseStats : MonoBehaviour
  {
    public int health;
    public int maxHealth;
    public bool isDead;
    public EnemySpawner enemySpawner;
    public GameObject gameOverPanel;

    private void Start()
    {
      InitVariables();
    }

    public void SetSpawner(EnemySpawner spawner)
    {
      enemySpawner = spawner;
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
        if (gameOverPanel != null)
        {
          gameOverPanel.SetActive(true);
        }
      }
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
      maxHealth = 200;
      SetHealth(maxHealth);
      isDead = false;
    }
  }
}