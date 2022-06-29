using UnityEngine;

namespace UI
{
    public class SingleMessageHandle : MonoBehaviour
    {
        [SerializeField] private float _showTime = 5;

        private void OnEnable()
        {
            ShowMessage(_showTime);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void ShowMessage(float ShowTime)
        {

        }
    }
}