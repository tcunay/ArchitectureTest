using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUi : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;

            _health.HealthChanged += UpdateHpBar;
        }

        private void OnDestroy()
        {
            if(_health == null)
                return;
            
            _health.HealthChanged -= UpdateHpBar;
        }

        private void UpdateHpBar() => 
            _hpBar.SetValue(_health.Current, _health.Max);
    }
}