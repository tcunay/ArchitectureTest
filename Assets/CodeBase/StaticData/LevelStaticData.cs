using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        [SerializeField] private string _levelKey;
        [SerializeField] private List<EnemySpawnerData> _enemySpawners;

        public List<EnemySpawnerData> EnemySpawners => _enemySpawners;
        public string LevelKey => _levelKey;

        public void SetEnemySpawners(List<EnemySpawnerData> spawners)
        {
            _enemySpawners = spawners;
        }

        public void SetLevelKey(string levelKey)
        {
            _levelKey = levelKey;
        }
    }
}