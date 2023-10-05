using Interfaces;
using UnityEngine;

namespace Managers
{
    public class GameSaveManager : MonoBehaviour, IInitializable
    {
        [ContextMenu(nameof(Initialize))]
        public void Initialize()
        {
            UpdateSaves(true);
        }

        public void SaveAll()
        {
            UpdateSaves();

        }

        private void UpdateSaves(bool force = false)
        {

        }
    }
}