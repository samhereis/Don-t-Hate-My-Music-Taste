using Configs;
using ErtenGamesInstrumentals.DataClasses;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Canvases;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = nameof(ListOfAllViews), menuName = "Scriptables/Lists/" + nameof(ListOfAllViews))]
    public class ListOfAllViews : ConfigBase
    {
        [SerializeField] private List<PrefabReference<MenuBase>> _views = new();

        public T GetView<T>() where T : MenuBase
        {
            T result = null;

            foreach (PrefabReference<MenuBase> view in _views)
            {
                if (view.targetTypeName == typeof(T).Name)
                {
                    result = (T)view.GetAsset();
                    continue;
                }
            }

            return result;
        }

        public async Task<T> GetViewAsync<T>() where T : MenuBase
        {
            T result = null;

            foreach (PrefabReference<MenuBase> view in _views)
            {
                if (view.targetTypeName == typeof(T).Name)
                {
                    var menu = await view.GetAssetAsync();
                    result = (T)menu;
                    continue;
                }
            }

            return result;
        }
    }
}