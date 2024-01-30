using DataClasses;
using Helpers;
using Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace SO.Lists
{
    [CreateAssetMenu(fileName = nameof(ListOfAllScenes_Extended), menuName = "Scriptables/Lists/" + nameof(ListOfAllScenes_Extended))]
    public class ListOfAllScenes_Extended : ListOfAllScenes, IInitializable
    {
        [field: SerializeField] public List<AScene_Extended> scenes { get; private set; } = new List<AScene_Extended>();
        [field: SerializeField] public AScene lastLoadedScene { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

#if UNITY_EDITOR
            mainMenuScene.Initialize();

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