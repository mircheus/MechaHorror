using UnityEngine;

namespace Game.Scripts
{
    public interface IState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}