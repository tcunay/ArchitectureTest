using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        [SerializeField] private MonsterTypeId _monsterTypeId;

        [Range(1, 100)]
        [SerializeField] private int _hp;
        
        [Range(1, 30)]
        [SerializeField] private float _damage;
        
        [Range(0.5f, 1)]
        [SerializeField] private float _effectiveDistance = 0.666f;
        
        [Range(0.5f, 1)]
        [SerializeField] private float _cleavage;

        [Range(0.1f, 100)]
        [SerializeField] private float _moveSpeed = 5;
        
        [SerializeField] private GameObject _prefab;
        
        public MonsterTypeId MonsterTypeId => _monsterTypeId;
        public int Hp => _hp;
        public float Damage => _damage;
        public float EffectiveDistance => _effectiveDistance;
        public float Cleavage => _cleavage;
        public float MoveSpeed => _moveSpeed;

        public GameObject Prefab => _prefab;
    }
}