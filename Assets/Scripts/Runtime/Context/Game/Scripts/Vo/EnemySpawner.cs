﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Context.Game.Scripts.Vo
{
  public enum SpawnState
  {
    Spawning,
    Waiting,
    Counting
  }

  public class EnemySpawner : MonoBehaviour
  {
    [SerializeField]
    private WaveVo[] waves;

    [SerializeField]
    private float timeBetweenWaves = 3f;

    [SerializeField]
    private float waveCountdown = 0;

    private SpawnState state = SpawnState.Counting;
    private int _currentWave;

    [SerializeField]
    private List<ZombieStats> enemyList;

    [SerializeField]
    private Transform[] spawners;

    [SerializeField]
    private GameObject zombie;

    private Transform target;


    private void Start()
    {
      waveCountdown = timeBetweenWaves;
      _currentWave = 0;
    }


    private void Update()
    {
      if (state == SpawnState.Waiting)
      {
        if (!EnemiesAreDead())
          return;
        else
        {
          CompleteWave();
        }
      }

      if (waveCountdown <= 0)
      {
        if (state != SpawnState.Spawning)
        {
          StartCoroutine(SpawnWave(waves[_currentWave]));
        }
      }
      else
      {
        waveCountdown -= Time.deltaTime;
      }
    }

    private IEnumerator SpawnWave(WaveVo wave)
    {
      state = SpawnState.Spawning;
      for (int i = 0; i < wave.enemiesAmount; i++)
      {
        SpawnZombie();
        yield return new WaitForSeconds(wave.delay);
      }

      state = SpawnState.Waiting;
      yield break;
    }

    private void SpawnZombie()
    {
      int randomInt = Random.Range(1, spawners.Length);
      Transform randomSpawner = spawners[randomInt];

      GameObject newEnemy = Instantiate(zombie, randomSpawner.position, randomSpawner.rotation);
      ZombieStats newEnemyStats = newEnemy.GetComponent<ZombieStats>();
      // newEnemyStats.SetTarget(target);
      enemyList.Add(newEnemyStats);
      Debug.Log(randomInt);
    }

    private bool EnemiesAreDead()
    {
      int i = 0;
      foreach (ZombieStats enemy in enemyList)
      {
        if (enemy.isDead)
        {
          i++;
        }
        else
        {
          return false;
        }
      }

      return true;
    }

    private void CompleteWave()
    {
      Debug.Log("WaveCompleted");
      state = SpawnState.Counting;
      waveCountdown = timeBetweenWaves;
      if (_currentWave + 1 > waves.Length - 1)
      {
        _currentWave = 0;
        Debug.Log("Completed all the waves");
      }
      else
      {
        _currentWave++;
      }
    }
  }
}