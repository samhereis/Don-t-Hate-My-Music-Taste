using System.Collections.Generic;
using UnityEngine;

namespace DI
{
    public class BindDIScene : MonoBehaviour
    {
        [SerializeField] private List<ObjectToDi> _objects = new List<ObjectToDi>();
        [SerializeField] private FactoryDI[] _manualBind;

        private void Awake()
        {
            foreach (var manual in _manualBind) manual.Create();
            foreach (var obj in _objects) DIBox.RegisterSingleType(obj.Instance, obj.id);
        }

        private void OnDestroy()
        {
            foreach (var manual in _manualBind) manual.DestroyDi();
            foreach (var obj in _objects)
            {
                if (obj.IsUnbind) DIBox.RemoveSingleType(obj.Instance.GetType(), obj.id);
            }
        }

        [System.Serializable]
        public class ObjectToDi
        {
            public bool IsUnbind = true;
            public string id = "";
            public Component Instance;
        }
    }
}