using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _current;
        [SerializeField] private float _max;
        
        public float Current => _current;
        public float Max => _max;

        public event Action HealthChanged;

        public void TakeDamage(float damage)
        {
            _current -= damage;
            _animator.PlayHit();
            
            HealthChanged?.Invoke();
        }
    }
}