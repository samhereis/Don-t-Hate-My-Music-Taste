using DependencyInjection;
using Services;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public static class GlobalGameSettings
    {
        public enum GameplayMode { Gameplay, UI }

        public static GameplayMode gameplayMode { get; private set; }

        private static PlayerInputService _inputHolder_;
        private static PlayerInputService _inputHolder
        {
            get
            {
                if (_inputHolder_ == null) { _inputHolder_ = DependencyContext.diBox.Get<PlayerInputService>(); }
                return _inputHolder_;
            }
        }

        public static void EnambleGameplayMode()
        {
            _inputHolder.input.Enable();
            _inputHolder.input.Player.Enable();
            _inputHolder.input.UI.Disable();
            _inputHolder.DisableOnUIBackPressed();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            gameplayMode = GameplayMode.Gameplay;
        }

        public static void EnableUIMode()
        {
            _inputHolder.input.Enable();
            _inputHolder.input.Player.Disable();
            _inputHolder.input.UI.Enable();
            _inputHolder.EnableOnUIBackPressed();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            gameplayMode = GameplayMode.UI;
        }
    }
}