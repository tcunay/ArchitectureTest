using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistantProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private MonsterTypeId _monsterTypeId;
        public string Id { get; private set; }

        [SerializeField] private bool _slain;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        public MonsterTypeId MonsterTypeId => _monsterTypeId;
        public bool Slain => _slain;

        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }

        public void SetId(string id)
        {
            Id = id;
        }

        public void SetMonsterTypeId(MonsterTypeId monsterTypeId)
        {
            _monsterTypeId = monsterTypeId;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
            {
                _slain = true;
            }
            else
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            GameObject monster = _factory.CreateMonster(_monsterTypeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happend += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                _enemyDeath.Happend -= Slay;
            
            _slain = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearedSpawners.Add(Id);
        }
    }
}