using DataClasses;
using Helpers;
using Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace SO.Lists
{
    [CreateAssetMenu(fileName = "ListOfAllScenes", menuName = "Scriptables/Lists/ListOfAllScenes")]
    public class ListOfAllScenes : ScriptableObject, IInitializable
    {
        [field: SerializeField] public List<AScene_Extended> scenes { get; private set; } = new List<AScene_Extended>();
        [field: SerializeField] public AScene mainMenu { get; private set; }

        public void Initialize()
        {

#if UNITY_EDITOR

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