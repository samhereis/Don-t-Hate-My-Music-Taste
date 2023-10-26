using Helpers;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "BoolSavable_SO", menuName = "Scriptables/Settings/BoolSavable_SO")]
    public sealed class BoolSavable_SO : BaseSavable_SO<bool>
    {
        public bool currentValue => _currentValue;
        public string key => _key;

        [SerializeField] private string _trueValue = "true";
        [SerializeField] private string _falseValue = "false";

        private void OnValidate()
        {
            _currentValue = PlayerPrefs.GetString(key, _defaultValue.ToString()) == _trueValue;

#if UNITY_EDITOR

            if (name.EndsWith(_keyEndsWith) == false)
            {
                _key = name + _keyEndsWith;
                this.TrySetDirty();
            }

#endif

        }

        public override void Initialize()
        {
            _currentValue = PlayerPrefs.GetString(key, _defaultValue.ToString()) == _trueValue;
        }

        public override void SetData(bool value)
        {
            base.SetData(value);

            PlayerPrefs.SetString(key, value ? _trueValue : _falseValue);
            PlayerPrefs.Save();
        }
    }
}