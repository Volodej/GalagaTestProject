using System;
using UnityEngine;

namespace UserInput
{
    public class KeyboardInput : MonoBehaviour, IUserInput
    {
        public event Action Fired = () => { };
        public event Action<float> Moved = _ => { };

        private void Update()
        {
            if (!Input.anyKey)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
                Fired();

            var leftInput = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;
            var rightInput = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
            Moved(leftInput + rightInput);
        }
    }
}