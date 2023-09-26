using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : FollowToHerro
    {
        [SerializeField] private NavMeshAgent _agent;
        
        private const float MinimalDistance = 1;

        private void Update()
        {
            if (HeroIsInit() && HeroNotReached())
            {
                _agent.destination = Hero.position;
            }
        }

        private bool HeroNotReached() => 
            Vector3.Distance(_agent.transform.position, Hero.position) >= MinimalDistance;
    }
}