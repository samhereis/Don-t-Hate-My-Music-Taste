using UnityEngine;

namespace Scriptables.Gameplay
{
    [CreateAssetMenu(fileName = "Sensitivity", menuName = "Scriptables/Settings/FloatSetting")]
    public class FloatSetting_SO : ScriptableObject
    {
        [SerializeField] private float _currentValue;
        public float currentValue => _currentValue;

#if UNITY_EDITOR
        private string keyStartsWith = "";
        private string keyEndsWith = "_Key";
#endif

        [SerializeField] private string _KEY;
        public string KEY => _KEY;

        [SerializeField] private float _defaultValue = 0.5f;

        private void OnValidate()
        {
            _currentValue = PlayerPrefs.GetFloat(KEY, _defaultValue);

            if(name.StartsWith("_Key") == false)
            {
                _KEY = keyStartsWith + name + keyEndsWith;
            }
        }

        private void OnEnable()
        {
            _currentValue = PlayerPrefs.GetFloat(KEY, _defaultValue);
        }

        public void SetData(float value)
        {
            _currentValue = value;

            PlayerPrefs.SetFloat(KEY, value);
            PlayerPrefs.Save();
        }
    }
}