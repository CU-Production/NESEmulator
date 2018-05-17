using System;

namespace NES
{
    [Serializable]
    public class Emulator
    {
        // NTSC Constants
        public const int ScreenWidth = 256;
        public const int ScreenHeight = 240;
        public const int ApuFrameCounterRate = 60;
        public const int PpuDotsPerCpuCycle = 3;

        public Cpu cpu;
        public readonly Ppu ppu;
        public readonly Apu apu;
        public readonly Controller controllerOne;
        public readonly Controller controllerTwo;
        public readonly Mapper mapper;

        private bool isValid;
        public bool IsValid => isValid;

        private Action onBeforeStep;

        private bool isRunning;
        private bool shouldReset;
        private bool stepMode;
        private bool shouldStep;

        public Emulator(Cartridge cartridge)
        {
            // Load Mapper
            mapper = cartridge.Mapper switch
            {
                000 => new Nrom(cartridge),
                // 001 or 105 or 155 => new Mmc1(cartridge),
                002 or 094 or 180 => new UxRom(cartridge),
                _ => null,
            };

            if (mapper == null)
            {
                return;
            }

            cpu = new Cpu(this);
            ppu = new Ppu(this);
            apu = new Apu(this);
            controllerOne = new Controller();
            controllerTwo = new Controller();

            isValid = true;
        }

        public void Init()
        {
            if (!IsValid)
            {
                return;
            }

            isRunning = true;
        }

        public void Frame()
        {
            var originalOddFrame = ppu.OddFrame;

            while (isRunning && originalOddFrame == ppu.OddFrame)
            {
                if (stepMode & !shouldStep)
                {
                    return;
                }

                shouldStep = false;

                if (shouldReset)
                {
                    cpu.Reset();
                    ppu.Reset();
                    apu.Reset();

                    originalOddFrame = ppu.OddFrame;

                    shouldReset = false;
                }

                onBeforeStep?.Invoke();

                var cycles = cpu.Step();

                for (var i = 0; i < cycles * PpuDotsPerCpuCycle; i++)
                {
                    ppu.Step();
                }

                for (var i = 0; i < cycles; i++)
                {
                    apu.Step();
                }
            }
        }

        public void StepMode(bool newStepMode)
        {
            stepMode = newStepMode;
        }

        public void Step()
        {
            shouldStep = true;
        }

        public void Reset()
        {
            shouldReset = true;
        }

        public void Stop()
        {
            isRunning = false;
        }

        public void RegisterOnBeforeStep(Action newOnBeforeStep)
        {
            onBeforeStep += newOnBeforeStep;
        }

        public void UnregisterOnBeforeStep(Action newOnBeforeStep)
        {
            onBeforeStep -= newOnBeforeStep;
        }
    }
}
