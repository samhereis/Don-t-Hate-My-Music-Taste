using SO.DataHolders;
using UnityEngine;

namespace DI
{
    public class Global_HCDI : HardCodeDependencyInjectorBase
    {
        [SerializeField] private AudioMixerDataHolder _audioMixer;

        public override void Inject()
        {
            base.Inject();

            DIBox.Remove<AudioMixerDataHolder>();
            DIBox.Add<AudioMixerDataHolder>(_audioMixer);
        }

        public override void Clear()
        {
            base.Clear();

            DIBox.Remove<AudioMixerDataHolder>();
        }
    }
}