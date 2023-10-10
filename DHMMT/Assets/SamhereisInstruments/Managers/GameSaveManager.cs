using DataClasses;
using Helpers;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class GameSaveManager : MonoBehaviour, IInitializable
    {
        [field: SerializeField, Header("Mode Saves")] public TD_Saves modeTDSaves { get; private set; }

        [ContextMenu(nameof(Initialize))]
        public void Initialize()
        {
            GetSaves();
        }

        public void Save(SaveBase save)
        {
            SaveHelper.SaveToJson(save, save.folderName, save.fileName);
        }

        public void SaveAll()
        {
            Save(modeTDSaves);
        }

        public T GetSave<T>(string folderName, string fileName) where T : SaveBase, new()
        {
            var save = SaveHelper.GetStoredDataClass<T>(folderName, fileName);

            if (save == null) { save = new T(); }

            return save;
        }

        public void GetSaves()
        {
            modeTDSaves = GetSave<TD_Saves>(modeTDSaves.folderName, modeTDSaves.fileName);
        }
    }
}