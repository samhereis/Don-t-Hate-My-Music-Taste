using Configs;
using DataClasses;
using Helpers;
using Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace SO.Lists
{
    [CreateAssetMenu(fileName = nameof(ListOfAllScenes), menuName = "Scriptables/Lists/" + nameof(ListOfAllScenes))]
    public class ListOfAllScenes : ConfigBase, IInitializable
    {
        [field: SerializeField] public List<AScene_Extended> scenes { get; private set; } = new List<AScene_Extended>();
        [field: SerializeField] public AScene mainMenu { get; private set; }
        [field: SerializeField] public AScene lastLoadedScene { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

#if UNITY_EDITOR
            mainMenu.Initialize();

            foreach (var item in scenes)
            {
                item.Initialize();
            }

            this.TrySetDirty();
#endif
        }

        public List<AScene_Extended> GetScenes()
        {
            return scenes;
        }
    }
}