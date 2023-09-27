using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        
        private IGameFactory _factory;

        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }
        
        private void Start()
        {
            _enemyDeath.Happend += SpawnLoot;
        }

        private void OnDestroy()
        {
            _enemyDeath.Happend -= SpawnLoot;
        }

        private void SpawnLoot()
        {
            GameObject loot = _factory.CreateLoot();
            loot.transform.position = transform.position;
        }
    }
}