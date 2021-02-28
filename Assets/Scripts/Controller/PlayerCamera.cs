using UnityEngine;


namespace NikolayT2DGame
{
    public class PlayerCamera
    {
        private Transform _cameraPosition;
        private Transform _playerPosition;
        private Vector3 _cameraOfset;

        public PlayerCamera(Transform camera, Transform player)
        {
            _cameraPosition = camera;
            _playerPosition = player;
            _cameraOfset = _cameraPosition.transform.position - _playerPosition.position;
        }

        public void LateUpdate()
        {
            _cameraPosition.transform.position = _playerPosition.position + _cameraOfset;
        }
    }
}