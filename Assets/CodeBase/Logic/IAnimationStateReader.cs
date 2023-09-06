using System;

namespace CodeBase.Logic
{
    public interface IAnimationStateReader
    {
        event Action<AnimatorState> StateEntered;
        event Action<AnimatorState> StateExited;
        
        AnimatorState State { get; }
        
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
    }
}