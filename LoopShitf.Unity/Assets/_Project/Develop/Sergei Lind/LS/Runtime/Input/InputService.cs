using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sergei_Lind.LS.Runtime.Utilities;
using UnityEngine.InputSystem;

namespace Sergei_Lind.LS.Runtime.Input
{
    [UsedImplicitly]
    public sealed class InputService : IDisposableLoadUnit, IInput
    {
        private InputSystem_Actions _inputActions;
        public event Action Tap;
        
        public UniTask Load()
        {
            _inputActions = new InputSystem_Actions();
            _inputActions.Player.Enable();
            _inputActions.Player.Attack.performed += OnTapPerformed;
            return UniTask.CompletedTask;
        }

        public void Dispose()
        {
            _inputActions.Player.Attack.performed -= OnTapPerformed;
            _inputActions.Dispose();
        }
        
        private void OnTapPerformed(InputAction.CallbackContext ctx)
        {
            Tap?.Invoke();
        }
    }
}