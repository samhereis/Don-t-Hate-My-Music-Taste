using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Samhereis.DI
{
    public class BindDIScene : MonoBehaviour
    {
        [SerializeField] private  List<ObjectToDI> _objects = new List<ObjectToDI>();
        [SerializeField] private List<SOToDI> _scriptableObjects = new List<SOToDI>();

        [SerializeField] private FactoryDI[] _manualBind;

        [Header("Dependencies")]
        [SerializeField] private AudioMixer _audioMixer;

        [Header("Dependencies")]
        [SerializeField] private MainMenuIdentifier _mainMenuPrefab;
        private async void Awake()
        {
            DIBox.RegisterSingle<AudioMixer>(_audioMixer);

            foreach (var manual in _manualBind) manual.Create();
            foreach (var obj in _objects) DIBox.RegisterSingleType(obj.Instance, obj.id);
            foreach (var obj in _scriptableObjects) DIBox.RegisterSingleType(obj.Instance, obj.id);

            await DIBox.CreateObjectAndInjectDataToIt<IdentifierBase>(_mainMenuPrefab);
        }

        private void OnDestroy()
        {
            foreach (var manual in _manualBind) manual.DestroyDi();

            foreach (var obj in _objects)
            {
                if (obj.IsUnbind) DIBox.RemoveSingleType(obj.Instance.GetType(), obj.id);
            }

            foreach (var obj in _scriptableObjects)
            {
                if (obj.IsUnbind) DIBox.RemoveSingleType(obj.Instance.GetType(), obj.id);
            }
        }

        [System.Serializable] public class ObjectToDI
        {
            public bool IsUnbind= true;
            public string id = "";
            public Component Instance;
        }

        [System.Serializable] public class SOToDI
        {
            public bool IsUnbind = true;
            public string id = "";
            public ScriptableObject Instance;
        }
    }
}