using DG.Tweening;
using Project.Scripts.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Camera
{
    public class CameraController : ITickable
    {
        private readonly UnityEngine.Camera _camera;
        private readonly CameraContainer _cameraContainer;
        private readonly Config _config;
        private readonly PlayerContainer _playerContainer;
        private readonly CoordinatesService _coordinatesService;
        
        private Tweener _sizeTweener;
        private Tweener _posTweener;

        public CameraController(CameraContainer cameraContainer, Config config, PlayerContainer playerContainer,
            CoordinatesService coordinatesService)
        {
            _coordinatesService = coordinatesService;
            _playerContainer = playerContainer;
            _config = config;
            _cameraContainer = cameraContainer;
            _camera = cameraContainer.Camera;
        }

        public void Tick()
        {
            /*if (Input.GetKey(KeyCode.DownArrow))
                MoveY(1);
            else if (Input.GetKey(KeyCode.UpArrow))
                MoveY(-1);*/

            // MoveX(_inputService.TouchDelta.normalized.x);
            // MoveY(_inputService.TouchDelta.normalized.y);
            
            Rotate();
        }

        private void Rotate()
        {
            Vector3 playerPos = _playerContainer.Transform.position;

            var playerAngle = _coordinatesService.GetAngleDeg(playerPos);

            Vector3 eulerAngles = _cameraContainer.CamParent.rotation.eulerAngles;
            eulerAngles.z = playerAngle - 90;

            Quaternion camParentRotation = Quaternion.Euler(eulerAngles);
            _cameraContainer.CamParent.rotation = Quaternion.Lerp(_cameraContainer.CamParent.rotation,
                camParentRotation, Time.deltaTime * _config.CameraXSpeedMovement.Value);
        }
        public void SwitchHeightLevel(CameraHeightLevelInfo heightLevelInfo)
        {
            var orthographicSize = _camera.orthographicSize;
            var neededOrthographicSize = heightLevelInfo.OrthographicSize;

            var positionY = _camera.transform.localPosition.y;
            var neededYHeight = heightLevelInfo.YHeight;

            _sizeTweener?.Kill();
            _posTweener?.Kill();

            _sizeTweener = DOVirtual.Float(orthographicSize, neededOrthographicSize, 1,
                    (value => _camera.orthographicSize = value))
                .SetEase(heightLevelInfo.PosTransition);
            
            _posTweener = DOVirtual.Float(positionY, neededYHeight, 1, (value =>
                {
                    Vector3 transformPosition = _camera.transform.localPosition;
                    transformPosition.y = value;
                    _camera.transform.localPosition = transformPosition;
                }))
                .SetEase(heightLevelInfo.PosTransition);
        }
    }
}