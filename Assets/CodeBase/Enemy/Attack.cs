using System.Linq;
using CodeBase.Hero;
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
        [SerializeField] private float _сleaveage = 0.5f;
        [SerializeField] private float _effectiveDistance = 0.5f;
        [SerializeField] private float _damage = 10;

        private readonly Collider[] _hits = new Collider[1];

        private IGameFactory _factory;
        private Transform _heroTransform;
        private float _currentAttackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private bool _attackIsActive;

        private const string PlayerLayerName = "Player";

        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
            _layerMask = 1 << LayerMask.NameToLayer(PlayerLayerName);
            _factory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        public void EnableAttack() => 
            _attackIsActive = true;

        public void DisableAttack() => 
            _attackIsActive = false;

        private void UpdateCooldown()
        {
            if (CooldownIsUp() == false)
                _currentAttackCooldown -= Time.deltaTime;
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), _сleaveage, 1);
                hit.transform.GetComponent<HeroHealth>().TakeDamage(_damage);
            }
        }

        private void OnAttackEnded()
        {
            _currentAttackCooldown = _attackCooldown;
            _isAttacking = false;
        }

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), _сleaveage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            
            return hitsCount > 0;
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private Vector3 StartPoint() => 
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * _effectiveDistance;

        private bool CanAttack() => 
            _attackIsActive && _isAttacking == false && CooldownIsUp();

        private bool CooldownIsUp() => 
            _currentAttackCooldown < 0;

        private void OnHeroCreated() =>
            _heroTransform = _factory.HeroGameObject.transform;
    }
}