using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using Object = System.Object;

namespace DI
{
    public static class DIBox
    {
        public delegate T TransientMethod<T>();

        private static readonly Dictionary<Type, Dictionary<string, object>> _dictionaryTransient = new Dictionary<Type, Dictionary<string, object>>();
        private static readonly Dictionary<Type, Dictionary<string, object>> _dictionarySingle = new Dictionary<Type, Dictionary<string, object>>();

        public static void RemoveSingle<T>(string id = "") where T : class
        {
            if (_dictionarySingle.ContainsKey(typeof(T))) _dictionarySingle[typeof(T)].Remove(id);
            if (_dictionarySingle.Count == 0) _dictionarySingle.Remove(typeof(T));
        }

        public static void RemoveSingleType(Type type, string id = "")
        {
            if (_dictionarySingle.ContainsKey(type)) _dictionarySingle[type].Remove(id);
            if (_dictionarySingle.Count == 0) _dictionarySingle.Remove(type);
        }

        public static void RemoveTransient<T>(string id = "") where T : class
        {
            if (_dictionaryTransient.ContainsKey(typeof(T))) _dictionaryTransient[typeof(T)].Remove(id);
            if (_dictionaryTransient.Count == 0) _dictionaryTransient.Remove(typeof(T));
        }

        public static T ResolveTransient<T>(string id = "") where T : class
        {
            if (_dictionaryTransient.ContainsKey(typeof(T)) == false)
            {
                throw new Exception($"DI container does not contain this type  - Type: {typeof(T)}");
            }

            if (_dictionaryTransient[typeof(T)].ContainsKey(id) == false)
            {
                throw new Exception($"The container does not contain under this ID - Type: {typeof(T)} \\ Id: '{id}'");
            }

            return ((TransientMethod<T>)_dictionaryTransient[typeof(T)][id]).Invoke();
        }

        public static async void InjectAndRegisterAsSingle<T>(T instance, string id = "")
        {
            RegisterSingle<T>(instance, id);
            await InjectDataTo(instance);
        }

        public static void RegisterTransient<T>(TransientMethod<T> transientMethod, string id = "") where T : class
        {
            if (transientMethod == null) throw new Exception($"Transit Method Create is null - type {typeof(T)}");

            if (_dictionaryTransient.ContainsKey(typeof(T)) || _dictionaryTransient[typeof(T)].ContainsKey(id))
            {
                throw new Exception($"DI container already contains this type '{typeof(T)}' and this ID '{id}' ");
            }

            if (_dictionaryTransient.ContainsKey(typeof(T)))
            {
                _dictionaryTransient[typeof(T)].Add(id, transientMethod);
            }
            else
            {
                _dictionaryTransient.Add(typeof(T), new Dictionary<string, object>());
                _dictionaryTransient[typeof(T)].Add(id, transientMethod);
            }
        }

        public static T ResolveSingle<T>(string id = "") where T : class
        {
            if (_dictionarySingle.ContainsKey(typeof(T)) == false) throw new Exception($"DI container does not contain this type  - Type: {typeof(T)}");
            if (_dictionarySingle[typeof(T)].ContainsKey(id) == false) throw new Exception($"The container does not contain under this ID - Type: {typeof(T)} \\ Id: '{id}'");

            return _dictionarySingle[typeof(T)][id] as T;
        }

        public static object ResolveSingle(Type type, string id = "")
        {
            if (_dictionarySingle.ContainsKey(type) == false) throw new Exception($"DI container does not contain this type  - Type: {type}");
            if (_dictionarySingle[type].ContainsKey(id) == false) throw new Exception($"The container does not contain under this ID - Type: {type} \\ Id: '{id}'");

            return _dictionarySingle[type][id];
        }

        public static void RegisterSingle<T>(T instance, string id = "")
        {
            if (instance == null) throw new Exception($"Instance is null - type {typeof(T)}");

            if (_dictionarySingle.ContainsKey(typeof(T)))
            {
                if (_dictionarySingle[typeof(T)].ContainsKey(id)) throw new Exception($"DI container already contains this type '{instance}' and this ID '{id}' ");
            }
            else AddToDictionary(instance, id, typeof(T));
        }

        public static void RegisterSingleType(Object instance, string id = "")
        {
            if (instance == null) throw new Exception($"Instance is null");

            Type typeInstance = instance.GetType();

            if (_dictionarySingle.ContainsKey(typeInstance))
            {
                if (_dictionarySingle[typeInstance].ContainsKey(id)) throw new Exception($"DI container already contains this type '{typeInstance}' and this ID '{id}' ");
            }
            else AddToDictionary(instance, id, typeInstance);
        }

        public static async Task<T> CreateObjectAndInjectDataToIt<T>(T prefab) where T : Component
        {
            if (prefab == null) throw null;

            bool initinalActiveStatePrefab = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);

            var createdPrefab = UnityEngine.Object.Instantiate(prefab);
            await InjectDataTo(createdPrefab);

            prefab.gameObject.SetActive(initinalActiveStatePrefab);
            createdPrefab.gameObject.SetActive(initinalActiveStatePrefab);

            return createdPrefab;
        }

        public static async Task InjectDataTo(GameObject gameObject)
        {
            foreach (var monoBeh in gameObject.GetComponentsInChildren<MonoBehaviour>(true)) await InjectDataTo(monoBeh);
        }

        public static async Task InjectDataTo(Object obj)
        {
            if (obj == null) return;

            var listFeild = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(x => CustomAttributeExtensions.GetCustomAttribute<DI>((MemberInfo)x) != null);

            foreach (var field in listFeild)
            {
                await AsyncHelper.Delay(() =>
                {
                    var att = field.GetCustomAttribute<DI>();
                    try
                    {
                        var gottenObj = ResolveSingle(field.FieldType, att.Id);
                        field.SetValue(obj, gottenObj);
                    }
                    catch (Exception ex) { Debug.LogError(ex); }
                });
            }

            var listProperty = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.GetCustomAttribute<DI>() != null);

            foreach (var prop in listProperty)
            {
                await AsyncHelper.Delay(() =>
                {
                    var att = prop.GetCustomAttribute<DI>();
                    prop.SetValue(obj, ResolveSingle(prop.PropertyType, att.Id));
                });
            }

            var listMethodInfo = obj.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            if (listMethodInfo.Length > 0)
            {
                var methodInit = listMethodInfo.Where(x => x.GetCustomAttribute<DI>() != null);
                if (methodInit.Count() > 0) methodInit.First().Invoke(obj, new object[0]);
            }
        }

        private static void AddToDictionary(object instance, string id, Type typeInstance)
        {
            if (_dictionarySingle.ContainsKey(typeInstance))
            {
                if (_dictionarySingle[typeInstance].ContainsValue(id) == false) _dictionarySingle[typeInstance].Add(id, instance);
            }
            else
            {
                _dictionarySingle.Add(typeInstance, new Dictionary<string, object>());
                _dictionarySingle[typeInstance].Add(id, instance);
            }
        }
    }
}