using System;
using UnityEngine;

namespace CodeBase.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        [SerializeField] private string _id;
        [SerializeField] private MonsterTypeId _monsterTypeId;
        [SerializeField] private Vector3 _position;

        public string ID => _id;
        public MonsterTypeId MonsterTypeId => _monsterTypeId;
        public Vector3 Position => _position;
    }
}