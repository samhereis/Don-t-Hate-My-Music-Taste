using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "intSavable_SO", menuName = "Scriptables/Savable/intSavable_SO")]
    public class intSavable_SO : BaseSavable_SO<int>
    {
        public int currentValue => _currentValue;
        public string key => _key;

        private void OnValidate()
        {
            _currentValue = PlayerPrefs.GetInt(key, _defaultValue);

#if UNITY_EDITOR

            if (name.EndsWith(_keyEndsWith) == false) _key = name + _keyEndsWith;

#endif

        }

        public override void Initialize()
        {
            _currentValue = PlayerPrefs.GetInt(key, _defaultValue);
        }

        public override void SetData(int value)
        {
            base.SetData(value);

            PlayerPrefs.SetFloat(key, _currentValue);
            PlayerPrefs.Save();
        }
    }
}