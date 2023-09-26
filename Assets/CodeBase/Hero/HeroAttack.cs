﻿using System;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistantProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private CharacterController _characterController;
        
        private IInputService _input;

        private static int _layerMask;
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();

            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_input.IsAttackButton() && _animator.isAttacking == false)
            {
                _animator.PlayAttack();
            }
        }

        public void OnAttack()
        {
            PhysicsDebug.DrawDebug(StartPoint(), _stats.DamageRadius, 1);

            for (int i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
            }
        }

        private int Hit() => 
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, _characterController.center.y, transform.position.z);

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.HeroStats;
        }
    }
}