using System;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUi : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private HeroHealth _heroHealth;

        public void Construct(HeroHealth heroHealth)
        {
            _heroHealth = heroHealth;

            _heroHealth.HealthChanged += UpdateHpBar;
        }

        private void OnDestroy() => 
            _heroHealth.HealthChanged -= UpdateHpBar;

        private void UpdateHpBar() => 
            _hpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
    }
}