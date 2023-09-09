using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
    public abstract class FollowToHerro : MonoBehaviour
    {
        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        protected Transform Herro => _heroTransform;

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
        
        protected bool HeroIsInit() => 
            _heroTransform != null;
        
        private void HeroCreated() => 
            InitHeroTransform();

        private void InitHeroTransform() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;
    }
}