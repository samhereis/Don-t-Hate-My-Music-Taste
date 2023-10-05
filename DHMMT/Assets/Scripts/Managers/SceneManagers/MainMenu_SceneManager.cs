using DI;
using PlayerInputHolder;
using UnityEngine;

namespace Managers.SceneManagers
{
    public class MainMenu_SceneManager : SceneManagerBase, IDIDependent
    {
        [DI(ConstStrings.DIStrings.inputHolder)][SerializeField] private Input_SO _inputContainer;

        private void OnEnable()
        {
            (this as IDIDependent).LoadDependencies();

            _inputContainer.Enable();
            _inputContainer.input.Gameplay.Disable();
            _inputContainer.input.UI.Enable();

            Initialize();
        }
    }
}