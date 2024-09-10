using System;
using GameHandler;
using UnityEngine;
using Zenject;

namespace InputSystem
{
    public class JoystickInput : MonoBehaviour, IInput
    {
        private UpdateHandler _updateHandler;

        [Inject]
        private void Construct(UpdateHandler updateHandler)
           => _updateHandler = updateHandler;

        [SerializeField] private Joystick _joystick;

        public event Action OnClickPause;
        public event Action Moved;

        public bool LastFrameMoving { get; private set; }

        public bool Moving => _joystick.Direction != Vector2.zero;
        public bool StoppedMoving => !Moving && LastFrameMoving;

        private IMovable _movable;

        private void Awake()
            => SetActive(false);

        public void Register(IMovable movable)
        {
            SetActive(true);

            Input.multiTouchEnabled = false;
            _movable = movable;

            Subscrive();
        }

        public void Unregister()
        {
            SetActive(false);
            Unsubscrive();
            _movable?.Stop();
        }

        private void OnUpdate()
        {
            if (_movable == null)
                return;

            if (!Moving)
            {
                _movable.Stop();
                return;
            }

            Vector2 rawDirection = new Vector2(_joystick.Direction.x, _joystick.Direction.y);
            _movable.Move(rawDirection);

            Moved?.Invoke();
        }

        private void OnLateUpdate() => LastFrameMoving = Moving;
        private void SetActive(bool isActive) => this.gameObject?.SetActive(isActive);

        private void Subscrive()
        {
            _updateHandler.AddUpdate(OnUpdate);
            _updateHandler.AddLateUpdate(OnLateUpdate);
        }

        private void Unsubscrive()
        {
            _updateHandler.RemoveUpdate(OnUpdate);
            _updateHandler.RemoveLateUpdate(OnLateUpdate);
        }
    }
}
