using System;
using Runtime.Context.Game.Scripts.Vo;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Context.Game.Scripts.Models.Bundle
{
  public class Chromozombie : MonoBehaviour
  {
    private NavMeshAgent _agent;
    private Animator _anim;
    private ZombieStats _stats;
    public bool hasReached = false;
    public Transform target;

    public float timeOfLastAttack = 0;

    [SerializeField]
    private float stoppingDistance;

    private void Start()
    {
      GetReferences();
    }

    private void Update()
    {
      MoveToTarget();
    }

    private void MoveToTarget()
    {
      _agent.SetDestination(target.position);
      _anim.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);
      float distanceToTarget = Vector3.Distance(transform.position, target.position);
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
          PlayerStats playerStats = target.GetComponent<PlayerStats>();
          AttackTarget(playerStats);
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
      _anim.SetTrigger("Attack");
      _stats.DealDamage(playerStats);
    }
    public void SetTarget(Transform newTarget)
    {
      target = newTarget;
    }
    private void GetReferences()
    {
      _agent = GetComponent<NavMeshAgent>();
      _anim = GetComponentInChildren<Animator>();
      _stats = GetComponent<ZombieStats>();
    }
  }
}