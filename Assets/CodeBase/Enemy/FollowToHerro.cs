using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
    public abstract class FollowToHerro : MonoBehaviour
    {
        private Transform _heroTransform;

        protected Transform Hero => _heroTransform;

        public void Construct(Transform hero)
        {
            _heroTransform = hero;
        }
        
        protected bool HeroIsInit() => 
            _heroTransform != null;
    }
}