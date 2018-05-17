using UnityEngine;

namespace NES
{
    [RequireComponent(typeof(AudioSource))]
    public class NESAudio : MonoBehaviour
    {
        private const float RadianConversion = 2.0f * Mathf.PI;

        private float frequency = 440.0f;
        private float phase;

        private float sampleRate;
        private float sampleTime;

        private NESManager manager;
        private Apu apu;

        private void Start()
        {
            sampleRate = AudioSettings.outputSampleRate;
            sampleTime = 1 / sampleRate;
        }

        void OnAudioFilterRead(float[] data, int channels)
        {
            if (apu == null)
            {
                return;
            }
            
            int dataLen = data.Length / channels;

            int n = 0;
            while (n < dataLen)
            {
                float x = Mathf.Sin(phase * frequency * RadianConversion);
                int i = 0;
                while (i < channels)
                {
                    data[n * channels + i] += x;
                    i++;
                }
                phase += sampleTime;
                n++;
            }

        }

        public void StartOutput(NESManager newManager)
        {
            manager = newManager;
            apu = manager.emulator.apu;
        }
    }
}
