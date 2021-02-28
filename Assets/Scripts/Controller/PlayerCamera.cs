using UnityEngine;


namespace NikolayT2DGame
{
    public class PlayerCamera
    {
        private Transform _cameraPosition;
        private Transform _playerPosition;
        private Vector3 _cameraOfset;
        private Vector3 _cameraPositionBufer;

        public PlayerCamera(Transform camera, Transform player)
        {
            _cameraPosition = camera;
            _playerPosition = player;
            _cameraOfset = _cameraPosition.transform.position - _playerPosition.position;
            _cameraPositionBufer = _playerPosition.position + _cameraOfset;
        }

        public void LateUpdate()
        {
            _cameraPositionBufer.x = _playerPosition.position.x;
            _cameraPosition.position = _cameraPositionBufer;
        }
    }
}