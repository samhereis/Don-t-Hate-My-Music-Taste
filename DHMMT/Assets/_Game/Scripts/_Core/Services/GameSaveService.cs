using DataClasses;
using Helpers;
using Interfaces;
using System.Threading.Tasks;
using UnityEngine;

namespace Services
{
    public class GameSaveService : GameSavesServiesBase, IInitializable
    {
        [field: SerializeField, Header("Mode Saves")] public TD_Saves modeTDSaves { get; private set; } = new();

        public GameSaveService()
        {
            Initialize();
        }

        public override async void Initialize()
        {
            base.Initialize();

            await LoadSaved();
        }

        protected override async Task LoadSaved()
        {
            modeTDSaves = GetSave<TD_Saves>(modeTDSaves.folderName, modeTDSaves.fileName);
            await AsyncHelper.Skip();
        }

        public override async Task UploadSaves()
        {
            Save(modeTDSaves);
            await AsyncHelper.Skip();
        }

        public void Save(SaveBase save)
        {
            SaveHelper.SaveToJson(save, save.folderName, save.fileName);
        }

        public T GetSave<T>(string folderName, string fileName) where T : SaveBase, new()
        {
            var save = SaveHelper.GetStoredDataClass<T>(folderName, fileName);

            if (save == null) { save = new T(); }

            return save;
        }
    }
}