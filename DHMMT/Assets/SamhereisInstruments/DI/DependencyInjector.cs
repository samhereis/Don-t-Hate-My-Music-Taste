using AYellowpaper.SerializedCollections;
using Configs;
using Events;
using Interfaces;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DI
{
    public class DependencyInjector : MonoBehaviour
    {
        public static bool isGLoballyInjected { get; private set; } = false;

        [Header("Objects To DI")]
        [SerializeField] private List<MonoBehaviourToDI> _objects = new List<MonoBehaviourToDI>();
        [SerializeField] private List<ConfigToDI> _configs = new List<ConfigToDI>();
        [SerializeField] private List<SOToDI> _scriptableObjects = new List<SOToDI>();
        [SerializeField] private List<EventToDI> _eventsWithNoParameters = new List<EventToDI>();

        [Header("Settings")]
        [SerializeField] private bool _isGlobal = false;

        [Header("Debug")]
        [SerializeField] private HardCodeDependencyInjectorBase[] _hardCodeDependencyInjectors;
        [SerializeField] private SerializedDictionary<string, SerializedDictionary<string, string>> _currentDIBox;

        private void Awake()
        {
            _hardCodeDependencyInjectors = GetComponents<HardCodeDependencyInjectorBase>();

            if (_isGlobal == true && isGLoballyInjected == true)
            {
                Destroy(gameObject);
                return;
            }

            Clear();
            Inject();
            InitAll();

            if (_isGlobal == true)
            {
                isGLoballyInjected = true;
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnDestroy()
        {

#if UNITY_EDITOR

            if (EditorApplication.isPlayingOrWillChangePlaymode == false && EditorApplication.isPlaying)
            {
                Debug.Log("Exiting playmode.");
                Clear();
            }
#endif
            if (_isGlobal == false)
            {
                Clear();
            }
        }

        private void Update()
        {
            _currentDIBox = DIBox.GetCopy();
        }

        private void Inject()
        {
            foreach (var objectToInject in _objects)
            {
                if (DIBox.Get(objectToInject.instance.GetType(), objectToInject.id, false) == null)
                {
                    DIBox.Add(objectToInject.instance, objectToInject.id);
                }
            }

            foreach (var config in _configs)
            {
                if (DIBox.Get(config.instance.GetType(), config.id, false) == null)
                {
                    DIBox.Add(config.instance, config.id);
                }
            }

            foreach (var scriptableObject in _scriptableObjects)
            {
                if (DIBox.Get(scriptableObject.instance.GetType(), scriptableObject.id, false) == null)
                {
                    DIBox.Add(scriptableObject.instance, scriptableObject.id);
                }
            }

            foreach (var eventWithNoParameter in _eventsWithNoParameters)
            {
                if (DIBox.Get(eventWithNoParameter.instance.GetType(), eventWithNoParameter.id, false) == null)
                {
                    DIBox.Add(eventWithNoParameter.instance, eventWithNoParameter.id);
                }
            }

            foreach (var hcdi in _hardCodeDependencyInjectors)
            {
                hcdi.Inject();
            }
        }

        private void InitAll()
        {
            foreach (var objectToInject in _objects)
            {
                if (objectToInject.instance is IInitializable)
                {
                    var initializable = (objectToInject.instance as IInitializable);
                    if (initializable.GetCanInitializeWithDI() == true) { initializable.Initialize(); }
                }
            }

            foreach (var config in _configs)
            {
                config.instance.Initialize();
            }

            foreach (var scriptableObject in _scriptableObjects)
            {
                if (scriptableObject.instance is IInitializable)
                {
                    (scriptableObject.instance as IInitializable).Initialize();
                }
            }

            foreach (var eventWithNoParameter in _eventsWithNoParameters)
            {
                eventWithNoParameter.Initialize();
            }
        }

        private void Clear()
        {
            foreach (var objectToInject in _objects)
            {
                DIBox.Remove(objectToInject.instance.GetType(), objectToInject.id);
            }

            foreach (var config in _configs)
            {
                DIBox.Remove(config.instance.GetType(), config.id);
            }

            foreach (var scriptableObject in _scriptableObjects)
            {
                DIBox.Remove(scriptableObject.instance.GetType(), scriptableObject.id);
            }

            foreach (var eventWithNoParameter in _eventsWithNoParameters)
            {
                DIBox.Remove(eventWithNoParameter.instance.GetType(), eventWithNoParameter.id);
            }

            foreach (var hcdi in _hardCodeDependencyInjectors)
            {
                hcdi.Clear();
            }

            if (_isGlobal == true)
            {
                isGLoballyInjected = false;
            }
        }

        [Serializable]
        public class MonoBehaviourToDI
        {
            public string id = "";
            public MonoBehaviour instance;
        }

        [Serializable]
        public class ConfigToDI
        {
            public string id = "";
            public ConfigBase instance;
        }

        [Serializable]
        public class SOToDI
        {
            public string id = "";
            public ScriptableObject instance;
        }

        [Serializable]
        public class EventToDI : IInitializable
        {
            public string id = "";
            public EventWithNoParameters instance;

            public void Initialize()
            {
                instance = new EventWithNoParameters(id);
            }
        }
    }
}