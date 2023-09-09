using CodeBase.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : FollowToHerro
    {
        [SerializeField] private NavMeshAgent _agent;

        private IGameFactory _gameFactory;
        
        private const float MinimalDistance = 1;

        private void Update()
        {
            if (HeroIsInit() && HeroNotReached())
            {
                _agent.destination = Herro.position;
            }
        }

        private bool HeroNotReached() => 
            Vector3.Distance(_agent.transform.position, Herro.position) >= MinimalDistance;
    }
}