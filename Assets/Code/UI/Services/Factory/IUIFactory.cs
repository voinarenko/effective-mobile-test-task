using Code.Infrastructure.States.StateMachine;
using Code.Services;
using Code.UI.Windows;
using UnityEngine;

namespace Code.UI.Services.Factory
{
    public interface IUiFactory : IService
    {
        Transform UIRoot { get; set; }
        EndGameWindow EndGameWindow { get; set; }
        void CreatePause(IGameStateMachine stateMachine);
    }
}