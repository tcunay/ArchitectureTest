using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _attackCooldown = 3f;
        [SerializeField] private float _сleaveage = 1f;

        private IGameFactory _factory;
        private Transform _heroTransform;
        private float _currentAttackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];

        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
            _layerMask = 1 << LayerMask.NameToLayer("Player");
            _factory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void UpdateCooldown()
        {
            if (CooldownIsUp() == false)
                _currentAttackCooldown -= Time.deltaTime;
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                
            }
        }

        private bool Hit(out Collider hit)
        {
            Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            Physics.OverlapSphereNonAlloc(startPoint, _сleaveage, _hits, _layerMask);
        }

        private void OnAttackEnded()
        {
            _currentAttackCooldown = _attackCooldown;
            _isAttacking = false;
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private bool CanAttack() => 
            _isAttacking == false && CooldownIsUp();

        private bool CooldownIsUp() => 
            _currentAttackCooldown < 0;

        private void OnHeroCreated() =>
            _heroTransform = _factory.HeroGameObject.transform;
    }
}