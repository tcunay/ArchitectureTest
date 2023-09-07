﻿using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        
        private const float MinimalDistance = 1;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null) 
                InitHeroTransform();
            else
            {
                _gameFactory.HeroCreated += HeroCreated;
            }
        }

        private void Update()
        {
            if (HeroIsInit() && HeroNotReached())
            {
                _agent.destination = _heroTransform.position;
            }
        }

        private bool HeroIsInit() => 
            _heroTransform != null;

        private void HeroCreated() => 
            InitHeroTransform();

        private void InitHeroTransform() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool HeroNotReached() => 
            Vector3.Distance(_agent.transform.position, _heroTransform.transform.position) >= MinimalDistance;
    }
}