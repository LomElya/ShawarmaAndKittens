using System;

namespace InputSystem
{
    public interface IInput
    {
        event Action OnClickPause;

        void Register(IMovable movable);
        void Unregister();
    }
}
