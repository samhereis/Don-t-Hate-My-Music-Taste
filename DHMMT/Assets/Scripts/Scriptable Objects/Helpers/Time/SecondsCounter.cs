using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Helpers
{
    [CreateAssetMenu(fileName = "SecondsCounter", menuName = "Scriptables/Helpers/SecondsCounter")]
    public class SecondsCounter : ScriptableObject
    {
        [SerializeField] private int _seconds;
        public int seconds { get { return _seconds; } set { _seconds = value; _onSecondsChange.Invoke(_seconds); } }

        private UnityEvent<int> _onSecondsChange = new UnityEvent<int>();
        public UnityEvent<int> onSecondsChange => _onSecondsChange;

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public void IncreaseSeconds(int value)
        {
            seconds += value;
        }

        public void DecreaseSeconds(int value)
        {
            seconds -= value;
        }

        public void Stop()
        {
            seconds = 0;
            _cancellationTokenSource.Cancel();
        }


        public void Beggin(float waitBeforeExecute, int BegginFrom)
        {
            Stop();
            StartCount(waitBeforeExecute, BegginFrom, _cancellationTokenSource = new CancellationTokenSource());
        }

        public void BegginCountDown(float waitBeforeExecute, int BegginFrom)
        {
            Stop();
            StartnCountDown(waitBeforeExecute, BegginFrom, _cancellationTokenSource = new CancellationTokenSource());
        }

        public void Pause()
        {

        }

        public void UnPause()
        {

        }

        private async void StartCount(float waitBeforeExecute, int BegginFrom, CancellationTokenSource sentCancellationTokenSource)
        {
            await AsyncHelper.Delay(waitBeforeExecute);
            seconds = BegginFrom;

            while (sentCancellationTokenSource.IsCancellationRequested == false)
            {
                await AsyncHelper.Delay(1, () => IncreaseSeconds(1));
            }
        }

        private async void StartnCountDown(float waitBeforeExecute, int BegginFrom, CancellationTokenSource sentCancellationTokenSource)
        {
            await AsyncHelper.Delay(waitBeforeExecute);
            seconds = BegginFrom;

            while (sentCancellationTokenSource.IsCancellationRequested == false)
            {
                DecreaseSeconds(1);
                await AsyncHelper.Delay(1, () => { if (seconds < 1) Stop(); });
            }
        }
    }
}
