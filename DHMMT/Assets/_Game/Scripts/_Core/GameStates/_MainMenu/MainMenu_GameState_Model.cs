using ConstStrings;
using DataClasses;
using DependencyInjection;
using GameState;
using Observables;
using SO;
using SO.Lists;

namespace GameStates
{
    public class MainMenu_GameState_Model : GameState_ModelBase, INeedDependencyInjection
    {
        [Inject] public ListOfAllScenes_Extended listOfAllScenes;
        [Inject] public ListOfAllViews listOfAllViews;

        [Inject(DataSignal_ConstStrings.onASceneSelected)] public DataSignal<AScene_Extended> onASceneSelected;
        [Inject(DataSignal_ConstStrings.onASceneLoadRequested)] public DataSignal<AScene_Extended> onASceneLoadRequested;

        public override void Initialize()
        {
            base.Initialize();

            DependencyContext.InjectDependencies(this);
            isInitialized = true;
        }
    }
}