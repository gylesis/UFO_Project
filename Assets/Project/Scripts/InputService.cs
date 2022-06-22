using UnityEngine;
using UnityEngine.XR;
using Zenject;

namespace Project.Scripts
{
    public class InputService : ITickable
    {
        private float _deltaPositionX;
        private float _deltaPositionY;

        public bool IsActive = true;
        public Vector2 TouchDelta { get; private set; }

        private bool _isMobilePlatform;

        public InputService()
        {
            _isMobilePlatform = Application.isMobilePlatform;
        }
        
        public void Tick()
        {
            //MobileInput();
            
            if(_isMobilePlatform)
                MobileInput();
            else
                StandaloneInput();
        }
        
        private void MobileInput()
        {
            if(IsActive == false) return;
            
            float deltaX = 0;
            float deltaY = 0;

            foreach (Touch touch in Input.touches)
            {
                //Debug.Log($"Touch - {touch.fingerId}, Phase - {touch.phase}, Pos - {touch.position}, Radius {touch.radius}, Pressure {touch.pressure}, Type {touch.type}, Delta {touch.deltaPosition}, TapCount {touch.tapCount} ");

                _deltaPositionX = touch.deltaPosition.x;
                _deltaPositionY = touch.deltaPosition.y;

                deltaX = touch.deltaPosition.normalized.x;
                deltaY = touch.deltaPosition.normalized.y;
            }

            if (deltaX != 0)
            {
                if (_deltaPositionX > 0)
                {
                    deltaX = Mathf.Lerp(_deltaPositionX, _deltaPositionX - 0.5f, 0.01f);
                }
                else if (_deltaPositionX < 0)
                {
                    deltaX = Mathf.Lerp(_deltaPositionX, _deltaPositionX + 0.5f, 0.01f);
                }
            }

            if (deltaY != 0)
            {
                if (_deltaPositionY > 0)
                {
                    deltaY = Mathf.Lerp(_deltaPositionY, _deltaPositionY - 1, 0.01f);
                }
                else if (_deltaPositionY < 0)
                {
                    deltaY = Mathf.Lerp(_deltaPositionY, _deltaPositionY + 1, 0.01f);
                }
            }

            // DOVirtual.Float(_deltaPositionX, 0f, 1);

            //deltaX *= 5;
            //deltaY *= 5;

            TouchDelta = new Vector2(deltaX, deltaY);
        }

        private void StandaloneInput()
        {
            if(IsActive == false) return;
            
            Vector2 input;

            input.y = Input.GetAxis("Vertical") * 5;
            input.x = Input.GetAxis("Horizontal") * 5;
            
            TouchDelta = input;
            
        }
    }
}