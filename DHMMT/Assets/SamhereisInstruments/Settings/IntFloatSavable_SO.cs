using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "intSavable_SO", menuName = "Scriptables/Savable/intSavable_SO")]
    public class intSavable_SO : ScriptableObject
    {
        [SerializeField] private int _currentValue;
        public int currentValue => _currentValue;

#if UNITY_EDITOR
        private string _keyEndsWith = "_key";
#endif

        [SerializeField] private string _key;
        public string key => _key;

        [SerializeField] private int _defaultValue = 0;

        private void OnValidate()
        {
            _currentValue = PlayerPrefs.GetInt(key, _defaultValue);

#if UNITY_EDITOR
            if (name.EndsWith(_keyEndsWith) == false) _key = name + _keyEndsWith;
#endif
        }

        private void OnEnable()
        {
            _currentValue = PlayerPrefs.GetInt(key, _defaultValue);
        }

        public void SetData(int value)
        {
            _currentValue = value;

            PlayerPrefs.SetFloat(key, _currentValue);
            PlayerPrefs.Save();
        }
    }
}