using System;
using UnityEditor.Animations;

namespace CodeBase.Hero
{
    public interface IAnimationStateReader
    {
        event Action<AnimatorState> StateEntered;
        event Action<AnimatorState> StateExited;
        bool isAttacking { get; }
        void PlayHit();
        void PlayAttack();
        void PlayDeath();
        void ResetToIdle();
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
    }
}