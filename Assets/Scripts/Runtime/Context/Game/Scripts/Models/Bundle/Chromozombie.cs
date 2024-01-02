using System;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Context.Game.Scripts.Models.Bundle
{
  public class Chromozombie : MonoBehaviour
  {
    private NavMeshAgent _agent;
    private GameObject _destination;


    public GameObject target;

    private void Start()
    {
      _destination = GameObject.FindGameObjectWithTag("Destination");
      _agent = GetComponent<NavMeshAgent>();
      _agent.SetDestination(_destination.transform.position);
    }

    private void Update()
    {
    }
  }
}