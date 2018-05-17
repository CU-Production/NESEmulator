using UnityEngine;

namespace NES
{
    public class NESManager : MonoBehaviour
    {
        [SerializeField]
        private NESScreen screen;

        [SerializeField]
        private NESAudio nesAudio;

        [SerializeField]
        private StandardController controller;

        [SerializeField]
        private PaletteScriptableObject palletteSO;
        public Color32[] Palette => palletteSO.palette;

#if UNITY_EDITOR
        [Header("Debugging")]

        [SerializeField]
        private bool stepMode;
#endif
        [HideInInspector]
        public Emulator emulator;

        [HideInInspector]
        public long frameCount;

        private double nextUpdate;

        private void Start()
        {
#if !UNITY_EDITOR
            StartEmulator(@"D:\Projects\NESEmulator\Roms\donkey kong.nes");
#endif
        }

        private void Update()
        {
            if (!(emulator?.IsValid ?? false))
            {
                return;
            }

            nextUpdate -= Time.deltaTime;

            if (nextUpdate > 0)
            {
                return;
            }

            emulator.Frame();

            frameCount++;

            nextUpdate += 1d / Emulator.ApuFrameCounterRate;
        }

        private void OnDestroy()
        {
            StopEmulator();
        }

        private void OnValidate()
        {
            UpdateStepMode();
        }

        public void StartEmulator(string path)
        {
            var cartridge = new Cartridge(path);
            if (!cartridge.IsValid)
            {
                return;
            }

            emulator = new Emulator(cartridge);
            if (!emulator.IsValid)
            {
                return;
            }

            screen.StartOutput(this);
            nesAudio.StartOutput(this);
            controller.StartController(emulator);

#if UNITY_EDITOR
            UpdateStepMode();
#endif

            emulator.Init();
        }

        private void UpdateStepMode() => emulator?.StepMode(stepMode);
        public void StepEmulator() => emulator?.Step();
        public void ResetEmulator() => emulator?.Reset();
        public void StopEmulator() => emulator?.Stop();
    }
}
