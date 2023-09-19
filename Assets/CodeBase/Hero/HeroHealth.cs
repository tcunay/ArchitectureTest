using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private HeroAnimator _animator;
        
        private State _state;

        public bool IsDead => Current <= 0;
        
        public Action HealthChanged;

        public float Current
        {
            get => _state.CurrentHealth;
            set
            {
                if (Math.Abs(value - _state.CurrentHealth) > 0.01)
                {
                    _state.CurrentHealth = value;
                    HealthChanged?.Invoke();
                }
                
            }
        }

        public float Max
        {
            get => _state.MaxHealth;
            set => _state.MaxHealth = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHealth = Current;
            progress.HeroState.MaxHealth = Max;
        }

        public void TakeDamage(float damage)
        {
            if(IsDead)
                return;
            
            Current -= damage;
            _animator.PlayHit();
        }
    }
}