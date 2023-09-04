using System;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int MoveHash = StringToHash("Walking");
        private static readonly int AttackHash = StringToHash("AttackNormal");
        private static readonly int HitHash = StringToHash("Hit");
        private static readonly int DieHash = StringToHash("Die");

        private readonly int _idleStateHash = StringToHash("Idle");
        private readonly int _idleStateFullHash = StringToHash("Base Layer.Idle");
        private readonly int _attackStateHash = StringToHash("Attack Normal");
        private readonly int _walkingStateHash = StringToHash("Run");
        private readonly int _deathStateHash = StringToHash("Die");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        
        public AnimatorState State { get; private set; }

        public Animator Animator;
        public CharacterController CharacterController;

        private void Update()
        {
            Animator.SetFloat(MoveHash, CharacterController.velocity.magnitude, 0f, Time.deltaTime);
        }

        public bool isAttacking => State == AnimatorState.Attack;
        
        public void PlayHit() => SetTrigger(HitHash);
        
        public void PlayAttack() => SetTrigger(AttackHash);

        public void PlayDeath() => SetTrigger(DieHash);

        public void ResetToIdle() => Animator.Play(_idleStateHash, -1);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash)
        {
            StateExited?.Invoke(StateFor(stateHash));
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            
            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _walkingStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;
            return state;
        }
        
        private static int StringToHash(string animationName) 
            => Animator.StringToHash(animationName);

        private void SetTrigger(int hash) 
            => Animator.SetTrigger(hash);
    }

    public enum AnimatorState
    {
        Unknown,
        Idle,
        Attack,
        Walking,
        Died,
    }
}