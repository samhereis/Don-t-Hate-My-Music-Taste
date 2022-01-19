using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Helpers
{
    [CreateAssetMenu(fileName = "SecondsCounter", menuName = "Scriptables/Helpers/SecondsCounter")]
    public class SecondsCounter : ScriptableObject
    {
        [SerializeField] private int _seconds;

        private UnityEvent<int> _onSecondsChange = new UnityEvent<int>();
        public UnityEvent<int> onSecondsChange => _onSecondsChange;

        public void IncreaseSeconds(int value)
        {
            Seconds += value;
            _text.text = Seconds.ToString();
        }

        public void DecreaseSeconds(int value)
        {
            Seconds -= value;
            _text.text = Seconds.ToString();
        }

        public void NullSeconds()
        {
            Seconds = 0;
            _text.text = Seconds.ToString();
        }


        public void Beggin(float waitBeforeExecute, int BegginFrom)
        {
            Stop();
            StartCoroutine(StartCount(waitBeforeExecute, BegginFrom));
        }

        public void BegginCountDown(float waitBeforeExecute, int BegginFrom)
        {
            Stop();
            StartCoroutine(StartnCountDown(waitBeforeExecute, BegginFrom));
        }

        public void Pause()
        {

        }

        public void UnPause()
        {

        }

        public void Stop()
        {
            NullSeconds();
            StopAllCoroutines();
        }

        IEnumerator StartCount(float waitBeforeExecute, int BegginFrom)
        {
            yield return Wait.NewWait(waitBeforeExecute);

            Seconds = BegginFrom;

            while (true)
            {
                IncreaseSeconds(1);
                yield return Wait.NewWaitRealTime(1);
            }
        }

        IEnumerator StartnCountDown(float waitBeforeExecute, int BegginFrom)
        {
            yield return Wait.NewWait(waitBeforeExecute);

            Seconds = BegginFrom;

            while (true)
            {
                DecreaseSeconds(1);

                if (Seconds < 1)
                {
                    PlayerHealthData.instance.GetComponent<IMatchWinable>().Win();
                }

                yield return Wait.NewWaitRealTime(1);
            }
        }
    }
}
