using UnityEngine;


namespace NikolayT2DGame
{
    public sealed class ParalaxManager
    {
        private readonly Transform _camera;
        private readonly Transform _back;
        private Vector3 _backStartPosition;
        private Vector3 _cameraStartPosition;
        private readonly float _coef;

        public ParalaxManager(Transform camera, Transform back, float coef)
        {
            _camera = camera;
            _back = back;
            _backStartPosition = _back.transform.position;
            _cameraStartPosition = _camera.transform.position;
            _coef = coef;
        }

        public void Update()
        {
            _back.position = _backStartPosition +
                (_camera.position - _cameraStartPosition) * _coef;
        }
    }
}