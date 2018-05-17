using UnityEngine;

namespace NES
{
    public class StandardController : MonoBehaviour
    {
        private Emulator emulator;

        private void Update()
        {
            if(emulator == null)
            {
                return;
            }

            emulator.controllerOne.Input(Controller.Button.Left, Input.GetKey(KeyCode.A));
            emulator.controllerOne.Input(Controller.Button.Right, Input.GetKey(KeyCode.D));
            emulator.controllerOne.Input(Controller.Button.Up, Input.GetKey(KeyCode.W));
            emulator.controllerOne.Input(Controller.Button.Down, Input.GetKey(KeyCode.S));
            emulator.controllerOne.Input(Controller.Button.A, Input.GetKey(KeyCode.I));
            emulator.controllerOne.Input(Controller.Button.B, Input.GetKey(KeyCode.U));
            emulator.controllerOne.Input(Controller.Button.Start, Input.GetKey(KeyCode.Return));
            emulator.controllerOne.Input(Controller.Button.Select, Input.GetKey(KeyCode.Backspace));
        }

        public void StartController(Emulator newEmulator)
        {
            emulator = newEmulator;
        }
    }
}
