using Configs;
using DataClasses;
using Helpers;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO.Lists
{
    [CreateAssetMenu(fileName = nameof(SimpleListOfAllScenes), menuName = "Scriptables/Lists/" + nameof(SimpleListOfAllScenes))]
    public class SimpleListOfAllScenes : ConfigBase, IInitializable
    {
        [Required]
        [field: SerializeField] public AScene mainMenuScene { get; private set; }

        [Required]
        [field: SerializeField] public AScene gameScene { get; private set; }

        public override void Initialize()
        {
            mainMenuScene.Initialize();
            gameScene.Initialize();

            this.TrySetDirty();
        }
    }
}