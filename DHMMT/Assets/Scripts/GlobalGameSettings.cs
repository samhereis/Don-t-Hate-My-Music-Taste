using ConstStrings;
using DI;
using PlayerInputHolder;
using UnityEngine;

namespace Managers
{
    public static class GlobalGameSettings
    {
        public enum GameplayMode { Gameplay, UI }

        public static GameplayMode gameplayMode { get; private set; }

        private static Input_SO _inputHolder_;
        private static Input_SO _inputHolder
        {
            get
            {
                if (_inputHolder_ == null) { _inputHolder_ = DIBox.Get<Input_SO>(DIStrings.inputHolder); }
                return _inputHolder_;
            }
        }

        public static void EnambleGameplayMode()
        {
            _inputHolder.input.Enable();
            _inputHolder.input.Gameplay.Enable();
            _inputHolder.input.UI.Disable();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            gameplayMode = GameplayMode.Gameplay;
        }

        public static void EnableUIMode()
        {
            _inputHolder.input.Enable();
            _inputHolder.input.Gameplay.Disable();
            _inputHolder.input.UI.Enable();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            gameplayMode = GameplayMode.UI;
        }
    }
}