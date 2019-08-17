using System;

namespace UserInput
{
    public interface IUserInput
    {
        event Action Fired;
        event Action<float> Moved;
        void Enable();
        void Disable();
    }
}